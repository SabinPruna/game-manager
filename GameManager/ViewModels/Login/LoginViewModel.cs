using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.Views;

namespace GameManager.ViewModels.PlayerViewModels
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

                    LoginEvent?.Invoke(this, EventArgs.Empty);
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

                    _playerManager.Register(player);

                    Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                }
            );

            QuitCommand = new RelayCommand(param =>
            {
                Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
            });
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

        public ICommand LoginCommand { get; }
        public ICommand ShowRegisterCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand QuitCommand { get; set; }

        #endregion


        #region Events

        public event EventHandler LoginEvent;

        #endregion
    }
}