using System.Windows;

namespace GameManager.Views.Login
{
    /// <summary>
    ///     Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView
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