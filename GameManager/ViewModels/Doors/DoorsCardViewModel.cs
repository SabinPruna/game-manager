using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.ViewModels.Doors
{
    public class DoorsCardViewModel : BaseViewModel
    {
        private string frontImage;
        private string backImage;

        bool DoorsCardFlipped;

        private bool _hidden;

        public bool Hidden
        {
            get { return _hidden; }
            set { SetProperty(ref _hidden, value); }
        }

        private bool _visibility;

        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                if (SetProperty(ref _visibility, value))
                {
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }


        public string FrontImage
        {
            get { return frontImage; }
            set { SetProperty(ref frontImage, value); }
        }



        public string BackImage
        {
            get { return backImage; }
            set { SetProperty(ref backImage, value); }
        }

        public string CurrentImage
        {
            get
            {
                if (Visibility)
                {
                    return FrontImage;
                }
                else
                {
                    return BackImage;
                }
            ;
            }


        }
    }
}

