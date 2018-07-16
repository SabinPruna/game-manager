using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using GameManager.Models;
using GameManager.Models.Entities;

namespace GameManager.ViewModels.Pairs
{
    public class PairGameEasyViewModel : BaseViewModel
    {
        private int _level;
        private string _levelDisplay;
        private ObservableCollection<User> _listOfUsers;

        #region Constructors

        public PairGameEasyViewModel(Player player) : this()
        {
            Player = player;
        }

        public PairGameEasyViewModel()
        {
            DefaultTime = 200;
            State = StateOfGame.GameOver;
            ListOfUsers = new ObservableCollection<User>();
            Cards = new Card[16];
            ImagesGame = Enumerable.Range(1, 8).Select(i => $"../../Images/For MatchGame/{i}.jpg").ToList();
            GenerateCards();

            CardsTurned = new List<int>();
            Elements = new List<List<string>>();

            OnPropertyChanged("Cards");
            OnPropertyChanged("Elements");

            Time = DefaultTime;
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        #endregion

        #region  Properties

        public Player Player { get; set; }
        public int NumberPairs { get; set; }

        public List<List<string>> Elements { get; set; }

        public StateOfGame State { get; set; } // starea jocului
        public Card[] Cards { get; set; }
        public List<string> ImagesGame { get; set; }
        public List<int> CardsTurned { get; set; }
        public int DefaultTime { get; set; }
        public int Time { get; set; }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                LevelDisplay = "Level: " + _level;
            }
        }

        public string LevelDisplay
        {
            get => _levelDisplay;
            set
            {
                _levelDisplay = value;
                OnPropertyChanged("LevelDisplay");
            }
        }

        public DispatcherTimer DispatcherTimer { get; set; }

        public ObservableCollection<User> ListOfUsers
        {
            get => _listOfUsers;

            set
            {
                _listOfUsers = value;
                OnPropertyChanged("ListOfUsers");
            }
        }

        public int Score { get; set; }

        #endregion

        public bool Win()
        {
            int nr = 0;
            foreach (Card card in Cards)
                if (card.Visibility == false)
                    nr++;
            if (nr >= 15)
                return true;
            return false;
        }

        public void GenerateCards()
        {
            List<string> cardImages = new List<string>();
            foreach (string card in ImagesGame)
            {
                cardImages.Add(card);
                cardImages.Add(card);
            }

            for (int i = 0; i < 16; i++)
            {
                Card card = new Card(GetRandomCard(cardImages), "/Images/For MatchGame/cardBack.jpg", true);
                cardImages.Remove(card.ImageUp);
                Cards[i] = card;
            }

            OnPropertyChanged("Elements");
        }

        public void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Time = Time - 1;
            DefaultTime = Time;
            OnPropertyChanged("DefaultTime");
            if (Time == 0)
            {
                DispatcherTimer.Stop();
                MessageBox.Show("Time is up! You lost!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                State = StateOfGame.GameOver;

                State = StateOfGame.GameOver;
                OnPropertyChanged("ListOfUsers");
            }
        }

        public string GetRandomCard(List<string> list)
        {
            if (list.Count != 1 && list.Count != 0)
            {
                Random rand = new Random();

                int index = rand.Next() % list.Count;
                return list.ElementAt(index);
            }

            return list.ElementAt(0);
        }
    }
}