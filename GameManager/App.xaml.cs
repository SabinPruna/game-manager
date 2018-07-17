using System.Windows;
using GameManager.ViewModels;
using GameManager.Views;

namespace GameManager
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region  Properties

        public static App CurrentApp => Current as App;

        public GamesViewModel MainViewModel { get; private set; }

        #endregion

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