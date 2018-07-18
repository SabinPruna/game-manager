using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.Views.Pairs;
using System.Windows.Threading;
using System.Windows;
using GameManager.Models.Entities;
using GameManager.BussinessLayer;

namespace GameManager.ViewModels.Pairs
{
    public class PairGameViewModel : BaseViewModel
    {
        #region Constructors

        public PairGameViewModel()
        {
            NewGameCommand = new RelayCommand(param => StartGame((string)param));
            FlipCardCommand = new RelayCommand(param => FlipCard((CardViewModel)param));
            _gameRecordManager = new GameRecordManager();
            CurrentTime = 200;
            Score = 0;
        }

        #endregion

        #region  Properties

        private int _gridSize;

        public int GridSize
        {
            get { return _gridSize; }
            set { SetProperty(ref _gridSize, value); }
        }

        private List<CardViewModel> _cards;

        public List<CardViewModel> Cards
        {
            get { return _cards; }
            set { SetProperty(ref _cards, value); }
        }

        private DispatcherTimer DispatcherTimer { get; set; }

        private int _currentTime;

        public int CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }

        private int Score { get; set; }

        private GameRecordManager _gameRecordManager;

        #endregion

        #region Methods

        public void StartGame(string param)
        {
            if (CurrentTime != 0)
            {
                if (param == "Easy")
                {
                    GridSize = 4;
                    Score = 100;
                }
                if (param == "Medium")
                {
                    GridSize = 6;
                    Score = 200;
                }
                if (param == "Hard")
                {
                    GridSize = 8;
                    Score = 300;
                }

                List<CardViewModel> cards = new List<CardViewModel>();
                for (int i = 0; i < GridSize * GridSize / 2; i++)
                {
                    CardViewModel cvm = new CardViewModel()
                    {
                        Visibility = false,
                        ImageDown = $"../../Images/For MatchGame/cardBack.jpg",
                        ImageUp = $"../../Images/For MatchGame/{i + 1}.jpg"

                    };
                    cards.Add(cvm);
                    CardViewModel cvm2 = new CardViewModel()
                    {
                        Visibility = false,
                        ImageDown = $"../../Images/For MatchGame/cardBack.jpg",
                        ImageUp = $"../../Images/For MatchGame/{i + 1}.jpg"
                    };
                    cards.Add(cvm2);
                }
                Shuffle(cards);
                Cards = cards;
                StartTimer();
            }
            else
                MessageBox.Show("Set the timer!");
        }

        private void Shuffle(List<CardViewModel> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                CardViewModel value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private async void FlipCard(CardViewModel card)
        {
            card.Visibility = true;

            int nrflipped = Cards.Count(c => c.Visibility && !c.Hidden);
            
            if (nrflipped == 2)
            {
                var visibleCards = Cards.Where(c => c.Visibility && !c.Hidden).ToList();
                int nrDistinctCards = visibleCards.Select(c => c.ImageUp).Distinct().Count();
                await Task.Delay(1000);
                if (nrDistinctCards == 1)
                {
                    foreach (var c in visibleCards)
                    {
                        c.Hidden = true;
                    }
                }
                else
                {
                    foreach (var c in visibleCards)
                    {
                        c.Visibility = false;
                    }
                }
            }
            int nrhidden = Cards.Count(c => c.Hidden);
            if (nrhidden == GridSize * GridSize && CurrentTime > 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("You Won!", "Message", MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation);

                GameRecord gameRecord = new GameRecord
                {
                    Date = DateTime.Now,
                    Game = "PairGame",
                    Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                    Score = Score
                };
                _gameRecordManager.Add(gameRecord);

            }
            else
            if (nrhidden != GridSize * GridSize && CurrentTime == 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("You Lost!", "Message", MessageBoxButton.OK,
                                       MessageBoxImage.Exclamation);
            }
        }

        private void StartTimer()
        {
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            DispatcherTimer.Tick += new EventHandler(timer_Tick);
            DispatcherTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTime -= 1;
            if (CurrentTime == 0)
            {
                DispatcherTimer.Stop();
            }
        }

        #endregion

        #region Commands

        public ICommand NewGameCommand { get; private set; }

        public ICommand FlipCardCommand { get; private set; }

        #endregion
    }
}


