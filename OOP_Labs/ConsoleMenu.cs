using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;

namespace OOP
{
    class ConsoleMenu
    {
        public UInt16 SelectedItem { get; private set; }

        private string[] items;
        private DispatcherTimer timer;

        public event EventHandler FirstItemSelected;
        public event EventHandler SecondItemSelected;
        public event EventHandler ThirdItemSelected;
        public event EventHandler FourthItemSelected;
        public event EventHandler FifthItemSelected;



        public ConsoleMenu(string[] _items, string description)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            SelectedItem = 0;
            items = _items;
            timer = new DispatcherTimer();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 0);
            Console.Write("\t\t\t    Use " + (char)30 + " and " + (char)31 + " to select item" + "\t\t\t\t");
            length = (UInt16)items.GetLength(0);

            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += draw;
            timer.Start();
        }
        bool ek = !Keyboard.IsKeyToggled(Key.Enter), uk = true, dk = true;
        private UInt16 length;
        
        private void draw(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.Enter) == ek)
            {

                switch (SelectedItem)
                {
                    case 0:
                        FirstItemSelected?.Invoke(this, null);
                        break;
                    case 1:
                        SecondItemSelected?.Invoke(this, null);
                        break;
                    case 2:
                        ThirdItemSelected?.Invoke(this, null);
                        break;
                    case 3:
                        FourthItemSelected?.Invoke(this, null);
                        break;
                    case 4:
                        FifthItemSelected?.Invoke(this, null);
                        break;
                }
                ek = !ek;
            }
            Console.SetCursorPosition(0, 10);
            if (Keyboard.IsKeyToggled(Key.Down) == dk)
            {
                SelectedItem++;
                dk = !dk;
            }
            if (Keyboard.IsKeyToggled(Key.Up) == uk)
            {
                SelectedItem += (ushort)(length - 1);
                uk = !uk;
            }
            SelectedItem %= length;
            for(int i = 0; i < length; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("\t\t\t");
                if(SelectedItem == i)
                    Console.BackgroundColor = ConsoleColor.Green;
                else
                    Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write("\t" + items[i] + "\t");
                if (items[i].Length < 8)
                    Console.Write("\t");
                if (items[i].Length < 16)
                    Console.Write("\t");
                Console.Write("\n");
            }
        }
        public void Destruct()
        {
            timer.Stop();
            timer.Tick -= draw;
            timer = null;
        }
        ~ConsoleMenu()
        {
           
        }
    }
}
