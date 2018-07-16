using System.Data.Entity;
using GameManager.DbContext;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.DataAccessLayer
{
    public class GameRecordRepository
    {
        public void Add(GameRecord gameRecord)
        {
            using (GameContext db = new GameContext())
            {
                db.GameRecords.Add(gameRecord);
                db.Entry(gameRecord.Player).State = EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}