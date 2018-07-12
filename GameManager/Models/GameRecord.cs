using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GameManager.Annotations;
using System.Data.Entity;

namespace GameManager.Models
{
    public class GameRecord : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _game;
        private int _id;
        private Player _player;
        private int _score;

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

        public string Game
        {
            get => _game;
            set
            {
                if (value == _game) return;
                _game = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value.Equals(_date)) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        public virtual Player Player
        {
            get => _player;
            set
            {
                if (Equals(value, _player)) return;
                _player = value;
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