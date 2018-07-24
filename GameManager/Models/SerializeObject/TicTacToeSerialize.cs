using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.Models.SerializeObject
{
    public class TicTacToeSerialize
    {
        private List<CardTicTacToe> cards;
        public int win { get; set; }
        public int numberOcupatedSpaces { get; set; }
        public List<CardTicTacToe> Cards{ get; set; }
        public TicTacToeSerialize()
        {
            Cards = new List<CardTicTacToe>();
        }
    }
}
