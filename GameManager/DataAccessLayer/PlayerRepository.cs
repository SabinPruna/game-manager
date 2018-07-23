using System.Collections.Generic;
using System.Linq;
using GameManager.DbContext;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.DataAccessLayer
{
    public class PlayerRepository
    {
        public Player Login(Player player)
        {
            using (GameContext db = new GameContext())
            {
                return db.Players.FirstOrDefault(p => p.Username == player.Username);
            }
        }

        public bool Register(Player player)
        {
            using (GameContext db = new GameContext())
            {
                if (!db.Players.Any(p => p.Username == player.Username))
                {
                    db.Players.Add(player);

                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public int GetPlayerScore(int? playerId)
        {
            using (GameContext db = new GameContext())
            {
                return db.Players.Find(playerId)?.GameRecords.Sum(p => p.Score) ?? 0;
            }
        }

        public List<TopPlayersScoreboardRecord> GetTopPlayers()
        {
            using (GameContext db = new GameContext())
            {
                return db.Players.OrderByDescending(p => p.GameRecords.Sum(r => r.Score)).Take(10).ToList()
                    .Select(p => new TopPlayersScoreboardRecord {Username = p.Username, Score = p.GameRecords.Sum(r => r.Score)})
                    .ToList();
            }
        }

        public List<TopPlayersScoreboardRecord> GetTopPlayersByGameName(string gameName)
        {
            using (GameContext db = new GameContext())
            {
                return db.Players.OrderByDescending(p => p.GameRecords.Where(r => r.Game == gameName).Sum(r => r.Score)).Take(10).ToList()
                    .Select(p => new TopPlayersScoreboardRecord {Username = p.Username, Score = p.GameRecords.Where(r => r.Game == gameName).Sum(r => r.Score)})
                    .ToList();
            }
        }

        public List<GameRecord> GetPlayerGameRecords(string playerName)
        {
            using (GameContext gameContext = new GameContext())
            {
               return gameContext.Players.FirstOrDefault(p => p.Username == playerName)?.GameRecords.OrderByDescending(r => r.Score).ToList();
            }
        }

        public Player Edit(int playerId, Player player)
        {
            using (GameContext gameContext = new GameContext())
            {
                Player dbPlayer = gameContext.Players.Find(playerId);
                if (dbPlayer != null)
                {
                    dbPlayer.Username = player.Username;
                    dbPlayer.Password = player.Password;
                    dbPlayer.UserPicture = player.UserPicture;
                }

                gameContext.SaveChanges();

                return dbPlayer;
            }
        }
    }
}