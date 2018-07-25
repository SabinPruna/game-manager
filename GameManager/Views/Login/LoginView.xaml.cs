using System;
using System.Windows;
using GameManager.ViewModels.Login;
using MahApps.Metro.Controls;

namespace GameManager.Views.Login
{
    /// <summary>
    ///     Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        #region Constructors

        public LoginView()
        {
            InitializeComponent();
        }

        #endregion

        #region  Properties

        private LoginViewModel ViewModel => DataContext as LoginViewModel;

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoginEvent += ViewModel_LoginEvent;
        }

        private void ViewModel_LoginEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}