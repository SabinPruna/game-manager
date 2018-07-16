using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameManager.Annotations;

namespace GameManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}