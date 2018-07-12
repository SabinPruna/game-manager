using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GameManager.Annotations;

namespace GameManager.Models
{
    public class Player : INotifyPropertyChanged
    {
        private int _id;
        private string _username;
        private string _password;
        private int _points;
        private List<GameRecord> _gameRecords;

        public Player(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Player()
        {
        }

        [Key]
        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            } 
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public int Points
        {
            get => _points;
            set
            {
                if (value == _points) return;
                _points = value;
                OnPropertyChanged();
            }
        }

        public virtual List<GameRecord> GameRecords
        {
            get => _gameRecords;
            set
            {
                if (Equals(value, _gameRecords)) return;
                _gameRecords = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}