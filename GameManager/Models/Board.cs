using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameManager.Models
{
    
    public class Board
   {
        #region Fields

        private int _width;
        private int _height;
        private bool _isColision;
        private Point _luckyPoint;

        public int Points;

        public int[,] Cells;

        #endregion

        #region Constructor

        public Board(int width, int height)
        {
            Points = 0;
            _width = width;
            _height = height;
            _isColision = false;
            Cells = new int[_height, _width];
        }

        #endregion

        #region Public Methods
        public void UpdateBoard(Snake snake)
       {
            ClearBoard();

            foreach (var husk in snake.Husks)
            {
                try
                {
                    Cells[(int)husk.Point.Y, (int)husk.Point.X] += 1;
                }
                catch (IndexOutOfRangeException)
                {
                    _isColision = true;
                }
            }

            Cells[(int)_luckyPoint.Y, (int)_luckyPoint.X] += 3;

            if (_luckyPoint == snake.Head.Point)
            {
                snake.AddHusk();
                Points += 10;
                drawPoint();
            }
        }

        /*public void ShowBoard(Snake snake)
        {

            Console.Clear();

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    switch (Cells[i, j])
                    {
                        case 1:
                            Console.Write("#");
                            break;
                        case 0:
                            Console.Write(" ");
                            break;
                        case -1:
                            Console.Write("@");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }*/

        public bool IsColision(Snake snake)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (Cells[i, j] == 2)
                    {
                        return true;
                    }
                }
            }

            return false || _isColision;
        }

        public void drawPoint()
        {
            Random rand = new Random();
            Point tmpPoint;

            do
            {
                tmpPoint = new Point(rand.Next(_width), rand.Next(_height));
            } while (Cells[(int)tmpPoint.Y, (int)tmpPoint.X] == 1);

            _luckyPoint = tmpPoint;

            Cells[(int)_luckyPoint.Y, (int)_luckyPoint.X] += 3;

        }

        #endregion

        #region Private Methods

        private void ClearBoard()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Cells[i, j] = 0;
                }
            }
        }

        #endregion
    }
}
