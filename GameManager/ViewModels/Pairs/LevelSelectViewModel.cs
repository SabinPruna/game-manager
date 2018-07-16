using GameManager.Models.Entities;

namespace GameManager.ViewModels.Pairs
{
    public class LevelSelectViewModel
    {
        #region Constructors

        public LevelSelectViewModel(Player player)
        {
            Player = player;
        }

        public LevelSelectViewModel()
        {
        }

        #endregion

        #region  Properties

        public Player Player { get; set; }

        #endregion
    }
}