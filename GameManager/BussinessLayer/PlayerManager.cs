using System;
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

        internal void AddMoney(int playerId, int money)
        {
            _playerRepository.AddMoney(playerId, money);
        }

        #endregion

        public Player Login(Player player)
        {
            return player != null ? _playerRepository.Login(player) : null;
        }

        public bool Register(Player player)
        {
            return _playerRepository.Register(player);
        }

        public int GetPlayerScore(int? playerId)
        {
            return null == playerId ? 0 : _playerRepository.GetPlayerScore(playerId);
        }

        public List<TopPlayersScoreboardRecord> GetTopPlayers()
        {
            return _playerRepository.GetTopPlayers();
        }

        public List<TopPlayersScoreboardRecord> GetTopPlayersByGameName(string gameName)
        {
            return _playerRepository.GetTopPlayersByGameName(gameName);
        }

        public List<GameRecord> GetPlayerGameRecords(string playerName)
        {
            List<GameRecord> gameRecords = _playerRepository.GetPlayerGameRecords(playerName);

            return gameRecords ?? new List<GameRecord>();
        }

        public Player Edit(int playerId, Player player)
        {
           return _playerRepository.Edit(playerId, player);
        }

        public void SetRating(int playerId, string gameName, int rating)
        {
            _playerRepository.SetRating(playerId, gameName, rating);
        }

        public double? GetRating(string gameName)
        {
            return _playerRepository.GetRating(gameName);
        }

        public int GetPlayerMoney(int? playerId)
        {
           return _playerRepository.GetPlayerMoney(playerId);
        }

    }
}