using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using GameManager.Models;
using GameManager.Models.Entities;
using GameManager.ViewModels;
using GameManager.ViewModels.Pairs;

namespace GameManager
{
    public class PairGameHardVM : BaseViewModel
    {
        //private List<Card> imagesGame;
        private string image;

        private int level;

        private string levelDisplay;
        private ObservableCollection<User> listOfUsers;

        private string timeDisplay;

        #region Constructors

        public PairGameHardVM()
        {
            DefaultTime = 200;
            State = StateOfGame.GameOver;
            ListOfUsers = new ObservableCollection<User>();
            Cards = new Card[64];
            ImagesGame = new List<string>();
            Player = new Player();
            ImagesGame = Enumerable.Range(1, 32).Select(i => $"../../Images/For MatchGame/{i}.jpg").ToList();


            GenerateCards();
            CardsTurned = new List<int>();

            OnPropertyChanged("Cards");
            OnPropertyChanged("Elements");

            CardsTurned = new List<int>();

            Time = DefaultTime;

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            Elements = new List<List<string>>();
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
        public User CurrentUser { get; set; }
        public int DefaultTime { get; set; }
        public int Time { get; set; }

        public int Level
        {
            get => level;
            set
            {
                level = value;
                LevelDisplay = "Level: " + level;
            }
        }

        public string LevelDisplay
        {
            get => levelDisplay;
            set
            {
                levelDisplay = value;
                OnPropertyChanged("LevelDisplay");
            }
        }

        public string TimeDisplay
        {
            get => timeDisplay;
            set
            {
                timeDisplay = value;
                OnPropertyChanged("TimeDisplay");
            }
        }

        public DispatcherTimer DispatcherTimer { get; set; }

        public ObservableCollection<User> ListOfUsers
        {
            get => listOfUsers;

            set
            {
                listOfUsers = value;
                OnPropertyChanged("ListOfUsers");
            }
        }

        public string Image
        {
            get => image;

            set
            {
                image = value;
                OnPropertyChanged("Image");
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
            if (nr >= 63)
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

            for (int i = 0; i < 64; i++)
            {
                Card card = new Card(GetRandomCard(cardImages), "/Images/For MatchGame/cardBack.jpg", true);
                cardImages.Remove(card.ImageUp);
                Cards[i] = card;
            }

            // List<string> lista = new List<string>();
            // Elements.Add(new List<string>());
            // lista.Add("-------");
            //  Elements.Add(lista);
            //  for (int i = 0; i < 16; i++)
            //  {
            //     lista.Add(Cards[i]);
            // }
            //  Elements.Add(lista);
            /* List<Card> lista = new List<Card>();
             int nr=0;
             for (int i = 0; i < 16; i++)
             {

                 Elements[i].Add(Cards[i]);
                /* if (nr % 4 == 0 && nr != 0)
                 {
                     nr = 0;
                     Elements.Add(new List<Card>());

                     lista.Clear();

                 }
                 else
                 {

                     lista.Add(Cards[i]);
                     nr++;
                 }

             }*/
            OnPropertyChanged("Elements");
            // OnPropertyChanged("Cards");
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
                //CurrentUser.PlayedGames++;
                //foreach (User user in ListOfUsers)
                //{
                //    if ((user.Name).Equals(CurrentUser.Name))
                //    {
                //        user.NumberWonGames = CurrentUser.NumberWonGames;
                //        user.PlayedGames = CurrentUser.PlayedGames;

                //        break;
                //    }
                //}
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