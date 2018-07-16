using GameManager.BussinessLayer;
using GameManager.Models.Entities;

namespace GameManager.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly PlayerManager _playerManager;
        private Player _player;
        private int _score;

        #region Constructors

        public GamesViewModel(Player player)
        {
            _player = player;
            _playerManager = new PlayerManager();
        }

        public GamesViewModel()
        {
            _playerManager = new PlayerManager();
        }

        #endregion

        #region  Properties

        public Player Player
        {
            get => _player;
            set
            {
                if (Equals(value, _player)) return;
                _player = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get => _playerManager.GetPlayerScore(Player?.Id);
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}