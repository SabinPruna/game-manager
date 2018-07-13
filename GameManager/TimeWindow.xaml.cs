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
    /// Interaction logic for TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : Window
    {
        public TimeWindow(PairGameEasyVM G)
        {
            InitializeComponent();
            this.DataContext = G;
        }

        private void OkTime_Click(object sender, RoutedEventArgs e)
        {
            string s = textBox1.Text;
            int timeNumber;
            if (int.TryParse(s, out timeNumber) == true)
            {
                GameEasy g = new GameEasy((this.DataContext as PairGameEasyVM).Player);
                (this.DataContext as PairGameEasyVM).DefaultTime = Convert.ToInt32(textBox1.Text);
                (this.DataContext as PairGameEasyVM).OnPropertyChanged("DefaultTime");
                this.Close();

                // Game g = new Game((this.DataContext as GameVM).CurrentUser);
                // g.Show();

            }
            else
            {
                MessageBox.Show("You must enter a number!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
