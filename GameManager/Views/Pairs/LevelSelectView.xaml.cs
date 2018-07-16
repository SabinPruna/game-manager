using System.Windows;
using GameManager.ViewModels.Pairs;

namespace GameManager.Views.Pairs
{
    public partial class LevelSelect : Window
    {
        #region Constructors

        public LevelSelect(GamesView gamesView)
        {
            InitializeComponent();
            GamesWindow = gamesView;
        }

        #endregion

        #region  Properties

        public GamesView GamesWindow { get; set; }

        #endregion

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            PairGameEasyView gameEasy = new PairGameEasyView((DataContext as LevelSelectViewModel).Player)
            {
                DataContext = new PairGameEasyViewModel((DataContext as LevelSelectViewModel).Player)
            };

            gameEasy.Show();
            Close();
            GamesWindow.Close();
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            GameMediumView gameMedium = new GameMediumView((DataContext as LevelSelectViewModel).Player);
            gameMedium.Show();

            Close();
            GamesWindow.Close();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            GameHardView gameHard = new GameHardView((DataContext as LevelSelectViewModel).Player);
            gameHard.Show();

            Close();
            GamesWindow.Close();
        }
    }
}