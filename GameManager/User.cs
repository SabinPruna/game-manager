using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager
{
    [Serializable()]
    public class User
    {
        public bool HasGameSaved{ get; set; }
        public int PlayedGames { get; set; }
        public int NumberWonGames { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public User()
        {
        }
        public User(string name, string image, bool hasGameSaved)
        {
            this.Name = name;
            this.Image = image;
            this.PlayedGames = 0;
            this.NumberWonGames = 0;
            this.HasGameSaved = false;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
