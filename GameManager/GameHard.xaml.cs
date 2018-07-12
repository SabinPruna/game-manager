﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using System.Windows.Threading;
namespace GameManager
{
    /// <summary>
    /// Interaction logic for GameHard.xaml
    /// </summary>
    public partial class GameHard : Window
    {
        public GameHard()
        {

            InitializeComponent();
            // (this.DataContext as GameVM).ListOfUsers = new ObservableCollection<User>();
            // (this.DataContext as GameVM).CurrentUser = currentUser;

        }



        private void exit_Click(object sender, RoutedEventArgs e)
        {
            GamesView gamesView = new GamesView();
            gamesView.Show();
            this.Close();

        }

        /*    private void Statistics_Click(object sender, RoutedEventArgs e)
            {

                StatisticsWindow statistic = new StatisticsWindow((this.DataContext as GameVM).ListOfUsers);

                statistic.Show();
            }
            */


        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if ((this.DataContext as PairGameHardVM).DefaultTime == 0 && (this.DataContext as PairGameHardVM).Time == 0)
            {
                (this.DataContext as PairGameHardVM).DefaultTime = 200;
                (this.DataContext as PairGameHardVM).OnPropertyChanged("DefaultTime");
                (this.DataContext as PairGameHardVM).Time = 200;
            }

            if ((this.DataContext as PairGameHardVM).State == StateOfGame.GameOver)
            {
                (this.DataContext as PairGameHardVM).DispatcherTimer.Start();
                for (int i = 0; i < 64; i++)
                {
                    gameGrid.Children[i].Visibility = Visibility.Visible;
                }
                TimeLabel.Visibility = Visibility.Visible;
                //LevelLabel.Visibility = Visibility.Visible;

                (this.DataContext as PairGameHardVM).Level = 1;
                (this.DataContext as PairGameHardVM).State = StateOfGame.Running;
                (this.DataContext as PairGameHardVM).GenerateCards();
                (this.DataContext as PairGameHardVM).Time = (this.DataContext as PairGameHardVM).DefaultTime;

            }
            else
            {
                MessageBox.Show("You are still in the process of playing", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        //private void OpenGame_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((this.DataContext as PairGameVM).State == StateOfGame.GameOver)
        //    {

        //        if ((this.DataContext as PairGameVM).CurrentUser.HasGameSaved)
        //        {
        //            string currentUserName = (this.DataContext as PairGameVM).CurrentUser.Name;
        //            string file = currentUserName + " .xml";
        //            PairGameVM gameSaved = Serialize.DeserializeObject<PairGameVM>(file);

        //            this.DataContext = gameSaved;
        //            for (int i = 0; i < 16; i++)
        //            {
        //                if (gameSaved.Cards[i].Visibility == true)
        //                    gameGrid.Children[i].Visibility = Visibility.Visible;

        //            }
        //            TimeLabel.Visibility = Visibility.Visible;
        //            LevelLabel.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            MessageBox.Show("You don't have any saved game!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Playing in progress", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }

        //}

        //private void SaveGame_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((this.DataContext as PairGameVM).State == StateOfGame.Running)
        //    {
        //        (this.DataContext as PairGameVM).DispatcherTimer.Stop();

        //        string currentUserName = (this.DataContext as PairGameVM).CurrentUser.Name;
        //        foreach (User user in (this.DataContext as PairGameVM).ListOfUsers)
        //        {
        //            if (user.Name.Equals(currentUserName))
        //            {
        //                user.HasGameSaved = true;
        //                (this.DataContext as PairGameVM).CurrentUser.HasGameSaved = true;
        //                break;
        //            }
        //        }

        //        Serialize.SerializeObject((this.DataContext as PairGameVM).ListOfUsers, "C:\\Users\\Isus te iubeste\\Documents\\Visual Studio 2010\\Projects\\Pairs\\Pairs\\bin\\Debug\\users.xml");
        //        string file = currentUserName + " .xml";
        //        Serialize.SerializeObject((this.DataContext as PairGameVM), file);
        //        MessageBox.Show("Game successfully saved", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    }
        //    else
        //    {
        //        MessageBox.Show("You can't save the game now", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}

        private void Card_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((this.DataContext as PairGameHardVM).Time > 0 && (this.DataContext as PairGameHardVM).State == StateOfGame.Running)
            {
                String nameOfTheCard = (sender as Image).Name.ToString();
                nameOfTheCard = nameOfTheCard.Substring(nameOfTheCard.Length - 2, 2);
                int indexOfTheCard;
                if (int.TryParse(nameOfTheCard, out indexOfTheCard) == false)
                {
                    nameOfTheCard = nameOfTheCard[nameOfTheCard.Length - 1] + "";
                }
                indexOfTheCard = int.Parse(nameOfTheCard);

                if ((sender as Image).Visibility == Visibility.Visible && !(this.DataContext as PairGameHardVM).CardsTurned.Contains(indexOfTheCard))
                {
                    if ((this.DataContext as PairGameHardVM).CardsTurned.Count == 0) // in momentul in care nu am in carti intoarse si intorc una 
                    {

                        (this.DataContext as PairGameHardVM).CardsTurned.Add(indexOfTheCard);
                        (this.DataContext as PairGameHardVM).Cards[indexOfTheCard].ChangeSideOfCard();
                        (this.DataContext as PairGameHardVM).OnPropertyChanged("Cards");

                    }
                    else if ((this.DataContext as PairGameHardVM).CardsTurned.Count == 1) // daca deja am o carte intoarsa
                    {
                        (this.DataContext as PairGameHardVM).CardsTurned.Add(indexOfTheCard);
                        (this.DataContext as PairGameHardVM).Cards[indexOfTheCard].ChangeSideOfCard();
                        (this.DataContext as PairGameHardVM).OnPropertyChanged("Cards");

                        int oldIndex = (this.DataContext as PairGameHardVM).CardsTurned[0];
                        if ((this.DataContext as PairGameHardVM).Cards[oldIndex].CurrentImageCard.Equals((this.DataContext as PairGameHardVM).Cards[indexOfTheCard].CurrentImageCard)) // daca am nimerit pereche
                        {
                            Wait(5000);
                            (this.DataContext as PairGameHardVM).Cards[oldIndex].ChangeSideOfCard();
                            (this.DataContext as PairGameHardVM).Cards[indexOfTheCard].ChangeSideOfCard();
                            (this.DataContext as PairGameHardVM).Cards[oldIndex].Visibility = false;
                            (this.DataContext as PairGameHardVM).Cards[indexOfTheCard].Visibility = false;

                            gameGrid.Children[oldIndex].Visibility = Visibility.Hidden;
                            gameGrid.Children[indexOfTheCard].Visibility = Visibility.Hidden;

                            if ((this.DataContext as PairGameHardVM).Win()) // daca toate cartonasele au fost intoarse
                            {

                                (this.DataContext as PairGameHardVM).Level++;

                                if ((this.DataContext as PairGameHardVM).Level == 2) // daca am trecut level 2
                                {
                                    //LevelLabel.Visibility = Visibility.Hidden;
                                    (this.DataContext as PairGameHardVM).DispatcherTimer.Stop();
                                    (this.DataContext as PairGameHardVM).State = StateOfGame.GameOver;
                                    //(this.DataContext as PairGameHardVM).CurrentUser.NumberWonGames++;
                                    //(this.DataContext as PairGameHardVM).CurrentUser.PlayedGames++;

                                    MessageBox.Show("You won!", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                    //foreach (User user in (this.DataContext as PairGameHardVM).ListOfUsers)
                                    //{
                                    //    if ((user.Name).Equals((this.DataContext as PairGameHardVM).CurrentUser.Name))
                                    //    {
                                    //        user.NumberWonGames = (this.DataContext as PairGameHardVM).CurrentUser.NumberWonGames;
                                    //        user.PlayedGames = (this.DataContext as PairGameHardVM).CurrentUser.PlayedGames;

                                    //        break;
                                    //    }
                                    //}
                                    //(this.DataContext as PairGameHardVM).OnPropertyChanged("ListOfUsers");
                                    //Serialize.SerializeObject((this.DataContext as PairGameHardVM).ListOfUsers, "C:\\Users\\Isus te iubeste\\Documents\\Visual Studio 2010\\Projects\\Pairs\\Pairs\\bin\\Debug\\users.xml");

                                }
                                else
                                {
                                    (this.DataContext as PairGameHardVM).GenerateCards();
                                    for (int i = 0; i < (this.DataContext as PairGameHardVM).NumberPairs; i++)
                                    {
                                        gameGrid.Children[i].Visibility = Visibility.Visible;
                                    }
                                }
                            }

                        }
                        else // daca nu am gasit pereche
                        {
                            Wait(5000);
                            (this.DataContext as PairGameHardVM).Cards[oldIndex].ChangeSideOfCard();
                            (this.DataContext as PairGameHardVM).Cards[indexOfTheCard].ChangeSideOfCard();
                            (this.DataContext as PairGameHardVM).OnPropertyChanged("Cards");
                        }

                        (this.DataContext as PairGameHardVM).CardsTurned.Clear();

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
                g.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate (object unused) { return null; }, null);
            }
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            if ((this.DataContext as PairGameHardVM).State == StateOfGame.GameOver)
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