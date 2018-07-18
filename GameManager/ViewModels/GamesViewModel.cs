using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.DoorsGame;
using GameManager.ViewModels.PlayerViewModels;
using GameManager.ViewModels.Scoreboard;
using GameManager.Views.Pairs;
using GameManager.Views.Scoreboard;
using GameManager.ViewModels.TicTacToe;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _score;

        public LoginViewModel LoginViewModel { get; private set; }
        public TicTacToeViewModel TicTacToeViewModel { get; private set; }

        #region Constructors

        public GamesViewModel()
        {
            LoginViewModel = new LoginViewModel();
            ScoreboardViewModel = new ScoreboardViewModel();
            TicTacToeViewModel = new TicTacToeViewModel();
            _playerManager = new PlayerManager();

            PairsGameCommand = new RelayCommand(param =>
            {
                LevelSelectView levelSelectView = new LevelSelectView();
                levelSelectView.ShowDialog();
            });

            DoorsGameCommand = new RelayCommand(param =>
            {
                DoorsVIew doorsView = new DoorsVIew();
                doorsView.ShowDialog();
            });

            TicTacToeGameCommand = new RelayCommand(param =>
            {
                TicTacToeView ticTacToeView = new TicTacToeView();
                TicTacToeViewModel.newWindow();
                ticTacToeView.ShowDialog();
            });
            SnakeGameCommand = new RelayCommand(param =>
            {
                SnakeView snakeView = new SnakeView();
                snakeView.ShowDialog();
            });

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
    }
}
