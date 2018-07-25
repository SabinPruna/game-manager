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
using Newtonsoft.Json;
using GameManager.Models.SerializeObject;

namespace GameManager.ViewModels.Pairs
{
    public class PairGameViewModel : BaseViewModel
    {
        private int _gridSize;
        private List<CardViewModel> _cards;
        private int _currentTime;
        private GameRecordManager _gameRecordManager;
        private string _output;
        private readonly PlayerManager _playerManager;

        #region Constructors

        public PairGameViewModel()
        {
            NewGameCommand = new RelayCommand(param => StartGame((string)param));
            FlipCardCommand = new RelayCommand(param => FlipCard((CardViewModel)param));
            ExitGameCommand = new RelayCommand(param => RefreshGame());
            SaveGameCommand = new RelayCommand(param => SaveGame());
            OpenGameCommand = new RelayCommand(param => OpenGame());
            _gameRecordManager = new GameRecordManager();
            CurrentTime = 200;
            Score = 0;
            _playerManager = new PlayerManager();
            DispatcherTimer = new DispatcherTimer();
        }

        #endregion

        #region  Properties

        public string Output
        {
            get { return _output; }
            set { SetProperty(ref _output, value); }
        }
        
        public int GridSize
        {
            get { return _gridSize; }
            set { SetProperty(ref _gridSize, value); }
        }
        
        public List<CardViewModel> Cards
        {
            get { return _cards; }
            set { SetProperty(ref _cards, value); }
        }

        public int CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }

        private DispatcherTimer DispatcherTimer { get; set; }

        private int Score { get; set; }

        #endregion

        #region Methods

        public void SaveGame()
        {
            PairsSerialize ps = new PairsSerialize();
            ps.Cards = Cards;
            ps.CurrentTime = CurrentTime;
            ps.GridSize = (int)Math.Sqrt(Cards.Count);
            Output =JsonConvert.SerializeObject(ps);
            _playerManager.SetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame", Output);
        }

        public void OpenGame()
        {
            Output = _playerManager.GetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame");
            PairGameViewModel deserializedObject = JsonConvert.DeserializeObject<PairGameViewModel>(Output);
            Cards = deserializedObject.Cards;
            CurrentTime = deserializedObject.CurrentTime;
            GridSize = deserializedObject.GridSize;
        }

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

                RefreshGame();

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
                await Task.Delay(200);
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
                MessageBox.Show("You Won!", "Winner", MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation);
                
                GameRecord gameRecord = new GameRecord
                {
                    Date = DateTime.Now,
                    Game = "PairGame",
                    Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                    Score = Score
                };
                _gameRecordManager.Add(gameRecord);
                App.CurrentApp.MainViewModel.Refresh();
                RefreshGame();
            }
            else
            if (nrhidden != GridSize * GridSize && CurrentTime == 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("You Lost!", "Loser", MessageBoxButton.OK,
                                       MessageBoxImage.Exclamation);
                RefreshGame();
            }
        }

        private void StartTimer()
        {
            DispatcherTimer.Tick += timer_Tick;
            DispatcherTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTime -= 1;
            if (CurrentTime == 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("You Lost!", "Message", MessageBoxButton.OK,
                                       MessageBoxImage.Exclamation);
                RefreshGame();
            }
        }

        private void RefreshGame()
        {
            DispatcherTimer.Stop();
            CurrentTime = 200;
            Score = 0;
            Cards = new List<CardViewModel>();
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        #endregion

        #region Commands

        public ICommand NewGameCommand { get; private set; }
        public ICommand FlipCardCommand { get; private set; }
        public ICommand ExitGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand OpenGameCommand { get; private set; }

        #endregion
    }
}


