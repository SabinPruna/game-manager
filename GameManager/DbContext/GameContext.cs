using System.Data.Entity;
using GameManager.Models;

namespace GameManager.DbContext
{
    public class GameContext : System.Data.Entity.DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<GameRecord> GameRecords { get; set; }
    }
}