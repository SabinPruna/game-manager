using GameManager.ViewModels;
using GameManager.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GameManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentApp => Current as App;

        public GamesViewModel MainViewModel { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainViewModel = new GamesViewModel();

            MainWindow = new LoginView();
            MainWindow.ShowDialog();

            if (MainViewModel.LoginViewModel.Player != null)
            {
                MainWindow = new GamesView();
                MainWindow.ShowDialog();
            }

            Shutdown();
        }
    }
}
