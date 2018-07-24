using GameManager.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.Models.Entities;
using System.Windows.Input;
using GameManager.Commands;
using System.Windows;

namespace GameManager.ViewModels.Rating
{
    public class RatingViewModel:BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private int _numberStars;
        private string _gameName;

        #region Constructors

        public RatingViewModel()
        {
            _playerManager = new PlayerManager();
            RateGameCommand = new RelayCommand(param =>
            {
                _playerManager.SetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, GameName, NumberStars+1);
                MessageBox.Show("Thank you for your feedback!");
                App.CurrentApp.MainViewModel.Refresh();
            });
        }

        #endregion

        #region Properties

        public int NumberStars
        {
            get { return _numberStars; }
            set { SetProperty(ref _numberStars, value); }
        }

        public string GameName
        {
            get { return _gameName; }
            set { SetProperty(ref _gameName, value); }
        }

        #endregion

        #region Commands

        public ICommand RateGameCommand { get; }

        #endregion
    }
}
