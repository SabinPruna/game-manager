using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models;
using GameManager.Views;

namespace GameManager.ViewModels
{
    public class LoginViewModel
    {
        private readonly PlayerManager _playerManager;

        public LoginViewModel()
        {
            _playerManager = new PlayerManager();
            Player = new Player();

            LoginCommand = new RelayCommand(param =>
                {
                    Player.Password = (param as PasswordBox)?.Password;
                    //get registered account

                    Player player = _playerManager.Login(Player);

                    if (null != player)
                    {
                        GamesView mainWindow = new GamesView();
                        Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();

                        mainWindow.Show();
                    }
                },
                param => !string.IsNullOrEmpty(Player.Username) &&
                         !string.IsNullOrEmpty((param as PasswordBox)?.Password));

            RegisterCommand = new RelayCommand(param =>
            {
                RegisterView registerView = new RegisterView();
                Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();
                registerView.Show();
            });
        }

        public Player Player { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
    }
}