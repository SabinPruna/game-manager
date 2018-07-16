using System.Windows;
using GameManager.Models.Entities;
using GameManager.ViewModels;
using GameManager.ViewModels.Pairs;
using GameManager.Views.Pairs;

namespace GameManager.Views
{
    /// <summary>
    ///     Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GamesView : Window
    {
        #region Constructors

        public GamesView()
        {
            InitializeComponent();
        }

        public GamesView(Player player)
        {
            InitializeComponent();
            DataContext = new GamesViewModel(player);
        }

        #endregion

        private void BtnPairs_Click(object sender, RoutedEventArgs e)
        {
            LevelSelect levelSelect = new LevelSelect(this)
            {
                DataContext = new LevelSelectViewModel((DataContext as GamesViewModel)?.Player),
                Owner = this
            };

            levelSelect.ShowDialog();
        }

        private void BtnTicTacToe_Click(object sender, RoutedEventArgs e)
        {
            TicTacToeView ticTacToeView = new TicTacToeView();
            ticTacToeView.Show();
        }

        private void BtnDoors_Click(object sender, RoutedEventArgs e)
        {
            DoorsView doorsView = new DoorsView();
            doorsView.Show();
        }

        private void BtnSnake_Click(object sender, RoutedEventArgs e)
        {
            SnakeView snakeView = new SnakeView();
            snakeView.Show();
        }

        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow window = new ShopWindow {Owner = this};
            window.ShowDialog();
        }
    }
}