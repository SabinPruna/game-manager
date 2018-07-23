using GameManager.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.Models.Entities;
using System.Windows.Input;
using GameManager.Commands;

namespace GameManager.ViewModels
{
    public class RatingViewModel:BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private string _numberStars;
        private string _gameName;

        #region Constructors

        public RatingViewModel()
        {
            _playerManager = new PlayerManager();
            RateGameCommand = new RelayCommand(param =>
            {
                _playerManager.SetRating(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id, GameName.Substring(38), Int32.Parse(NumberStars.Substring(38)));
            });
        }

        #endregion

        #region Properties

        public string NumberStars
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
