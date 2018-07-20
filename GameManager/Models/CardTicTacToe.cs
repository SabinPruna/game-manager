using GameManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.Models
{
    public class CardTicTacToe:BaseViewModel
    {
        private string card;
        
        #region Constructors

        public CardTicTacToe() { }

        public CardTicTacToe(string card)
        {
            Card = card;
        }

        #endregion

        #region Properties

        public string Card
        {
            get
            {
                return card;
            }
            set
            {
                //card = value;
                //OnPropertyChanged("Card");
                SetProperty(ref card, value);
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return card.ToString();
        }

        #endregion
    }
}
