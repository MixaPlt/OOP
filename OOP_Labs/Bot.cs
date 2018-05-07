using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    struct Point
    {
        public int x, y;
        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public Point(Point a)
        {
            x = a.x;
            y = a.y;
        }
    }
    abstract class Bot
    {
        protected GameField field;

        public Bot (GameField _field, Point _position)
        {
            field = _field;
        }

        public void NextStep()
        {
            
        }
        public Point Position { get; protected set; }
    }
}
