using System.Collections.Generic;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.ViewModels.Scoreboard
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private string _gameSearchCriteria;
        private List<GameRecord> _playerGameRecords;
        private string _playerSearchCriteria;
        private List<TopPlayersScoreboardRecord> _topPlayerRecords;
        private List<TopPlayersScoreboardRecord> _topPlayerRecordsByGameName;

        #region Constructors

        public ScoreboardViewModel()
        {
            _playerManager = new PlayerManager();
            TopPlayerRecords = _playerManager.GetTopPlayers();

            PlayerSearchCommand = new RelayCommand(param =>
            {
                PlayerGameRecords = _playerManager.GetPlayerGameRecords(PlayerSearchCriteria);
            });
            GameSearchCommand = new RelayCommand(param =>
            {
                TopPlayerRecordsByGameName = _playerManager.GetTopPlayersByGameName(GameSearchCriteria);
            });
        }

        #endregion

        #region  Properties

        public List<TopPlayersScoreboardRecord> TopPlayerRecords
        {
            get => _topPlayerRecords;
            set => SetProperty(ref _topPlayerRecords, value);
        }

        public List<GameRecord> PlayerGameRecords
        {
            get => _playerGameRecords;
            set => SetProperty(ref _playerGameRecords, value);
        }

        public List<TopPlayersScoreboardRecord> TopPlayerRecordsByGameName
        {
            get => _topPlayerRecordsByGameName;
            set => SetProperty(ref _topPlayerRecordsByGameName, value);
        }

        public string PlayerSearchCriteria
        {
            get => _playerSearchCriteria;
            set => SetProperty(ref _playerSearchCriteria, value);
        }

        public string GameSearchCriteria
        {
            get => (null == _gameSearchCriteria) ? _gameSearchCriteria : _gameSearchCriteria.Substring(38) ;
            set => SetProperty(ref _gameSearchCriteria, value);
        }

        public ICommand PlayerSearchCommand { get; }

        public ICommand GameSearchCommand { get; }

        #endregion

        public void Refresh()
        {
            TopPlayerRecords = _playerManager.GetTopPlayers();
        }
    }
}