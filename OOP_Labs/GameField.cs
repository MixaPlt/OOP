using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace OOP
{
    struct cell
    {
        public char Char;
        public char Standart;
        public UInt16 Type;
        public static readonly UInt16 Wall = 0, Empty = 1, Dot = 2, Enrgyzer = 3;
        public static bool operator !=(cell a, UInt16 b)
        {
            return a.Type != b;
        }
        public static bool operator ==(cell a, UInt16 b)
        {
            return a.Type == b;
        }
    }
    class GameField
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int BotsNumber { get; private set; }

        private int[] botsX;
        private int[] botsY;

        public bool IsEnergyzed { get; private set; }
        private DispatcherTimer energyTimer;

        private cell[,] field;
        public GameField(string map)
        {
            BotsNumber = 0;
            energyTimer = new DispatcherTimer();

            energyTimer.Tick += unEnergyzer;
            string[] k = map.Split('\n');
            Width = k[0].Length;
            Height = k.GetLength(0);
            map = "";
            for (int i = 0; i < k.Length; i++)
                map += k[i];
            Height = (ushort)(map.Length / Width);
            field = new cell[Height, Width];

            for (ushort i = 0; i < Height; i++)
                for (ushort j = 0; j < Width; j++)
                {
                    field[i, j].Char = map[i * Width + j];
                    if (field[i, j].Char == 'A')
                        BotsNumber++;
                    switch (field[i, j].Char)
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
            botsX = new int[BotsNumber];
            botsY = new int[BotsNumber];
            BotsNumber = 0;
            for (ushort i = 0; i < Height; i++)
                for (ushort j = 0; j < Width; j++)
                {
                    switch (field[i, j].Char)
                    {
                        case 'o':
                            X = j;
                            Y = i;
                            field[i, j].Type = 2;
                            break;
                        case 'A':
                            botsX[BotsNumber] = j;
                            botsY[BotsNumber] = i;
                            BotsNumber++;
                            break;
                    }
                }
            MessageBox.Show(BotsNumber.ToString());
        }

        public cell this[int a, int b]
        {
            get
            {
                return field[a, b];
            }
        }

        public void MoveDown()
        {
            int ny = Y + 1;
            ny %= Height;
            if (field[ny, X].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            Y = ny;
            field[Y, X].Char = 'o';
        }

        public void MoveUp()
        {
            int ny = (Y + Height - 1);
            ny %= Height;
            if (field[ny, X].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            Y = ny;
            field[Y, X].Char = 'o';
        }
        public void MoveRight()
        {
            int nx = X + 1;
            nx %= Width;
            if (field[Y, nx].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            X = nx;
            field[Y, X].Char = 'o';
        }
        public void MoveLeft()
        {
            int nx = (X + Width - 1);
            nx %= Width;
            if (field[Y, nx].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            X = nx;
            field[Y, X].Char = 'o';
        }

        public void Draw()
        {

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    switch (field[i, j].Char)
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

        public void Update()
        {
            if(field[Y, X].Type == cell.Enrgyzer)
            {
                useEnergyzer();
            }
        }

        private void useEnergyzer()
        {
            field[Y, X].Type = cell.Empty;
            IsEnergyzed = true;
            energyTimer.Stop();
            energyTimer.Interval = new TimeSpan(0, 0, 5);
            energyTimer.Start();
            for (int i = 0; i < BotsNumber; i++)
                field[botsY[i], botsX[i]].Char = 'V';
        }

        private void unEnergyzer(object sender, EventArgs e)
        {
            IsEnergyzed = false;
            for (int i = 0; i < BotsNumber; i++)
                field[botsY[i], botsX[i]].Char = 'A';
            energyTimer.Stop();
        }

        ~GameField()
        {
            energyTimer.Stop();
        }
    }
}
