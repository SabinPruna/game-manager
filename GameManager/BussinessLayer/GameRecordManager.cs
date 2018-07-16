using GameManager.DataAccessLayer;
using GameManager.Models.Entities;

namespace GameManager.BussinessLayer
{
    public class GameRecordManager
    {
        private readonly GameRecordRepository _gameRecordRepository;

        #region Constructors

        public GameRecordManager()
        {
            _gameRecordRepository = new GameRecordRepository();
        }

        #endregion

        public void Add(GameRecord gameRecord)
        {
            _gameRecordRepository.Add(gameRecord);
        }
    }
}