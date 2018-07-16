using System.Windows;
using GameManager.ViewModels.Pairs;
using GameManager.Views.Pairs;

namespace GameManager
{
    public partial class TimeWindow : Window
    {
        #region Constructors

        public TimeWindow(PairGameEasyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #endregion

        private void OkTime_Click(object sender, RoutedEventArgs e)
        {
            string time = textBox1.Text;
            if (int.TryParse(time, out int timeNumber))
            {
                PairGameEasyView gameView = new PairGameEasyView((DataContext as PairGameEasyViewModel).Player);
                (DataContext as PairGameEasyViewModel).DefaultTime = timeNumber;
                (DataContext as PairGameEasyViewModel).OnPropertyChanged("DefaultTime");
                Close();
            }
            else
            {
                MessageBox.Show("You must enter a number!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}