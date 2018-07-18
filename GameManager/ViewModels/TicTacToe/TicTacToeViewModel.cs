using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models;
using GameManager.Models.Entities;
using GameManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameManager.ViewModels.TicTacToe
{
    public class TicTacToeViewModel : BaseViewModel
    {
        private readonly GameRecordManager _gameRecordManager;
        private List<CardTicTacToe> cards;
        public int win { get; set; }
        public int numberOcupatedSpaces { get; set; }


        public List<CardTicTacToe> Cards
        {
            get
            {
                return cards;
            }
            set
            {
                cards = value;
                OnPropertyChanged("Cards");
            }
        }

        #region Constructors

        public TicTacToeViewModel()
        {
            Cards = new List<CardTicTacToe>();
            for (int i = 0; i < 9; i++)
            {
                CardTicTacToe card = new CardTicTacToe("");
                Cards.Add(card);
            }
            win = 0;
            numberOcupatedSpaces = 0;
            _gameRecordManager = new GameRecordManager();

            TicTacToeCommand = new RelayCommand(param =>
            {
                Logica((CardTicTacToe)param);
            });
        }

        #endregion

        private void Logica(CardTicTacToe card)
        {
            if (win != 0)
                return;
            Random rnd = new Random();
            if (!Cards[Cards.IndexOf(card)].Card.Equals(""))
            {
                MessageBox.Show("Press another one");
            }
            else
            {
                int ok = 0;
                Cards[Cards.IndexOf(card)].Card = "X";
                numberOcupatedSpaces++;
                if (Winner() != "X")
                    if (numberOcupatedSpaces < 8)
                    {
                        if (win == 0)
                            do
                            {
                                int r = rnd.Next(9);
                                if (Cards[r].Card.Equals(""))
                                {
                                    Cards[r].Card = "0";
                                    ok = 1;
                                }
                                if (Winner() == "0")
                                {
                                    MessageBox.Show("Player 0 Wins");
                                    win = 2;
                                    break;
                                }
                            } while (ok == 0);
                        numberOcupatedSpaces++;
                    }
                    else
                        return;
                else
                {
                    MessageBox.Show("Player X Wins");
                    win = 1;
                    IsXWinner();
                }
            }
        }
        

        public void newWindow()
        {
            for (int i = 0; i < 9; i++)
                Cards[i].Card = "";
            win = 0;
            numberOcupatedSpaces = 0;
        }

        public string Winner()
        {
            if (Cards[0].Card.Equals(Cards[1].Card) && Cards[0].Card.Equals(Cards[2].Card))
                return Cards[0].Card.ToString();
            else
                 if (Cards[0].Card.Equals(Cards[3].Card) && Cards[0].Card.Equals(Cards[6].Card))
                    return Cards[0].Card.ToString();
            else
                if (Cards[0].Card.Equals(Cards[4].Card) && Cards[0].Card.Equals(Cards[8].Card))
                return Cards[0].Card.ToString();
            else
                if (Cards[1].Card.Equals(Cards[4].Card) && Cards[7].Card.Equals(Cards[1].Card))
                return Cards[1].Card.ToString();
            else
                 if (Cards[2].Card.Equals(Cards[5].Card) && Cards[5].Card.Equals(Cards[8].Card))
                return Cards[2].Card.ToString();
            else
                if (Cards[2].Card.Equals(Cards[4].Card) && Cards[4].Card.Equals(Cards[6].Card))
                return Cards[2].Card.ToString();
            else
                 if (Cards[3].Card.Equals(Cards[5].Card) && Cards[5].Card.Equals(Cards[4].Card))
                return Cards[3].Card.ToString();
            else
                if (Cards[8].Card.Equals(Cards[7].Card) && Cards[7].Card.Equals(Cards[6].Card))
                return Cards[8].Card.ToString();
            return "not";
        }

        public void IsXWinner()
        {
            if (Winner().Equals("X"))
            {
                GameRecord game = new GameRecord()
                {
                    Date = DateTime.Now,
                    Game = "TicTacToe",
                    Score = 100,
                    Player = App.CurrentApp.MainViewModel.LoginViewModel.Player
                };
                _gameRecordManager.Add(game);
            }
        }


        public ICommand TicTacToeCommand { get; private set; }


    }
}
