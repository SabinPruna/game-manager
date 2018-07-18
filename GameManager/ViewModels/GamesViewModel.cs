using GameManager.BussinessLayer;
using GameManager.Models.Entities;
using GameManager.ViewModels.PlayerViewModels;
using GameManager.ViewModels.Pairs;
using System.Windows.Input;
using GameManager.Commands;
using GameManager.Views.Pairs;
using System.Windows;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private Player _player;
        private int _score;
        public PairGameViewModel PairGameViewModel { get; private set; }

        public LoginViewModel LoginViewModel { get; private set; }

        #region Constructors

        public GamesViewModel(Player player)
        {
            _player = player;
            _playerManager = new PlayerManager();
        }

        public GamesViewModel()
        {
            LoginViewModel = new LoginViewModel();
            PairGameViewModel = new PairGameViewModel();
            NewGameCommand = new RelayCommand(param => StartGame((string)param));

            _playerManager = new PlayerManager();
        }

        #endregion

        #region  Properties

        public Player Player
        {
            get => _player;
            set
            {
                if (Equals(value, _player)) return;
                _player = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get => _playerManager.GetPlayerScore(Player?.Id);
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public void StartGame(string param)
        {
            if (param == "PairGame")
            {
                PairGameView pairGameView = new PairGameView();
                pairGameView.DataContext= new PairGameViewModel();
                pairGameView.Show();
            }
            if (param == "TicTacToe")
            {
                MessageBox.Show("TicTacToe");
            }
            if (param == "DoorsGame")
            {
                MessageBox.Show("DoorsGame");
            }
            if(param== "SnakeGame")
            {
                MessageBox.Show("SnakeGame");
            }
        }

        public ICommand NewGameCommand { get; private set; }
        public object PairsGameView { get; private set; }
    }
}