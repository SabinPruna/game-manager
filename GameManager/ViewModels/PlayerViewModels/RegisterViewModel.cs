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
    public class RegisterViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private Player _player;

        #region Constructors

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

        #endregion

        #region  Properties

        public Player Player
        {
            get => _player;
            set
            {
                if (Equals(value, _player)) return;
                _player = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand QuitCommand { get; set; }

        public ICommand SelectAvatarCommand { get; set; }

        #endregion
    }
}