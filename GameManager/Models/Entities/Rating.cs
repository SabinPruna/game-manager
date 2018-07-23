using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GameManager.Models.Entities
{
    public class Rating
    {
        #region Properties

        [Key] public int Id { get; set; }

        public string Game { get; set; }

        public int NumberStars { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        #endregion
    }
}
