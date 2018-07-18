using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameManager.Models
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class Husk
    {
        public Direction Direction;
        public Point Point;

        public Husk(Direction direction, Point point)
        {
            Direction = direction;
            Point = point;
        }
    }
}
