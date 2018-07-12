using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.Annotations;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models;
using GameManager.Views;

namespace GameManager.ViewModels
{
    public class RegisterViewModel
    {
        private readonly PlayerManager _playerManager;

        public RegisterViewModel()
        {
            _playerManager = new PlayerManager();
            Player = new Player();

            RegisterCommand = new RelayCommand(param =>
            {
                Player.Password = (param as PasswordBox)?.Password;

                Player = new Player(Player.Username, Player.Password);

                _playerManager.Register(Player);

                LoginView loginView = new LoginView();
                Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                loginView.Show();
            });

            //SelectAvatarCommand = new RelayCommand(param => { Player.AvatarPath = FileExplorer.GetAvatarPath(); });

            QuitCommand = new RelayCommand(param =>
            {
                LoginView loginView = new LoginView();
                Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                loginView.Show();
            });
        }

        public Player Player { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand QuitCommand { get; set; }

        public ICommand SelectAvatarCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}