using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class FastBot : Bot
    {
        public static new int speed = 2;
        int steps = 0;
        public FastBot(GameField _field, Point _position) : base(_field, _position)
        {
            Type = 'F';
        }
        public override bool NextStep()
        {
            steps = (steps + 1) % speed;
            if (steps == 0)
                return false;
            char[,] used = new char[field.Height, field.Width];
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    used[i, j] = '*';
            used[Position.y, (Position.x - 1 + field.Width) % field.Width] = 'L';
            used[Position.y, (Position.x + 1) % field.Width] = 'R';
            used[(Position.y - 1 + field.Height) % field.Height, Position.x] = 'U';
            used[(Position.y + 1) % field.Height, Position.x] = 'D';

            Queue<Point> q = new Queue<Point>();
                q.Enqueue(new Point((Position.x - 1 + field.Width) % field.Width, Position.y));
                q.Enqueue(new Point((Position.x + 1) % field.Width, Position.y));
                q.Enqueue(new Point(Position.x, (Position.y - 1 + field.Height) % field.Height));
                q.Enqueue(new Point(Position.x, (Position.y + 1) % field.Height));

            while(q.Count != 0 && used[field.Y, field.X] == '*')
            {
                Point cur = q.Dequeue();
                if (field[cur.y, cur.x].Standart == '#')
                    continue;
                int mx = (cur.x - 1 + field.Width) % field.Width;
                int px = (cur.x + 1) % field.Width;
                int my = (cur.y - 1 + field.Height) % field.Height;
                int py = (cur.y + 1) % field.Height;

                if (used[cur.y, mx] == '*')
                {
                    used[cur.y, mx] = used[cur.y, cur.x];
                    q.Enqueue(new Point(mx, cur.y));
                }
                if (used[cur.y, px] == '*')
                {
                    used[cur.y, px] = used[cur.y, cur.x];
                    q.Enqueue(new Point(px, cur.y));
                }
                if (used[my, cur.x] == '*')
                {
                    used[my, cur.x] = used[cur.y, cur.x];
                    q.Enqueue(new Point(cur.x, my));
                }
                if (used[py, cur.x] == '*')
                {
                    used[py, cur.x] = used[cur.y, cur.x];
                    q.Enqueue(new Point(cur.x, py));
                }
            }
            switch(used[field.Y, field.X])
            {
                case 'L':
                    MoveLeft();
                    break;
                case 'R':
                    MoveRight();
                    break;
                case 'U':
                    MoveUp();
                    break;
                case 'D':
                    MoveDown();
                    break;
            }
            return true;
        }
    }
}
