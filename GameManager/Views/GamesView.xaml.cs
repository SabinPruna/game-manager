using System.Windows;
using GameManager.Models.Entities;
using GameManager.ViewModels;
using GameManager.ViewModels.Pairs;
using GameManager.Views.Pairs;
using GameManager.DoorsGame;

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
    }
}