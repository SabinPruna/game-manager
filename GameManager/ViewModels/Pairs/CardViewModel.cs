using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;
using GameManager.Models.Entities;
using GameManager.Models;
using GameManager.Views;

namespace GameManager.ViewModels.Pairs
{
    public class CardViewModel : BaseViewModel
    {
        private string _imageUp;
        private bool _visibility;
        private bool _hidden;
        private string _imageDown;

        #region Properties

        public bool Hidden
        {
            get { return _hidden; }
            set { SetProperty(ref _hidden, value); }
        }

        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                if (SetProperty(ref _visibility, value))
                {
                    OnPropertyChanged(nameof(CurrentImageCard));
                }
            }
        }
        
        public string ImageUp
        {
            get { return _imageUp; }
            set { SetProperty(ref _imageUp, value); }
        }

        public string ImageDown
        {
            get { return _imageDown; }
            set { SetProperty(ref _imageDown, value); }
        }
        
        public string CurrentImageCard
        {
            get
            {
                if (Visibility)
                {
                    return ImageUp;
                }
                else
                {
                    return ImageDown;
                }
            }
        }

        #endregion
    }
}
