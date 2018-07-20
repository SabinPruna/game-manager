using GameManager.DbContext;
using GameManager.Models.Entities;

namespace GameManager.DataAccessLayer
{
    public class GameRecordRepository
    {
        public void Add(GameRecord gameRecord)
        {
            using (GameContext db = new GameContext())
            {
                db.Players.Attach(gameRecord.Player);
                db.GameRecords.Add(gameRecord);

                db.SaveChanges();
            }
        }
    }
}