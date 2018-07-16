using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameManager.Models.Entities
{
    public class GameRecord
    {
        #region  Properties

        [Key] public int Id { get; set; }

        public string Game { get; set; }

        public int Score { get; set; }

        public DateTime Date { get; set; }

        public virtual Player Player { get; set; }

        #endregion
    }
}