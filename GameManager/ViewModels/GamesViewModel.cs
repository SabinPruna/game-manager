using System.Windows;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.ViewModels.Login;
using GameManager.ViewModels.Pairs;
using GameManager.ViewModels.Scoreboard;
using GameManager.ViewModels.Snake;
using GameManager.ViewModels.TicTacToe;
using GameManager.Views.Pairs;
using GameManager.Views.Scoreboard;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _score;

        #region Constructors

        public GamesViewModel()
        {
            LoginViewModel = new LoginViewModel();
            ScoreboardViewModel = new ScoreboardViewModel();
            TicTacToeViewModel = new TicTacToeViewModel();
            PairGameViewModel = new PairGameViewModel();
            SnakeViewModel = new SnakeViewModel();
            _playerManager = new PlayerManager();

            NewGameCommand = new RelayCommand(param => StartGame((string) param));

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

        public PairGameViewModel PairGameViewModel { get; }
        public LoginViewModel LoginViewModel { get; }
        public TicTacToeViewModel TicTacToeViewModel { get; }
        public SnakeViewModel SnakeViewModel { get; }

        public ScoreboardViewModel ScoreboardViewModel { get; }

        public int Score
        {
            get => _playerManager.GetPlayerScore(LoginViewModel.Player?.Id);
            set
            {
                if (value == _score)
                {
                    return;
                }

                _score = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShopCommand { get; }
        public ICommand ScoreboardCommand { get; }

        public ICommand NewGameCommand { get; }

        #endregion


        public void StartGame(string param)
        {
            switch (param)
            {
                case "PairGame":
                    PairGameView pairGameView = new PairGameView();
                    pairGameView.ShowDialog();
                    break;
                case "TicTacToe":
                    TicTacToeView ticTacToeView = new TicTacToeView();
                    TicTacToeViewModel.newWindow();
                    ticTacToeView.ShowDialog();
                    break;
                case "DoorsGame":
                    MessageBox.Show("DoorsGame");
                    break;
                case "SnakeGame":
                    SnakeView snake = new SnakeView();
                    snake.ShowDialog();
                    break;
            }
        }

        public void Refresh()
        {
            Score = Score;
        }
    }
}