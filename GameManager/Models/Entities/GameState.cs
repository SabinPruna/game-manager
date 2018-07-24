using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.Models.Entities
{
    public class GameState
    {
        #region Properties

        [Key] public int Id { get; set; }

        public string Game { get; set; }

        public string SaveState { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        #endregion
    }
}