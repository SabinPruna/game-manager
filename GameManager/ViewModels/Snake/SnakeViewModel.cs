using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameManager.Models;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.BussinessLayer;

namespace GameManager.ViewModels.Snake
{
    public class SnakeViewModel:BaseViewModel
    {
        #region Fields

        public int Width { get; set; }
        public int Height { get; set; }

        public Models.Snake Snake;
        public DispatcherTimer Timer;

        private Board _board;
        private Timer _timer;

        private int _movement;
        public int Movement
        {
            get { return _movement; }
            set
            {
                _movement = value;
                OnPropertyChanged("Movement");
            }
        }

        public ObservableCollection<int> Board { get; set; }

        private bool _run;
        public bool Run
        {
            get { return _run; }
            set
            {
                _run = value;
                OnPropertyChanged("Run");
            }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged("Points");
            }
        }
        private GameRecordManager _gameRecordManager;

        #endregion

        #region Constructor

        public SnakeViewModel()
        {
            Width = Height = 15;

            Board = new ObservableCollection<int>();
            Snake = new Models.Snake(new Point(5, 2), 5, Direction.Right);
            Run = false;
            _gameRecordManager = new GameRecordManager();
            Movement = 300;

            _board = new Board(Width, Height);

            _board.drawPoint();
            _board.UpdateBoard(Snake);

            UpdateWindow();
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Public Methods

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
                MessageBox.Show("You lost!");

                GameRecord game = new GameRecord
                {
                    Date = DateTime.Now,
                    Game = "Snake",
                    Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                    Score = Points
                };
                _gameRecordManager.Add(game);
                App.CurrentApp.MainViewModel.Refresh();

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
                            Snake = new Models.Snake(new Point(5, 2), 5, Direction.Right);
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


