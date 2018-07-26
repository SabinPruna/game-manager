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
        private int _time;
        private bool _isEnabledOpen;
        private bool _isEnabledSave;
        private int _score;

        #region Constructors

        public PairGameViewModel()
        {
            NewGameCommand = new RelayCommand(param => StartGame((string)param));
            FlipCardCommand = new RelayCommand(param => FlipCard((CardViewModel)param));
            ExitGameCommand = new RelayCommand(param => RefreshGame());
            SaveGameCommand = new RelayCommand(param => SaveGame());
            OpenGameCommand = new RelayCommand(param => OpenGame());
            _gameRecordManager = new GameRecordManager();
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

        public int Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        public DispatcherTimer DispatcherTimer { get; set; }

        public int Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value); }
        }

        public bool IsEnabledOpen
        {
            get { return _isEnabledOpen; }
            set { SetProperty(ref _isEnabledOpen, value); }
        }

        public bool IsEnabledSave
        {
            get { return _isEnabledSave; }
            set { SetProperty(ref _isEnabledSave, value); }
        }

        #endregion

        #region Methods

        public void SaveGame()
        {
            PairsSerialize ps = new PairsSerialize();
            ps.Cards = Cards;
            ps.CurrentTime = CurrentTime;
            ps.GridSize = (int)Math.Sqrt(Cards.Count);
            ps.Score = Score;
            Output =JsonConvert.SerializeObject(ps);
            _playerManager.SetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame", Output);
            IsEnabledOpen = true;
        }

        public void OpenGame()
        {
            DispatcherTimer.Stop();

            PairGameViewModel deserializedObject = JsonConvert.DeserializeObject<PairGameViewModel>(Output);
            Cards = deserializedObject.Cards;
            CurrentTime = deserializedObject.CurrentTime;
            GridSize = deserializedObject.GridSize;
            Score = deserializedObject.Score;
            IsEnabledSave = true;
            StartTimer();
        }

        public void StartGame(string param)
        {
            Score = 0;
            Output = _playerManager.GetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame");
            if (Output != "")
            {
                IsEnabledOpen = true;
            }
            else
            {
                IsEnabledOpen = false;
            }
            IsEnabledSave = true;

            if (param == "Easy")
            {
                GridSize = 4;
                Time = 100;
            }
            if (param == "Medium")
            {
                GridSize = 6;
                Time = 150;
            }
            if (param == "Hard")
            {
                GridSize = 8;
                Time = 200;
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
                    Score += 10;
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
                CurrentTime = 0;
                Score = 0;
            }
            else
            if (nrhidden != GridSize * GridSize && CurrentTime == 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("You Lost!", "Loser", MessageBoxButton.OK,
                                       MessageBoxImage.Exclamation);
                CurrentTime = 0;
                Score = 0;
            }
        }

        private void StartTimer()
        {
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
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
                Time = 0;
                RefreshGame();
            }
        }

        public void ResetGame()
        {
            Time = 0;
            Score = 0;
            Output = _playerManager.GetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame");
            if (Output != "")
            {
                IsEnabledOpen = true;
            }
            else
            {
                IsEnabledOpen = false;
            }
            IsEnabledSave = false;
            RefreshGame();
        }

        public void RefreshGame()
        {
            DispatcherTimer.Stop();
            CurrentTime = Time;
            Cards = new List<CardViewModel>();
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


