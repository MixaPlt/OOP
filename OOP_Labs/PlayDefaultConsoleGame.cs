using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    struct cell
    {
        public char Char;
        public char Standart;
        public UInt16 Type; //0 - wall 1 - empty 2 - dots 3 - energyzer
    }
    static class PlayDefaultConsoleGame
    {
        static UInt16 X = 6, Y = 3;
        static UInt16[] Px = {9, 1, 5}, Py = { 1, 5, 5};
        static cell[,] field = new cell[7, 13];
        static readonly string standartField = "##############·····#··A··##@###·#·###·##·····o·····##·###·#·###·##A···A#····@##############";
        static AI ai;
        public static void Build()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            for(int i = 0; i < 7; i++)
                for(int j = 0; j < 13; j++)
                {
                    field[i, j].Char = standartField[i * 13 + j];
                    switch(field[i, j].Char)
                    {
                        case '#':
                            field[i, j].Type = 0;
                            break;
                        case '@':
                            field[i, j].Type = 3;
                            break;
                        default:
                            field[i, j].Type = 2;
                            break;
                    }
                }
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0 ,500);
            timer.Tick += update;
            timer.Start();

            System.Windows.Threading.DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 2);
            timer2.Tick += v_update;
            timer2.Start();

            ai = new AI(field);
        }
        static char v = 'L';
        private static void v_update(object sender, EventArgs e)
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.Left))
                v = 'L';
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.Right))
                v = 'R';
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.Up))
                v = 'U';
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.Down))
                v = 'D';
        }
        private static void update(object sender, EventArgs e)
        {
            switch (v)
            {
                case 'L':
                    if (field[Y, X - 1].Type != 0)
                    {
                        field[Y, X].Char = field[Y, X].Standart;
                        X--;
                        field[Y, X].Char = 'o';
                    }
                    break;
                case 'R':
                    if (field[Y, X + 1].Type != 0)
                    {
                        field[Y, X].Char = field[Y, X].Standart;
                        X++;
                        field[Y, X].Char = 'o';
                    }
                    break;
                case 'U':
                    if (field[Y - 1, X].Type != 0)
                    {
                        field[Y, X].Char = field[Y, X].Standart;
                        Y--;
                        field[Y, X].Char = 'o';
                    }
                    break;
                case 'D':
                    if (field[Y + 1, X].Type != 0)
                    {
                        field[Y, X].Char = field[Y, X].Standart;
                        Y++;
                        field[Y, X].Char = 'o';
                    }
                    break;

            }
            Console.SetCursorPosition(0, 7);
            for(int i = 0; i < field.GetLength(0); i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("\t\t\t\t");
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    switch(field[i, j].Char)
                    {
                        case '#':
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 'o':
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case 'A':
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 'V':
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case '@':
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(field[i, j].Char);
                }
                Console.Write("\n");
            }
        }
    }
}
