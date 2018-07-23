using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.DoorsGame;
using GameManager.ViewModels.PlayerViewModels;
using GameManager.ViewModels.Scoreboard;
using GameManager.Views.Pairs;
using GameManager.Views.Scoreboard;
using GameManager.ViewModels.TicTacToe;
using GameManager.ViewModels.Pairs;
using System.Windows;
using GameManager.ViewModels.Doors;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _score;
        public PairGameViewModel PairGameViewModel { get; private set; }
        public DoorsGameViewModel DoorsGameViewModel { get; }
        public LoginViewModel LoginViewModel { get; private set; }
        public TicTacToeViewModel TicTacToeViewModel { get; private set; }

        #region Constructors

        public GamesViewModel()
        {
            LoginViewModel = new LoginViewModel();
            ScoreboardViewModel = new ScoreboardViewModel();
            TicTacToeViewModel = new TicTacToeViewModel();
            PairGameViewModel = new PairGameViewModel();
            DoorsGameViewModel = new DoorsGameViewModel();
            _playerManager = new PlayerManager();


            NewGameCommand = new RelayCommand(param => StartGame((string)param));

            ShopCommand = new RelayCommand(param =>
            {
                ShopWindow shopWindow = new ShopWindow();
                shopWindow.ShowDialog();
            });
            

            ScoreboardCommand = new RelayCommand(param =>
            {
                ScoreboardViewModel.Refresh();
                ScoreboardView scoreboardView = new ScoreboardView();
                scoreboardView.ShowDialog();
            });
        }

        #endregion

        #region  Properties

        public ScoreboardViewModel ScoreboardViewModel { get; private set; }

        public int Score
        {
            get => _playerManager.GetPlayerScore(LoginViewModel.Player?.Id);
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        public ICommand PairsGameCommand { get; }
        public ICommand DoorsGameCommand { get; }
        public ICommand TicTacToeGameCommand { get; }
        public ICommand SnakeGameCommand { get; }
        public ICommand ShopCommand { get; }
        public ICommand ScoreboardCommand { get; }
        


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
                TicTacToeView ticTacToeView = new TicTacToeView();
                TicTacToeViewModel.newWindow();
                ticTacToeView.ShowDialog();
            }
            if (param == "DoorsGame")
            {
                DoorsView doorsView = new DoorsView();
                doorsView.DataContext = new DoorsGameViewModel();
                doorsView.ShowDialog();
            }
            if(param== "SnakeGame")
            {
                SnakeView snake = new SnakeView();
                snake.ShowDialog();
            }
        }

        public ICommand NewGameCommand { get; private set; }
        public object PairsGameView { get; private set; }
    }
}
