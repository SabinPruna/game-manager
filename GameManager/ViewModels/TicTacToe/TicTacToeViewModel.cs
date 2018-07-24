﻿using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models;
using GameManager.Models.Entities;
using GameManager.Models.SerializeObject;
using GameManager.Views;
using Newtonsoft.Json;
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
        private string _output;
        #region Properties

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
        public string Output
        {
            get { return _output; }
            set { SetProperty(ref _output, value); }
        }

        #endregion

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
            TicTacToeCommand = new RelayCommand(param =>Logica((CardTicTacToe)param));

            SaveTicTacToeCommand = new RelayCommand(param => SaveGame());
            OpenTicTacToeCommand = new RelayCommand(param => OpenGame());
            NewGameCommand = new RelayCommand(param => NewGame());
        }

        #endregion

        #region Methods

        public void NewGame()
        {
            for (int i = 0; i < 9; i++)
            {
                Cards[i].Card = "";
            }
            win = 0;
            numberOcupatedSpaces = 0;
        }

        public void SaveGame()
        {
            TicTacToeSerialize ttts = new TicTacToeSerialize();
            ttts.Cards = Cards;
            ttts.numberOcupatedSpaces = numberOcupatedSpaces;
            ttts.win= win;
            Output = JsonConvert.SerializeObject(ttts);
        }

       public void OpenGame()
        {
            TicTacToeViewModel deserializedObject = JsonConvert.DeserializeObject<TicTacToeViewModel>(Output);
            deserializedObject.Cards.RemoveRange(0, 9);
            win = deserializedObject.win;
            numberOcupatedSpaces = deserializedObject.numberOcupatedSpaces;

            List<CardTicTacToe> list = new List<CardTicTacToe>();
            list = deserializedObject.Cards;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Card == "" && Cards[i].Card != "")
                    Cards[i].Card = "";
                if (list[i].Card != "" && Cards[i].Card == "")
                    Cards[i].Card = list[i].Card;
            }
        }

        private void Logica(CardTicTacToe card)
        {
            if (win != 0)
                return;
            Random rnd = new Random();
            if (!Cards[Cards.IndexOf(card)].Card.Equals(""))
            {
                if (numberOcupatedSpaces <= 8)
                    MessageBox.Show("Press another one");
            }
            else
            {
                int ok = 0;
                Cards[Cards.IndexOf(card)].Card = $"../../Images/For TicTacToe/cat.jpg";
                numberOcupatedSpaces++;
                if (Winner() != $"../../Images/For TicTacToe/cat.jpg")
                    if (numberOcupatedSpaces < 8)
                    {
                        if (win == 0)
                            do
                            {
                                int r = rnd.Next(9);
                                if (Cards[r].Card.Equals(""))
                                {
                                    Cards[r].Card = $"../../Images/For TicTacToe/mouse.jpg";
                                    ok = 1;
                                }
                                if (Winner() == $"../../Images/For TicTacToe/mouse.jpg")
                                {
                                    MessageBox.Show("You Lost!", "Message", MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation);
                                    win = 2;
                                    break;
                                }
                            } while (ok == 0);
                        numberOcupatedSpaces++;
                    }
                    else
                    {
                        MessageBox.Show("Drow!", "Message", MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation);
                        return;
                    }
                else
                {
                    MessageBox.Show("You Won!", "Message", MessageBoxButton.OK,
                                         MessageBoxImage.Exclamation);
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
            if (Winner().Equals("../../Images/For TicTacToe/cat.jpg"))
            {
                GameRecord game = new GameRecord()
                {
                    Date = DateTime.Now,
                    Game = "TicTacToe",
                    Score = 100,
                    Player = App.CurrentApp.MainViewModel.LoginViewModel.Player
                };
                _gameRecordManager.Add(game);
                App.CurrentApp.MainViewModel.Refresh();
            }
        }

        #endregion

        #region Commands

        public ICommand TicTacToeCommand { get; private set; }
        public ICommand SaveTicTacToeCommand { get; private set; }
        public ICommand OpenTicTacToeCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }


        #endregion
    }
}
