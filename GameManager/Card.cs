using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager
{
    public class Card
    {
        public Card()
        {
        }
        public Card(string imageUp, string imageDown, Boolean visibility)
        {
            this.ImageUp = imageUp;
            this.ImageDown = imageDown;
            this.CurrentImageCard = imageDown;
            this.Visibility = visibility;
        }
        public void ChangeSideOfCard()
        {
            if ((this.CurrentImageCard).Equals(ImageUp))
                this.CurrentImageCard = ImageDown;
            else
                this.CurrentImageCard = ImageUp;
        }
        public Boolean Visibility { get; set; }
        public string ImageUp { get; set; }
        public string ImageDown { get; set; }
        public string CurrentImageCard { get; set; }
    }
}
