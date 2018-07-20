using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace GameManager.Models.Snake
{
    public class Snake
    {
        public List<Husk> Husks;
        public Husk Head;
        public Husk Tail;
        public Direction CurrentDirection;

        #region Constructors

        public Snake() { }

        public Snake(Point head, int length, Direction direction)
        {
            Husks = new List<Husk>();

            Head = new Husk(Direction.Right, head);

            for (int i = 0; i < length; i++)
            {
                Point tmpPoint = new Point(head.X - i, head.Y);
                Husks.Add(new Husk(Direction.Right, tmpPoint));
            }

            CurrentDirection = direction;

            Tail = new Husk(Direction.Right, Husks.Last().Point);
        }

        #endregion

        #region Properties

        public int Length
        {
            get { return Husks.Count; }
        }

        #endregion

        #region Methods

        public void MoveHusks()
        {
            foreach (var husk in Husks)
            {
                switch (husk.Direction)
                {
                    case Direction.Left:
                        husk.Point = new Point(husk.Point.X - 1, husk.Point.Y);
                        break;

                    case Direction.Right:
                        husk.Point = new Point(husk.Point.X + 1, husk.Point.Y);
                        break;

                    case Direction.Up:
                        husk.Point = new Point(husk.Point.X, husk.Point.Y - 1);
                        break;

                    case Direction.Down:
                        husk.Point = new Point(husk.Point.X, husk.Point.Y + 1);
                        break;
                }
            }
            Head.Point = Husks.First().Point;
            Tail.Point = Husks.Last().Point;
        }

        internal void AddHusk()
        {
            Point tmpPoint = new Point();
            switch (Tail.Direction)
            {
                case Direction.Left:
                    tmpPoint = new Point(Tail.Point.X + 1, Tail.Point.Y);
                    break;
                case Direction.Right:
                    tmpPoint = new Point(Tail.Point.X - 1, Tail.Point.Y);
                    break;
                case Direction.Up:
                    tmpPoint = new Point(Tail.Point.X, Tail.Point.Y + 1);
                    break;
                case Direction.Down:
                    tmpPoint = new Point(Tail.Point.X, Tail.Point.Y - 1);
                    break;
            }

            Husks.Add(new Husk(
                Husks.Last().Direction,
                tmpPoint
                )
            );
        }

        public void UpdateDirections()
        {
            for (int i = Husks.Count - 1; i > 0; i--)
            {
                Husks[i].Direction = Husks[i - 1].Direction;
            }

            Head.Direction = Husks[0].Direction = CurrentDirection;
            Tail.Direction = Husks.Last().Direction;
        }

        public void Move(Direction direction)
        {
            CurrentDirection = direction;
            UpdateDirections();
            MoveHusks();
        }

        #endregion
    }
}
