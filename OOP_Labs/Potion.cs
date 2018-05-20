using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OOP
{
    abstract class Potion
    {
        public bool isGood;
        public Point Position;
        public char Type;

        public static DispatcherTimer bTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 6)};
        
        public Potion(Point position)
        {
            Position = position;
        }
        public void UsePotion()
        {
            if (isGood)
                Console.BackgroundColor = ConsoleColor.Green;
            else
                Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            bTimer.Stop();
            bTimer.Start();

        }
        public static void reBackGround(object sender, EventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
        }
    }
}
