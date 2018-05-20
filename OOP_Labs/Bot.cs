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
        public static int speed = 1;
        protected GameField field;

        public Bot (GameField _field, Point _position)
        {
            field = _field;
            Position = _position;
        }

        public virtual bool NextStep() { return false; }
        public Point Position { get;  set; }
        public char Type = 'B';

        public void MoveLeft()
        {
            int nx = (Position.x - 1 + field.Width) % field.Width;
            if (field[Position.y, nx].Standart != '#')
                Position = new Point(nx, Position.y);
        }
        public void MoveRight()
        {
            int nx = (Position.x + 1) % field.Width;
            if (field[Position.y, nx].Standart != '#')
                Position = new Point(nx, Position.y);
        }
        public void MoveUp()
        {
            int ny = (Position.y - 1 + field.Height) % field.Height;
            if (field[ny, Position.x].Standart != '#')
                Position = new Point(Position.x, ny);
        }
        public void MoveDown()
        {
            int ny = (Position.y + 1) % field.Height;
            if (field[ny, Position.x].Standart != '#')
                Position = new Point(Position.x, ny);
        }
    }
}