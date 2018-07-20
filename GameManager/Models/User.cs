using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameManager.Annotations;

namespace GameManager.Models
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        private bool _hasGameSaved;

        #region Constructors

        public User() { }

        public User(string name, string image, bool hasGameSaved)
        {
            Name = name;
            Image = image;
            PlayedGames = 0;
            NumberWonGames = 0;
            HasGameSaved = false;
        }

        #endregion

        #region  Properties

        public bool HasGameSaved
        {
            get => _hasGameSaved;
            set
            {
                if (value == _hasGameSaved) return;
                _hasGameSaved = value;
                OnPropertyChanged();
            }
        }

        public int PlayedGames { get; set; }
        public int NumberWonGames { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}