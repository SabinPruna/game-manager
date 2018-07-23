using System.Data.Entity;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.DbContext
{
    public class GameContext : System.Data.Entity.DbContext
    {
        #region  Properties

        public DbSet<Player> Players { get; set; }
        public DbSet<GameRecord> GameRecords { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        #endregion
    }
}