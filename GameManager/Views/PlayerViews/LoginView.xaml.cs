using GameManager.ViewModels.PlayerViewModels;
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

namespace GameManager.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel ViewModel
        {
            get
            {
                return DataContext as LoginViewModel;
            }
        }

        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoginEvent += ViewModel_LoginEvent;
        }

        private void ViewModel_LoginEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}
