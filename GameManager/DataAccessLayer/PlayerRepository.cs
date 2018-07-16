using System.Linq;
using GameManager.DbContext;
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

        public void Register(Player player)
        {
            using (GameContext db = new GameContext())
            {
                if (!db.Players.Any(p => p.Username == player.Username))
                {
                    db.Players.Add(player);

                    db.SaveChanges();
                }
            }
        }

        public int GetPlayerScore(int? playerId)
        {
            using (GameContext db = new GameContext())
            {
                return db.Players.Find(playerId)?.GameRecords.Sum(p => p.Score) ?? 0;
            }
        }
    }
}