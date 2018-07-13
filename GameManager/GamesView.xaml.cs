using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameManager
{
    /// <summary>
    /// Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GamesView : Window
    {
        public GamesView()
        {
            InitializeComponent();
        }

        private void btnPairs_Click(object sender, RoutedEventArgs e)
        {
            LevelSelect levelSelect = new LevelSelect();
            levelSelect.Show();
        }
        private void btnTicTacToe_Click(object sender, RoutedEventArgs e)
        {
            TicTacToeView ticTacToeView = new TicTacToeView();
            ticTacToeView.Show();
        }
        private void btnDoors_Click(object sender, RoutedEventArgs e)
        {
            DoorsView doorsView = new DoorsView();
            doorsView.Show();
        }
        private void btnSnake_Click(object sender, RoutedEventArgs e)
        {
            SnakeView snakeView = new SnakeView();
            snakeView.Show();
        }

        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow window = new ShopWindow();
            window.Owner = this;
            window.ShowDialog();
            
        }
    }
}
