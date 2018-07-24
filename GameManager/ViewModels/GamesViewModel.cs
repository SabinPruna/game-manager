using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.ViewModels.Login;
using GameManager.ViewModels.Pairs;
using GameManager.ViewModels.Scoreboard;
using GameManager.ViewModels.Snake;
using GameManager.ViewModels.TicTacToe;
using GameManager.Views.Login;
using GameManager.Views;
using GameManager.Views.Pairs;
using GameManager.Views.Scoreboard;
using GameManager.ViewModels.Doors;
using GameManager.DoorsGame;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _score;
        private ImageSource _userProfilePicture;
        private int? _numberStarsPair;
        private int? _numberStarsDoors;
        private int? _numberStarsTicTacToe;
        private int? _numberStarsSnake;

        #region Constructors

        public GamesViewModel()
        {
            LoginViewModel = new LoginViewModel();
            ScoreboardViewModel = new ScoreboardViewModel();
            TicTacToeViewModel = new TicTacToeViewModel();
            PairGameViewModel = new PairGameViewModel();
            RatingViewModel = new RatingViewModel();
            SnakeViewModel = new SnakeViewModel();
            DoorsGameViewModel = new DoorsGameViewModel();
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

            RatingCommand = new RelayCommand(param =>
            {
                RatingView ratingView = new RatingView();
                ratingView.ShowDialog();
            });

            PlayerEditCommand = new RelayCommand(param =>
            {
                EditView editView = new EditView();
                editView.ShowDialog();
            });
        }

        #endregion

        #region  Properties

        public RatingViewModel RatingViewModel { get; }
        public PairGameViewModel PairGameViewModel { get; }
        public LoginViewModel LoginViewModel { get; }
        public TicTacToeViewModel TicTacToeViewModel { get; }
        public SnakeViewModel SnakeViewModel { get; }
        public DoorsGameViewModel DoorsGameViewModel { get; }
        public ScoreboardViewModel ScoreboardViewModel { get; }

        public int? NumberStarsPair
        {
            get => _playerManager.GetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "PairGame");
            set
            {
                if (value == _numberStarsPair)
                {
                    return;
                }

                _numberStarsPair = value;
                OnPropertyChanged();
            }
        }

        public int? NumberStarsDoors
        {
            get => _playerManager.GetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "DoorsGame");
            set
            {
                if (value == _numberStarsDoors)
                {
                    return;
                }

                _numberStarsDoors = value;
                OnPropertyChanged();
            }
        }

        public int? NumberStarsTicTacToe
        {
            get => _playerManager.GetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "TicTacToe");
            set
            {
                if (value == _numberStarsTicTacToe)
                {
                    return;
                }

                _numberStarsTicTacToe = value;
                OnPropertyChanged();
            }
        }

        public int? NumberStarsSnake
        {
            get => _playerManager.GetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, "SnakeGame");
            set {
                if (value == _numberStarsSnake)
                {
                    return;
                }

                _numberStarsSnake = value;
                OnPropertyChanged();
            }
        }

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

        #endregion

        #region Methods

        public void StartGame(string param)
        {
            switch (param)
            {
                case "PairGame":
                    PairGameView pairGameView = new PairGameView();
                    pairGameView.ShowDialog();
                    break;
                case "TicTacToe":
                    if (Score > 1500)
                    {
                        TicTacToeView ticTacToeView = new TicTacToeView();
                        TicTacToeViewModel.newWindow();
                        ticTacToeView.ShowDialog();
                    }
                    else
                        MessageBox.Show("You need at least 1500 points for this game", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "DoorsGame":
                    if (Score > 2500)
                    {
                        DoorsView dv = new DoorsView();
                        dv.ShowDialog();
                    }
                    else
                        MessageBox.Show("You need at least 2500 points for this game", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "SnakeGame":
                    if (Score > 3500)
                    {
                        SnakeView snake = new SnakeView();
                        snake.ShowDialog();
                    }
                    else
                        MessageBox.Show("You need at least 3500 points for this game", "Message", MessageBoxButton.OK, MessageBoxImage.Error);

                    break;
            }
        }

        public void Refresh()
        {
            Score = Score;
            UserProfilePicture = LoginViewModel.UserProfilePicture;
            NumberStarsPair = NumberStarsPair;
            NumberStarsDoors = NumberStarsDoors;
            NumberStarsTicTacToe = NumberStarsTicTacToe;
            NumberStarsSnake = NumberStarsSnake;
        }

        public ImageSource UserProfilePicture
        {
            get => _userProfilePicture;
            set => SetProperty(ref _userProfilePicture, value);
        }

        #endregion

        #region Commands

        public ICommand ShopCommand { get; }
        public ICommand ScoreboardCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand PlayerEditCommand { get; }
        public ICommand RatingCommand { get; }

        #endregion
    }
}