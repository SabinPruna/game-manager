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
        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private readonly PlayerManager _playerManager;

        #region Constructors

        public LoginViewModel()
        {
            _playerManager = new PlayerManager();

            LoginCommand = new RelayCommand(param =>
                {
                    var player = new Player(Username, (param as PasswordBox)?.Password);
                    Player = _playerManager.Login(player);

                    LoginEvent?.Invoke(this, EventArgs.Empty);
                },
                param => !string.IsNullOrEmpty(Username) &&
                         !string.IsNullOrEmpty((param as PasswordBox)?.Password));

            RegisterCommand = new RelayCommand(param =>
            {
                RegisterView registerView = new RegisterView();
                Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();
                registerView.Show();
            });
        }

        #endregion

        #region  Properties

        private Player _player;

        public Player Player
        {
            get { return _player; }
            private set { SetProperty(ref _player, value); }
        }


        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        #endregion

        #region Events

        public event EventHandler LoginEvent;

        #endregion
    }
}