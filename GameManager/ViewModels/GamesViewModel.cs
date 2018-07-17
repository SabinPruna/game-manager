using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.DoorsGame;
using GameManager.ViewModels.PlayerViewModels;
using GameManager.Views.Pairs;

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
        }

        #endregion

        #region  Properties

        public LoginViewModel LoginViewModel { get; }

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

        #endregion
    }
}
