using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.Views.Login;
using Microsoft.Win32;

namespace GameManager.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private ImageSource _avatarPath;
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
                        AvatarPath = ByteToImage(Player.UserPicture);
                        LoginEvent?.Invoke(this, EventArgs.Empty);
                        App.CurrentApp.MainViewModel.Refresh();
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
                Player player = new Player(Username, (param as PasswordBox)?.Password)
                {
                    UserPicture = GetJpgFromImageControl(AvatarPath)
                };

                if (!_playerManager.Register(player))
                {
                    MessageBox.Show("Username already in use.\n Please, pick another.", "Register Error",
                        MessageBoxButton.OK);
                }
                else
                {
                    Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                }
            });

            QuitCommand = new RelayCommand(param =>
            {
                Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
                Application.Current.Windows.OfType<EditView>().FirstOrDefault()?.Close();
                AvatarPath = ByteToImage(Player.UserPicture);
            });

            EditCommand = new RelayCommand(param =>
                {
                    Player player =
                        new Player(Username, (param as PasswordBox)?.Password)
                        {
                            UserPicture = GetJpgFromImageControl(AvatarPath)
                        };
                    Player = _playerManager.Edit(Player.Id, player);

                    App.CurrentApp.MainViewModel.Refresh();

                    Application.Current.Windows.OfType<EditView>().FirstOrDefault()?.Close();
                    AvatarPath = ByteToImage(Player.UserPicture);
                },
                param => !string.IsNullOrEmpty(Username) &&
                         !string.IsNullOrEmpty((param as PasswordBox)?.Password));

            SelectAvatarCommand = new RelayCommand(param => { AvatarPath = GetUserPicture(); });
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

        public ImageSource AvatarPath
        {
            get => _avatarPath ?? new BitmapImage(new Uri("../../Images/default.jpg", UriKind.Relative));
            set => SetProperty(ref _avatarPath, value);
        }

        public ImageSource UserProfilePicture => ByteToImage(Player.UserPicture) ??
                                                 new BitmapImage(new Uri("../../Images/default.jpg", UriKind.Relative));

        #endregion

        private ImageSource GetUserPicture()
        {
            OpenFileDialog file = new OpenFileDialog();
            bool? result = file.ShowDialog();

            if (File.Exists(file.FileName))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(file.FileName, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }

            return null;
        }

        #region Events

        public event EventHandler LoginEvent;

        #endregion


        public byte[] GetJpgFromImageControl(ImageSource imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg;

            return imgSrc;
        }

        #region Commands

        public ICommand LoginCommand { get; }
        public ICommand ShowRegisterCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand QuitCommand { get; set; }
        public ICommand EditCommand { get; }
        public ICommand SelectAvatarCommand { get; }

        #endregion
    }
}