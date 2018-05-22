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
        public bool isGood = false;
        public Point Position;
        public char Type;
        public GameField field;
        public static bool st = false;

        public static DispatcherTimer bTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 8002)};
        private DispatcherTimer effectTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 8) };
        
        public Potion(Point position, GameField _field)
        {
            Position = position;
            effectTimer.Tick += RevokeEffect;
            field = _field;
        }
        public virtual void UsePotion()
        {
            if (isGood)
                Console.BackgroundColor = ConsoleColor.Green;
            else
                Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            bTimer.Stop();
            bTimer.Start();
            effectTimer.Start();
        }
        public virtual void RevokeEffect(object sender, EventArgs e)
        {
            effectTimer.Stop();
            if (field == null)
                return;
        }
        public static void reBackGround(object sender, EventArgs e)
        {

            if (st)
                return;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
        }
    }
}
