﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class CycleBot:Bot
    {
        private Random rnd = new Random();

        public CycleBot (GameField _field, Point _position, Point[] _way) : base(_field, _position)
        {
            way = _way;
        }
        private int cp = 0;
        private Point[] way;
        public override bool NextStep()
        {
            cp = (cp + 1) % way.Length;
            Position = way[cp];
            return true;
        }
    }
}
