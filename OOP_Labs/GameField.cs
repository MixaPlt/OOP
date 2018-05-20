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
        public event EventHandler Won;

        public int Score { get; private set; } = 0;

        public int dots = 0;

        public int Height { get; private set; }
        public int Width { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int BotsNumber { get; private set; }

        public Bot[] Bots;

        public bool IsEnergyzed { get; private set; }
        private DispatcherTimer energyTimer;

        private cell[,] field;

        public EventHandler Loose;
        public GameField(string map)
        {
            BotsNumber = 0;
            energyTimer = new DispatcherTimer();

            energyTimer.Tick += unEnergyzer;
            string[] k = map.Split('\n');
            Width = Int32.Parse(k[0].Split(' ')[1]);
            Height = Int32.Parse(k[0].Split(' ')[0]);
            map = "";
            field = new cell[Height, Width];

            for (ushort i = 0; i < Height; i++)
            {
                for (ushort j = 0; j < Width; j++)
                {
                    field[i, j].Char = k[i + 1][j];
                    field[i, j].Standart = field[i, j].Char;
                    switch (field[i, j].Char)
                    {
                        case '#':
                            field[i, j].Type = 0;
                            break;
                        case '@':
                            field[i, j].Type = 3;
                            break;
                        case '·':
                            field[i, j].Type = 2;
                            dots++;
                            break;
                        default:
                            field[i, j].Type = 2;
                            break;
                    }
                    if(field[i, j].Char == 'o')
                    {
                        X = j;
                        Y = i;
                        field[i, j].Standart = ' ';
                    }
                }
            }

            BotsNumber = Int32.Parse(k[Height + 1]);

            Bots = new Bot[BotsNumber];

            for(int i = 0; i < BotsNumber; i++)
            {
                string[] b = k[i + Height + 2].Split(' ');
                if(b[0][0] == 'C')
                {
                    Point[] way = new Point[(b.Length - 1) / 2];
                    for(int j = 0; j < way.Length; j++)
                    {
                        way[j] = new OOP.Point(Int32.Parse(b[j * 2 + 1]), Int32.Parse(b[j * 2 + 2]));
                    }
                    Bots[i] = new CycleBot(this, way[0], way);
                }
                if (b[0][0] == 'R')
                {
                    Bots[i] = new RandomBot(this, new Point(Int32.Parse(b[1]), Int32.Parse(b[2])));
                }
                if (b[0][0] == 'F')
                {
                    Bots[i] = new FastBot(this, new Point(Int32.Parse(b[1]), Int32.Parse(b[2])));
                }
            }

            useEnergyzer();
            energyTimer.Interval = new TimeSpan(0, 0, 3);
            
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
        }

        public void MoveUp()
        {
            int ny = (Y + Height - 1);
            ny %= Height;
            if (field[ny, X].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            Y = ny;
        }
        public void MoveRight()
        {
            int nx = X + 1;
            nx %= Width;
            if (field[Y, nx].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            X = nx;
        }
        public void MoveLeft()
        {
            int nx = (X + Width - 1);
            nx %= Width;
            if (field[Y, nx].Type == 0)
                return;
            field[Y, X].Char = field[Y, X].Standart;
            X = nx;
        }

        public void Draw()
        {
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    field[i, j].Char = field[i, j].Standart;
                }
            }
            field[Y, X].Char = '0';
            for(int i = 0; i < BotsNumber; i++)
            {
                if(!IsEnergyzed)
                    field[Bots[i].Position.y, Bots[i].Position.x].Char = 'A';
                else
                    field[Bots[i].Position.y, Bots[i].Position.x].Char = 'V';
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    switch (field[i, j].Char)
                    {
                        case '#':
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '0':
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case 'A':
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
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
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Score: " + Score.ToString());
        }

        public void Update()
        {
            checkSubCell();
            for(int i = 0; i < BotsNumber; i++)
            {
                Bots[i].NextStep();
            }
            checkSubCell();
        }

        private void checkSubCell()
        {
            if (field[Y, X].Type == cell.Enrgyzer)
            {
                useEnergyzer();
            }
            if (field[Y, X].Char == 'V')
            {
                int l = 0;
                
                for (int i = 0; i < BotsNumber; i++)
                    if (Bots[i].Position.x != X || Bots[i].Position.y != Y)
                    {
                        l++;
                    }
                Bot[] kek = new Bot[l];
                l = 0;
                for (int i = 0; i < BotsNumber; i++)
                    if (Bots[i].Position.x != X || Bots[i].Position.y != Y)
                    {
                        kek[l] = Bots[i];
                        l++;
                    }
                Score += 50;
                BotsNumber--;
                Bots = kek;
            }
            if (field[Y, X].Char == 'A')
            {
                Loose?.Invoke(this, null);
            }
            if (field[Y, X].Standart == '·')
            {
                Score++;
                dots--;
                field[Y, X].Standart = ' ';
                if (dots == 0)
                    Won?.Invoke(this, null);

            }
        }

        private void useEnergyzer()
        {
            field[Y, X].Type = cell.Empty;
            field[Y, X].Standart = ' ';
            IsEnergyzed = true;
            energyTimer.Stop();
            energyTimer.Interval = new TimeSpan(0, 0, 5);
            energyTimer.Start();
        }

        private void unEnergyzer(object sender, EventArgs e)
        {
            IsEnergyzed = false;
            energyTimer.Stop();
        }

        ~GameField()
        {
            energyTimer.Stop();
        }
    }
}
