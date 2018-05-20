using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class RandomBot:Bot
    {
        public RandomBot(GameField _field, Point _position):base(_field, _position)
        {

        }
        public override bool NextStep()
        {
            int r = Resources.Random(4);
            Point n = new Point(Position);
            switch (r)
            {
                case 0:
                    n.y--;
                    break;
                case 1:
                    n.y++;
                    break;
                case 2:
                    n.x--;
                    break;
                case 3:
                    n.x++;
                    break;
            }
            n.x = (n.x + field.Width) % field.Width;
            n.y = (n.y + field.Height) % field.Height;
            if (field[n.y, n.x].Standart == '#')
                return true;
            Position = n;
            return true;
        }
    }
}
