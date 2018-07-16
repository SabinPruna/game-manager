namespace GameManager.Models
{
    public class Card
    {
        #region Constructors

        public Card()
        {
        }

        public Card(string imageUp, string imageDown, bool visibility)
        {
            ImageUp = imageUp;
            ImageDown = imageDown;
            CurrentImageCard = imageDown;
            Visibility = visibility;
        }

        #endregion

        #region  Properties

        public bool Visibility { get; set; }
        public string ImageUp { get; set; }
        public string ImageDown { get; set; }
        public string CurrentImageCard { get; set; }

        #endregion

        public void ChangeSideOfCard()
        {
            CurrentImageCard = CurrentImageCard.Equals(ImageUp) ? ImageDown : ImageUp;
        }
    }
}