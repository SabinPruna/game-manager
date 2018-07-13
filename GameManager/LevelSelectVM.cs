using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.Models;
namespace GameManager
{
    public class LevelSelectVM
    {
        public Player Player { get; set; } 
        public LevelSelectVM()
        {
            Player = new Player();
        }
    }
}
