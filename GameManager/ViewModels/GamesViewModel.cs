using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.DoorsGame;
using GameManager.ViewModels.Doors;
using GameManager.ViewModels.Login;
using GameManager.ViewModels.Money;
using GameManager.ViewModels.Pairs;
using GameManager.ViewModels.Rating;
using GameManager.ViewModels.Scoreboard;
using GameManager.ViewModels.Snake;
using GameManager.ViewModels.TicTacToe;
using GameManager.Views.Login;
using GameManager.Views.Rating;
using GameManager.Views;

using GameManager.Views.Money;
using GameManager.Views.Pairs;
using GameManager.Views.Rating;
using GameManager.Views.Scoreboard;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _money;
        private double _numberStarsDoors;
        private double _numberStarsPair;
        private double _numberStarsSnake;
        private double _numberStarsTicTacToe;
        private int _score;
        private ImageSource _userProfilePicture;

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
            MoneyViewModel = new MoneyViewModel();

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

            AddMoneyCommand = new RelayCommand(param =>
            {
                MoneyView moneyView = new MoneyView();
                moneyView.ShowDialog();
            });

            StartGameTestingCommand = new RelayCommand(param => { StartGameTesting((string) param); });


            BuyItemCommand = new RelayCommand(param =>
            {
                if (int.Parse(param.ToString()) > Money)
                {
                    MessageBox.Show("You do not have enough to buy this game.\n Consider adding money to your balance.",
                        "Message", MessageBoxButton.OK);
                }
                else
                {
                    _playerManager.AddMoney(LoginViewModel.Player.Id, -int.Parse(param.ToString()));
                    Money = Money;
                }
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
        public MoneyViewModel MoneyViewModel { get; set; }

        public double NumberStarsPair
        {
            get => Math.Round(_playerManager.GetRating("PairGame"), 2);
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

        public double NumberStarsDoors
        {
            get => Math.Round(_playerManager.GetRating("DoorsGame"), 2);
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

        public double NumberStarsTicTacToe
        {
            get => Math.Round(_playerManager.GetRating("TicTacToe"), 2);
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

        public double NumberStarsSnake
        {
            get => Math.Round(_playerManager.GetRating("SnakeGame"), 2);
            set
            {
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

        public int Money
        {
            get => _playerManager.GetPlayerMoney(LoginViewModel.Player?.Id);
            set => SetProperty(ref _money, value);
        }

        public bool CanPlaySnake { get; set; }
        public bool CanPlayDoors { get; set; }
        public bool CanPlayTicTacToe { get; set; }

        #endregion


        #region Methods

        private void StartGameTesting(string gameName)
        {
            switch (gameName)
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
                    DoorsView dv = new DoorsView();
                    DoorsGameViewModel.ResetGame();
                    dv.ShowDialog();
                    break;
                case "SnakeGame":
                    SnakeView snake = new SnakeView();
                    snake.ShowDialog();
                    break;
            }
        }

        public void StartGame(string param)
        {
            switch (param)
            {
                case "PairGame":
                    PairGameView pairGameView = new PairGameView();
                    pairGameView.ShowDialog();
                    break;
                case "TicTacToe":
                    if (Score > 1500 || CanPlayTicTacToe)
                    {
                        TicTacToeView ticTacToeView = new TicTacToeView();
                        TicTacToeViewModel.newWindow();
                        ticTacToeView.ShowDialog();
                    }
                    else
                    {
                        CanPlayTicTacToe = AllowPlayerToPlayGame(1500, 100);
                    }

                    break;
                case "DoorsGame":
                    if (Score > 2500 || CanPlayDoors)
                    {
                        DoorsView dv = new DoorsView();
                        DoorsGameViewModel.ResetGame();
                        dv.ShowDialog();
                    }
                    else
                    {
                        CanPlayDoors = AllowPlayerToPlayGame(2500, 150);
                    }

                    break;
                case "SnakeGame":
                    if (Score > 3500 || CanPlaySnake)
                    {
                        SnakeView snake = new SnakeView();
                        snake.ShowDialog();
                    }
                    else
                    {
                        CanPlaySnake = AllowPlayerToPlayGame(3500, 200);
                    }

                    break;
            }
        }

        private bool AllowPlayerToPlayGame(int pointsRequired, int moneyRequired)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                $"You need at least {pointsRequired} points for this game.\n Would you like to pay ${moneyRequired} to unlock this game for this game session?",
                "Message", MessageBoxButton.YesNo,
                MessageBoxImage.Error);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes when Money < moneyRequired:
                    MessageBox.Show("You do not have enough money. Consider adding some to your balance.",
                        "Message", MessageBoxButton.OK);
                    break;
                case MessageBoxResult.Yes:
                    _playerManager.AddMoney(LoginViewModel.Player.Id, -moneyRequired);
                    Refresh();
                    return true;
            }

            return false;
        }

        public void Refresh()
        {
            Score = Score;
            Money = Money;
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
        public ICommand AddMoneyCommand { get; }
        public ICommand StartGameTestingCommand { get; }
        public ICommand BuyItemCommand { get; }

        #endregion
    }
}