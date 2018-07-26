using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameManager.ViewModels;
using GameManager.Models.SerializeObject;
using GameManager.Views;
using Newtonsoft.Json;
using System.Threading;

namespace GameManager.ViewModels.Doors
{
   public class DoorsGameViewModel : BaseViewModel
    {
        //List<DoorsCardViewModel> _DoorsCards;

        public static bool flippegImage { get; set; }
        public static string level;
        private GameRecordManager _gameRecordManager;
        private string _output;
        private readonly PlayerManager _playerManager;
        private int score;
        private bool _isEnabledOpen;
        private bool _isEnabledSave;

        #region Constructors

        public DoorsGameViewModel()
        {
            _playerManager = new PlayerManager();
            flippegImage = false;
            Score = 0;
            _gameRecordManager = new GameRecordManager();
            NewGameCommand = new RelayCommand(param => StartGame((string)param));
            DoorsCommand = new RelayCommand(param => ResetScore());

            SaveDoorsCommand = new RelayCommand(param => SaveGame());
            OpenDoorsCommand = new RelayCommand(param => OpenGame());
            FlipCardCommand = new RelayCommand(param => DoorsCardFlipped((DoorsCardViewModel)param));
        }

        #endregion

        #region Properties

        private List<DoorsCardViewModel> doorsCards;

        public List<DoorsCardViewModel> DoorsCards
        {
            get { return doorsCards; }
            set { SetProperty(ref doorsCards, value); }
        }

        public string backImage { get; private set; }

        public string frontImage { get; private set; }
        
        public string Output
        {
            get { return _output; }
            set { SetProperty(ref _output, value); }
        }

        public int Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
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

        public void StartGame(string param)
        {
            Output = _playerManager.GetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "DoorsGame");
            if (Output != "")
            {
                IsEnabledOpen = true;
            }
            else
            {
                IsEnabledOpen = false;
            }
            IsEnabledSave = true;


            int nrcartiBune = 0;
            int nrcartiRele = 0;
            
            level = param;
            flippegImage = false;
            if (param == "Easy")
            {
                nrcartiBune = 2;
                nrcartiRele = 1;
                
            }
            if (param == "Medium")
            {
                nrcartiBune = 4;
                nrcartiRele = 2; ;
            }
            if (param == "Hard")
            {
                nrcartiBune = 6;
                nrcartiRele = 3;
            }
            
            List<DoorsCardViewModel> doorsCards = new List<DoorsCardViewModel>();
            for (int i = 0; i < nrcartiBune; i++)
            {
                DoorsCardViewModel dcvm = new DoorsCardViewModel()
                {
                    Visibility = false,
                    BackImage = $"../../Images/Doors/door.jpg",
                    FrontImage = $"../../Images/Doors/door4.jpg"

                };
                doorsCards.Add(dcvm);
            }
            for (int j = 0; j < nrcartiRele; j++)
            {
                DoorsCardViewModel dcvm3 = new DoorsCardViewModel()
                {
                    Visibility = false,
                    BackImage = $"../../Images/Doors/door.jpg",
                    FrontImage = $"../../Images/Doors/blackcat.jpg"
                };
                doorsCards.Add(dcvm3);
            }
            Shuffle(doorsCards);
            DoorsCards = doorsCards;
            ResetScore();
        }

        private void Shuffle(List<DoorsCardViewModel> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0,n));
                n--;
                DoorsCardViewModel value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private async void DoorsCardFlipped(DoorsCardViewModel card)
        {

            if (flippegImage == false)
            {
                card.Visibility = true;

                int nrflipped = doorsCards.Count(c => c.Visibility && !c.Hidden);

                if (DoorsCards[DoorsCards.IndexOf(card)].FrontImage == $"../../Images/Doors/door4.jpg")
                {

                   // MessageBox.Show("You win! Try next door!");
                    await Task.Delay(200);
                    flippegImage = true;
                    int oldScore = Score;
                    StartGame(level);
                    Score = oldScore + 10;
                }
                else if (DoorsCards[DoorsCards.IndexOf(card)].FrontImage == $"../../Images/Doors/blackcat.jpg")
                {
                    MessageBox.Show("You Lost!", "Message", MessageBoxButton.OK,
                                           MessageBoxImage.Exclamation);
                    flippegImage = true;
                    GameRecord gameRecord = new GameRecord
                    {
                        Date = DateTime.Now,
                        Game = "DoorsGame",
                        Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                        Score = Score
                    };
                    _gameRecordManager.Add(gameRecord);
                    App.CurrentApp.MainViewModel.Refresh();
                    ResetGame();
                    ResetScore();

                }
            }
        }

        public void SaveGame()
        {
            DoorsSerialize dss = new DoorsSerialize();
            dss.level = level;
            dss.Score = Score;
            Output = JsonConvert.SerializeObject(dss);
            _playerManager.SetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "DoorsGame", Output);
            IsEnabledOpen = true;
        }

        public void OpenGame()
        {
            DoorsSerialize deserializedObject = JsonConvert.DeserializeObject<DoorsSerialize>(Output);
            level = deserializedObject.level;
            StartGame(level);
            Score = deserializedObject.Score;
            IsEnabledSave = true;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        public void ResetGame()
        {
            DoorsCards = new List<DoorsCardViewModel>();
            Output = _playerManager.GetGameState(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "DoorsGame");
            if (Output != "")
            {
                IsEnabledOpen = true;
            }
            else
            {
                IsEnabledOpen = false;
            }
            IsEnabledSave = false;
            ResetScore();
        }

        #endregion

        #region Commands

        public ICommand DoorsCommand { get; private set; }
        public ICommand SaveDoorsCommand { get; private set; }
        public ICommand OpenDoorsCommand { get; private set; }
        public ICommand NewGameCommand { get; }
        public RelayCommand DoorsGameCommand { get; }
        public RelayCommand FlipCardCommand { get; }

        #endregion

    }
}
