using System.Collections.Generic;
using GameManager.BussinessLayer;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.ViewModels.Scoreboard
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private List<ScoreboardRecord> _topPlayerRecords;

        #region Constructors

        public ScoreboardViewModel()
        {
            _playerManager = new PlayerManager();
            TopPlayerRecords = _playerManager.GetTopPlayers();
        }

        #endregion

        #region  Properties

        public List<ScoreboardRecord> TopPlayerRecords
        {
            get => _topPlayerRecords;
            set => SetProperty(ref _topPlayerRecords, value);
        }

        #endregion

        public void Refresh()
        {
            TopPlayerRecords = _playerManager.GetTopPlayers();
        }
    }
}