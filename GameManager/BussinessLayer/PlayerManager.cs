using System.Collections.Generic;
using GameManager.DataAccessLayer;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.BussinessLayer
{
    public class PlayerManager
    {
        private readonly PlayerRepository _playerRepository;

        #region Constructors

        public PlayerManager()
        {
            _playerRepository = new PlayerRepository();
        }

        #endregion

        public Player Login(Player player)
        {
            return player != null ? _playerRepository.Login(player) : null;
        }

        public void Register(Player player)
        {
            _playerRepository.Register(player);
        }

        public int GetPlayerScore(int? playerId)
        {
            return null == playerId ? 0 : _playerRepository.GetPlayerScore(playerId);
        }

        public List<ScoreboardRecord> GetTopPlayers()
        {
            return _playerRepository.GetTopPlayers();
        }
    }
}