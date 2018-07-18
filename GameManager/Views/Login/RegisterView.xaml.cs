using System.Windows;

namespace GameManager.Views.Login
{
    /// <summary>
    ///     Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        #region Constructors

        public RegisterView()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
        }

        #endregion
    }
}