using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.Views.Login;

namespace GameManager.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private Player _player;
        private string _username;

        #region Constructors

        public LoginViewModel()
        {
            _playerManager = new PlayerManager();

            LoginCommand = new RelayCommand(param =>
                {
                    Player player = new Player(Username, (param as PasswordBox)?.Password);
                    Player = _playerManager.Login(player);

                    if (null == Player)
                    {
                        MessageBox.Show("There are no players with given credentials in the database", "Login Error",
                            MessageBoxButton.OK);
                    }
                    else
                    {
                        LoginEvent?.Invoke(this, EventArgs.Empty);
                    }
                },
                param => !string.IsNullOrEmpty(Username) &&
                         !string.IsNullOrEmpty((param as PasswordBox)?.Password));

            ShowRegisterCommand = new RelayCommand(param =>
            {
                RegisterView registerView = new RegisterView();
                registerView.Show();
            });

            RegisterCommand = new RelayCommand(param =>
                {
                    Player player = new Player(Username, (param as PasswordBox)?.Password);

                    if (!_playerManager.Register(player))
                    {
                        MessageBox.Show("Username already in use.\n Please, pick another.", "Register Error",
                            MessageBoxButton.OK);
                    }
                    else
                    {
                        Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                    }
                }
            );

            QuitCommand = new RelayCommand(param =>
            {
                Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                Application.Current.Windows.OfType<EditView>().FirstOrDefault()?.Close();
            });

            EditCommand = new RelayCommand(param =>
            {
                Player player = new Player(Username, (param as PasswordBox)?.Password);
                Player = _playerManager.Edit(Player.Id, player);

                Application.Current.Windows.OfType<EditView>().FirstOrDefault()?.Close();
            },
                param => !string.IsNullOrEmpty(Username) &&
                         !string.IsNullOrEmpty((param as PasswordBox)?.Password));
        }

        #endregion

        #region  Properties

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public Player Player
        {
            get => _player;
            private set => SetProperty(ref _player, value);
        }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; }
        public ICommand ShowRegisterCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand QuitCommand { get; set; }
        public ICommand EditCommand { get; }


        #endregion

        #region Events

        public event EventHandler LoginEvent;

        #endregion
    }
}
