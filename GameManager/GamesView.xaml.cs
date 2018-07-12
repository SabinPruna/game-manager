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
        public GamesView(Models.Player player)
        {
            InitializeComponent();
            (this.DataContext as PairGameVM).Player = player;
            Username.Text = (this.DataContext as PairGameVM).Player.Username;


        }

        private void btnPairs_Click(object sender, RoutedEventArgs e)
        {
            LevelSelect levelSelect = new LevelSelect((this.DataContext as PairGameVM).Player, this);
             levelSelect.Owner = this;
             levelSelect.ShowDialog();
            
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
    }
}
