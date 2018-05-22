using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Bonsus : RandomBot
    {
        public Bonsus (GameField _field, Point _position) : base(_field, _position)
        {
            Type = 'B';
        }

    }
}
