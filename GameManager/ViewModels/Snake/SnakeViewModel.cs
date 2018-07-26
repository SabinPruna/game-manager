using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameManager.Models.Snake;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.BussinessLayer;

namespace GameManager.ViewModels.Snake
{
    public class SnakeViewModel:BaseViewModel
    {
        public Models.Snake.Snake Snake;
        public DispatcherTimer Timer;
        private Board _board;
        public Timer _timer;
        private int _movement;
        private bool _run;
        private int _points;
        private readonly GameRecordManager _gameRecordManager;

        #region Constructor

        public SnakeViewModel()
        {
            Width = Height = 15;

            Board = new ObservableCollection<int>();
            Snake = new Models.Snake.Snake(new Point(5, 2), 5, Direction.Right);
            Run = false;
            _gameRecordManager = new GameRecordManager();
            Movement = 300;

            _board = new Board(Width, Height);

            _board.drawPoint();
            _board.UpdateBoard(Snake);

            UpdateWindow();
        }

        #endregion

        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }

        public ObservableCollection<int> Board { get; set; }

        public int Movement
        {
            get { return _movement; }
            set { SetProperty(ref _movement, value); }
        }

        public bool Run
        {
            get { return _run; }
            set { SetProperty(ref _run, value); }
        }

        public int Points
        {
            get { return _points; }
            set { SetProperty(ref _points, value); }
        }

        #endregion

        #region Methods

        private void TimerInit()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(_movement);

            Timer.Tick += (s, e) =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(
                        delegate ()
                        {
                            MovingSpan();
                        }
                    )
                );
            };
        }

        private void MovingSpan()
        {
            Move(Snake.CurrentDirection);
        }

        private void UpdateWindow()
        {
            Board.Clear();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Board.Add(_board.Cells[i, j]);
                }
            }
            Points = _board.Points;
        }

        private void TimerRush(object state)
        {
            var interval = Timer.Interval;
            interval -= TimeSpan.FromMilliseconds(50);
            Timer.Interval = interval;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:

                    Snake.Move(Direction.Down);
                    break;

                case Direction.Up:

                    Snake.Move(Direction.Up);
                    break;

                case Direction.Left:

                    Snake.Move(Direction.Left);
                    break;

                case Direction.Right:

                    Snake.Move(Direction.Right);
                    break;
            }

            _board.UpdateBoard(Snake);
            UpdateWindow();
            if (_board.IsColision(Snake))
            {
                Timer.Stop();
                _timer.Dispose();
                MessageBox.Show("You Lost!","Message", MessageBoxButton.OK,
                                                 MessageBoxImage.Exclamation);

                if (Points != 0)
                {
                    GameRecord game = new GameRecord
                    {
                        Date = DateTime.Now,
                        Game = "SnakeGame",
                        Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                        Score = Points
                    };
                    _gameRecordManager.Add(game);
                    App.CurrentApp.MainViewModel.Refresh();
                }

            };
                Run = false;
            }
        

        #endregion

        #region Commands

        private ICommand _startButtonClickCommand { get; set; }
        public ICommand StartButtonClick
        {
            get
            {
                if (_startButtonClickCommand == null)
                {
                    _startButtonClickCommand = new RelayCommand(
                        o =>
                        {
                            Snake = new Models.Snake.Snake(new Point(5, 2), 5, Direction.Right);
                            _board = new Board(Width, Height);

                            _board.drawPoint();
                            _board.UpdateBoard(Snake);

                            UpdateWindow();
                            Run = true;
                            TimerInit();
                            Timer.Start();
                            _timer = new System.Threading.Timer(TimerRush, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
                        },
                        o =>
                        {
                            return !Run;
                        }
                    );
                }

                return _startButtonClickCommand;
            }
        }

        #endregion
    }
}


