using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameManager.Models.Entities
{
    public class Player
    {
        #region Constructors

        public Player(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Player() { }

        #endregion

        #region  Properties

        [Key] public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] UserPicture { get; set; }

        public int Money { get; set; }

        public virtual List<GameRecord> GameRecords { get; set; }


        #endregion
    }
}