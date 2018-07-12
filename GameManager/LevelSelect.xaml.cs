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
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
        public GamesView gamesWindow { get; set; }
        public LevelSelect(Models.Player player, GamesView gamesView)
        {
            InitializeComponent();
            (this.DataContext as LevelSelectVM).Player = player;
            gamesWindow = gamesView;

        }
        
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            GameEasy gameEasy = new GameEasy((this.DataContext as LevelSelectVM).Player);
            gameEasy.Show();
            this.Close();
            gamesWindow.Close();
            
        }
        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            GameMedium gameMedium = new GameMedium((this.DataContext as LevelSelectVM).Player);
            gameMedium.Show();
            this.Close();
            gamesWindow.Close();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            GameHard gameHard = new GameHard((this.DataContext as LevelSelectVM).Player);
            gameHard.Show();
            this.Close();
            gamesWindow.Close();
        }
    }
}
