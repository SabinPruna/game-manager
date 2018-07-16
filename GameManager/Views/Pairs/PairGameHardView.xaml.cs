using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GameManager.BussinessLayer;
using GameManager.Models.Entities;
using GameManager.ViewModels.Pairs;

namespace GameManager.Views.Pairs
{
    public partial class GameHardView : Window
    {
        private readonly GameRecordManager _gameRecordManager;

        #region Constructors

        public GameHardView(Player player)
        {
            InitializeComponent();
            ViewModel.Score = 0;
            ViewModel.Player = player;
            _gameRecordManager = new GameRecordManager();
        }

        #endregion

        #region  Properties

        public PairGameHardVM ViewModel => DataContext as PairGameHardVM;

        #endregion


        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            GameRecord gameRecord = new GameRecord
            {
                Player = ViewModel.Player,
                Date = DateTime.Now,
                Game = Title,
                Score = ViewModel.Score
            };
            _gameRecordManager.Add(gameRecord);

            GamesView gamesView = new GamesView(ViewModel.Player);
            gamesView.Show();
            ViewModel.DispatcherTimer.Stop();
            ViewModel.State = StateOfGame.GameOver;
            Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.DefaultTime == 0 && ViewModel.Time == 0)
            {
                ViewModel.DefaultTime = 200;
                ViewModel.OnPropertyChanged("DefaultTime");
                ViewModel.Time = 200;
            }

            if (ViewModel.State == StateOfGame.GameOver)
            {
                ViewModel.DispatcherTimer.Start();
                for (int i = 0; i < 64; i++) gameGrid.Children[i].Visibility = Visibility.Visible;
                TimeLabel.Visibility = Visibility.Visible;

                ViewModel.Level = 1;
                ViewModel.State = StateOfGame.Running;
                ViewModel.GenerateCards();
                ViewModel.Time = ViewModel.DefaultTime;
            }
            else
            {
                MessageBox.Show("You are still in the process of playing", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.Time > 0 && ViewModel.State == StateOfGame.Running)
            {
                string nameOfTheCard = (sender as Image).Name;
                nameOfTheCard = nameOfTheCard.Substring(nameOfTheCard.Length - 2, 2);
                int indexOfTheCard;
                if (int.TryParse(nameOfTheCard, out indexOfTheCard) == false)
                    nameOfTheCard = nameOfTheCard[nameOfTheCard.Length - 1] + "";
                indexOfTheCard = int.Parse(nameOfTheCard);

                if ((sender as Image).Visibility == Visibility.Visible &&
                    !ViewModel.CardsTurned.Contains(indexOfTheCard))
                {
                    if (ViewModel.CardsTurned.Count == 0) // in momentul in care nu am in carti intoarse si intorc una 
                    {
                        ViewModel.CardsTurned.Add(indexOfTheCard);
                        ViewModel.Cards[indexOfTheCard].ChangeSideOfCard();
                        ViewModel.OnPropertyChanged("Cards");
                    }
                    else if (ViewModel.CardsTurned.Count == 1) // daca deja am o carte intoarsa
                    {
                        ViewModel.CardsTurned.Add(indexOfTheCard);
                        ViewModel.Cards[indexOfTheCard].ChangeSideOfCard();
                        ViewModel.OnPropertyChanged("Cards");

                        int oldIndex = ViewModel.CardsTurned[0];
                        if (ViewModel.Cards[oldIndex].CurrentImageCard
                            .Equals(ViewModel.Cards[indexOfTheCard].CurrentImageCard)) // daca am nimerit pereche
                        {
                            Wait(5000);
                            ViewModel.Cards[oldIndex].ChangeSideOfCard();
                            ViewModel.Cards[indexOfTheCard].ChangeSideOfCard();
                            ViewModel.Cards[oldIndex].Visibility = false;
                            ViewModel.Cards[indexOfTheCard].Visibility = false;

                            gameGrid.Children[oldIndex].Visibility = Visibility.Hidden;
                            gameGrid.Children[indexOfTheCard].Visibility = Visibility.Hidden;

                            if (ViewModel.Win()) // daca toate cartonasele au fost intoarse
                            {
                                ViewModel.Level++;
                                ViewModel.Score += 300;

                                if (ViewModel.Level == 2) // daca am trecut level 2
                                {
                                    //LevelLabel.Visibility = Visibility.Hidden;
                                    ViewModel.DispatcherTimer.Stop();
                                    ViewModel.State = StateOfGame.GameOver;

                                    MessageBox.Show("You won!", "Message", MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);
                                }
                                else
                                {
                                    ViewModel.GenerateCards();
                                    for (int i = 0; i < ViewModel.NumberPairs; i++)
                                        gameGrid.Children[i].Visibility = Visibility.Visible;
                                }
                            }
                        }
                        else // daca nu am gasit pereche
                        {
                            Wait(5000);
                            ViewModel.Cards[oldIndex].ChangeSideOfCard();
                            ViewModel.Cards[indexOfTheCard].ChangeSideOfCard();
                            ViewModel.OnPropertyChanged("Cards");
                        }

                        ViewModel.CardsTurned.Clear();
                    }
                }
            }
        }

        private void Wait(long milliseconds)
        {
            long time = DateTime.Now.AddTicks(milliseconds * 1000).Ticks;
            while (DateTime.Now.Ticks < time)
            {
                Grid g = new Grid();
                g.Dispatcher.Invoke(DispatcherPriority.Background,
                    (DispatcherOperationCallback) delegate { return null; }, null);
            }
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.State == StateOfGame.GameOver)
            {
                // TimeWindow timeWindow = new TimeWindow((this.DataContext as PairGameVM));
                //timeWindow.Show();
            }
            else
            {
                MessageBox.Show("Playing in progress!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}