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
        public List<CardViewModel> Cards
        {
            get;
            set;
        }

        public int CurrentTime
        {
            get;
            set;
        }
        public PairsSerialize()
        {
            Cards = new List<CardViewModel>();
        }
    }
}
