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
        public LevelSelect(Models.Player player)
        {
            InitializeComponent();
            (this.DataContext as LevelSelectVM).Player = player;
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            GameEasy gameEasy = new GameEasy((this.DataContext as LevelSelectVM).Player);
            gameEasy.Show();
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            GameMedium gameMedium = new GameMedium((this.DataContext as LevelSelectVM).Player);
            gameMedium.Show();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            GameHard gameHard = new GameHard((this.DataContext as LevelSelectVM).Player);
            gameHard.Show();
        }
    }
}
