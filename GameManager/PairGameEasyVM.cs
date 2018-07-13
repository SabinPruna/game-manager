using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameManager.Models;
namespace GameManager
{
    public enum StateOfGame
    {
        Running,
        GameOver
    }

    public class PairGameEasyVM : BaseVM
    {
        private ObservableCollection<User> listOfUsers;
        public Player Player { get; set; }
        //private List<Card> imagesGame;
        private string image;
        public int NumberPairs { get; set; }

        public List<List<string>> Elements { get; set; }

        public StateOfGame State { get; set; } // starea jocului
        public Card[] Cards { get; set; }
        public List<string> ImagesGame { get; set; }
        public List<int> CardsTurned { get; set; }
        public User CurrentUser { get; set; }
        public int DefaultTime { get; set; }
        public int Time { get; set; }

        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; LevelDisplay = "Level: " + level; }
        }

        private string levelDisplay;
        public string LevelDisplay
        {
            get { return levelDisplay; }
            set { levelDisplay = value; OnPropertyChanged("LevelDisplay"); }
        }

        private string timeDisplay;
        public string TimeDisplay
        {
            get
            {
                return timeDisplay;
            }
            set
            {
                timeDisplay = value;
                OnPropertyChanged("TimeDisplay");
            }
        }
        public DispatcherTimer DispatcherTimer { get; set; }

        public ObservableCollection<User> ListOfUsers
        {
            get
            {
                return listOfUsers;
            }

            set
            {
                listOfUsers = value;
                OnPropertyChanged("ListOfUsers");
            }
        }
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
     

        public Boolean Win()
        {
            int nr = 0;
            foreach (Card card in Cards)
            {
                if (card.Visibility == false)
                {
                    nr++;
                }

            }
            if (nr >= 15)
                return true;
            else
                return false;


        }
        public int Score { get; set; }
        public PairGameEasyVM()
        {
            DefaultTime = 200;
            State = StateOfGame.GameOver;
            ListOfUsers = new ObservableCollection<User>();
            Cards = new Card[16];
            ImagesGame = new List<string>();

            ImagesGame.Add("../Images/For MatchGame/4.jpg");
            ImagesGame.Add("../Images/For MatchGame/5.jpg");
            ImagesGame.Add("../Images/For MatchGame/6.jpg");
            ImagesGame.Add("../Images/For MatchGame/12.jpg");
            ImagesGame.Add("../Images/For MatchGame/8.jpg");
            ImagesGame.Add("../Images/For MatchGame/9.png");
            ImagesGame.Add("../Images/For MatchGame/10.jpg");
            ImagesGame.Add("../Images/For MatchGame/11.jpg");

            Player = new Player();
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
                Card card = new Card(GetRandomCard(cardImages), "/Images/2.jpg", true);
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
            else
            {
                return list.ElementAt(0);

            }
        }
    }

}
