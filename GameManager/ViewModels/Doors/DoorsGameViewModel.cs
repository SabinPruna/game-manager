﻿using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GameManager.ViewModels;

namespace GameManager.ViewModels.Doors
{
   public class DoorsGameViewModel : BaseViewModel
    {
        List<DoorsCardViewModel> _DoorsCards;

        public static bool flippegImage { get; set; }
        public static string lastParam;
        private GameRecordManager _gameRecordManager;



        public DoorsGameViewModel()
        {
            flippegImage = false;
            IsEnabled = true;
            Score = 0;
            _gameRecordManager = new GameRecordManager();
            NewGameCommand = new RelayCommand(param => StartGame((string)param));
            FlipCardCommand = new RelayCommand(param => DoorsCardFlipped((DoorsCardViewModel)param));
        }

                
        private List<DoorsCardViewModel> doorsCards;
        private bool isEnabled;

        public List<DoorsCardViewModel> DoorsCards
        {
            get { return doorsCards; }
            set { SetProperty(ref doorsCards, value); }
        }

        public RelayCommand DoorsGameCommand { get; }
        public RelayCommand FlipCardCommand { get; }
        public string backImage { get; private set; }
        public string frontImage { get; private set; }

        public void ResetScore()
        {
            Score = 0;
        }
        public void ResetGame()
        {
            DoorsCards = new List<DoorsCardViewModel>();
        }

        private int score;
       
        public int Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }


        public void StartGame(string param)
        {
            int nrcartiBune = 0;
            int nrcartiRele = 0;
            lastParam = param;
            flippegImage = false;
            if (param == "Easy")
            {
                nrcartiBune = 2;
                nrcartiRele = 1;
                
            }
            if (param == "Medium")
            {
                nrcartiBune = 4;
                nrcartiRele = 2; ;
            }
            if (param == "Hard")
            {
                nrcartiBune = 6;
                nrcartiRele = 3;
            }

            IsEnabled = false;
            List<DoorsCardViewModel> doorsCards = new List<DoorsCardViewModel>();
            for (int i = 0; i < nrcartiBune; i++)
            {
                DoorsCardViewModel dcvm = new DoorsCardViewModel()
                {
                    Visibility = false,
                    BackImage = $"../../Images/Doors/door.jpg",
                    FrontImage = $"../../Images/Doors/door4.jpg"

                };
                doorsCards.Add(dcvm);
            }
            for (int j = 0; j < nrcartiRele; j++)
            {
                DoorsCardViewModel dcvm3 = new DoorsCardViewModel()
                {
                    Visibility = false,
                    BackImage = $"../../Images/Doors/door.jpg",
                    FrontImage = $"../../Images/Doors/blackcat.jpg"
                };
                doorsCards.Add(dcvm3);
            }
            Shuffle(doorsCards);
            DoorsCards = doorsCards;
        }

                
        private void Shuffle(List<DoorsCardViewModel> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0,n));
                n--;
                DoorsCardViewModel value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void DoorsCardFlipped(DoorsCardViewModel card)
        {

            if (flippegImage == false)
            {
                card.Visibility = true;

            int nrflipped = doorsCards.Count(c => c.Visibility && !c.Hidden);

            //int nrhidden = doorsCards.Count(c => c.Hidden);
                if (DoorsCards[DoorsCards.IndexOf(card)].FrontImage == $"../../Images/Doors/door4.jpg")
                {
                    MessageBox.Show("You Won! Try next step!", "Message", MessageBoxButton.OK,
                                             MessageBoxImage.Exclamation);
                    flippegImage = true;
                    StartGame(lastParam);
                    Score += 10;
                }
                else
              if (DoorsCards[DoorsCards.IndexOf(card)].FrontImage == $"../../Images/Doors/blackcat.jpg")
                {
                    MessageBox.Show("You Lost!", "Message", MessageBoxButton.OK,
                                           MessageBoxImage.Exclamation);
                    flippegImage = true;
                    GameRecord gameRecord = new GameRecord
                    {
                        Date = DateTime.Now,
                        Game = "DoorsGame",
                        Player = App.CurrentApp.MainViewModel.LoginViewModel.Player,
                        Score = Score
                    };
                    _gameRecordManager.Add(gameRecord);
                    App.CurrentApp.MainViewModel.Refresh();
                    ResetGame();
                    ResetScore();
                    
                }
            }
        }
        

        public ICommand NewGameCommand { get; }
       
    }
}
