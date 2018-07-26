using GameManager.ViewModels.Pairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.Models.SerializeObject
{
    public class PairsSerialize
    {
        private List<CardViewModel> _cards;
        private int _currentTime;
        private int _gridSize;

        #region Properties

        public List<CardViewModel> Cards { get; set; }

        public int CurrentTime { get; set; }

        public int GridSize { get; set; }

        public int Score { get; set; }

        #endregion

        #region Constructors

        public PairsSerialize()
        {
            Cards = new List<CardViewModel>();
        }

        #endregion
    }
}
