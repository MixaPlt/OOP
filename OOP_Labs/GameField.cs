using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;

namespace OOP
{
    struct cell
    {

        public char Char;
        public char Standart;
        public UInt16 Type;
        public int id;
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
        public int PotionsNumber { get; private set; }
        public DispatcherTimer timer { get; private set; }
        public static Image[,] imageField;
        public int Steps { get; private set; } = 0;

        
        public int SlowMo = 1;

        public int BonusNumber = 0;
        public Bonsus[] Bonuses;

        public Bot[] Bots;
        public Potion[] Potions;

        public bool IsEnergyzed { get; private set; }
        private DispatcherTimer energyTimer;

        private cell[,] field;



        public EventHandler Loose;

        public bool Invulnerability = false;

        public GameField(string map)
        {
            timer = new DispatcherTimer();
            BotsNumber = 0;
            energyTimer = new DispatcherTimer();

            energyTimer.Tick += unEnergyzer;
            string[] k = map.Split('\n');
            Width = Int32.Parse(k[0].Split(' ')[1]);
            Height = Int32.Parse(k[0].Split(' ')[0]);
            BonusNumber = Int32.Parse(k[0].Split(' ')[2]);
            Bonuses = new Bonsus[BonusNumber];
            BonusNumber = 0;
            map = "";
            field = new cell[Height, Width];
            imageField = new Image[Height, Width];

            for (ushort i = 0; i < Height; i++)
            {
                for (ushort j = 0; j < Width; j++)
                {
                    field[i, j].id = 0;
                    int w = (int)Math.Min(Resources.MainCanvas.ActualHeight / Height, Resources.MainCanvas.ActualWidth / Width);
                    int h = 1;
                    while (h * 2 <= w)
                        h *= 2;
                    imageField[i, j] = new Image() { Height = h, Width = h, Source = Resources.imageSources[0], Margin = new Thickness(j*h, i*h, 0, 0) };
                    
                    //Resources.MainCanvas.Children.Add(imageField[i, j]);
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
                        case 'B':
                            field[i, j].Type = 2;
                            field[i, j].Standart = ' ';
                            Bonuses[BonusNumber] = new Bonsus(this, new Point(j, i));
                            BonusNumber++;
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

            PotionsNumber = Int32.Parse(k[Height + BotsNumber + 2]);
            Potions = new Potion[PotionsNumber];
            for (int i = 0; i < PotionsNumber; i++)
            {
                string[] b = k[i + Height + BotsNumber + 3].Split(' ');
                if (b[0][0] == 'S')
                {
                    Potions[i] = new SlowMoPotion(new Point(Int32.Parse(b[1]), Int32.Parse(b[2])), this);
                }
                if (b[0][0] == 'D')
                {
                    Potions[i] = new DisorientationPotion(new Point(Int32.Parse(b[1]), Int32.Parse(b[2])), this);
                }
                if (b[0][0] == 'I')
                {
                    Potions[i] = new InvulnerabilityPotion(new Point(Int32.Parse(b[1]), Int32.Parse(b[2])), this);
                }
            }

            useEnergyzer();
            energyTimer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();
            
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
            Resources.imageSources[8].Rotation = System.Windows.Media.Imaging.Rotation.Rotate90;
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
            Console.WriteLine("Player: " + Resources.PlayerName + "; Steps: " + Steps.ToString() + "; Time: " + (Steps / 5).ToString() + "s.");
            for (int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    field[i, j].Char = field[i, j].Standart;
                }
            }
            field[Y, X].Char = '☺';
            for (int i = 0; i < PotionsNumber; i++)
            {
                field[Potions[i].Position.y, Potions[i].Position.x].Char = '⌂';
            }
            for (int i = 0; i < BonusNumber; i++)
            {
                field[Bonuses[i].Position.y, Bonuses[i].Position.x].Char = '♣';
            }
            for (int i = 0; i < BotsNumber; i++)
            {
                if(!IsEnergyzed)
                    field[Bots[i].Position.y, Bots[i].Position.x].Char = 'A';
                else
                    field[Bots[i].Position.y, Bots[i].Position.x].Char = 'V';
            }
            string st = "";
            
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    switch (field[i, j].Char)
                    {
                        case '#':
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case '☺':
                            if (field[i, j].id != 1)
                            {
                                imageField[i, j].Source = Resources.imageSources[1];
                                field[i, j].id = 1;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 'A':
                            if (field[i, j].id != 3)
                            {
                                imageField[i, j].Source = Resources.imageSources[3];
                                field[i, j].id = 3;
                            }
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 'V':
                            if (field[i, j].id != 6)
                            {
                                imageField[i, j].Source = Resources.imageSources[6];
                                field[i, j].id = 6;
                            }
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case '@':
                            if (field[i, j].id != 5)
                            {
                                imageField[i, j].Source = Resources.imageSources[5];
                                field[i, j].id = 5;
                            }
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        case '⌂':
                            if (field[i, j].id != 8)
                            {
                                imageField[i, j].Source = Resources.imageSources[8];
                                field[i, j].id = 8;
                            }
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case '♣':
                            if (field[i, j].id != 7)
                            {
                                imageField[i, j].Source = Resources.imageSources[7];
                                field[i, j].id = 7;
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case '·':
                            if (field[i, j].id != 4)
                            {
                                imageField[i, j].Source = Resources.imageSources[4];
                                field[i, j].id = 4;
                            }
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                        default:
                            if (field[i, j].id != 2)
                            {
                                imageField[i, j].Source = Resources.imageSources[2];
                                field[i, j].id = 2;
                            }
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(field[i, j].Char);
                    st += field[i, j].Char;
                }
                st += '\n';
                Console.Write("\n");
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("Score: " + Score.ToString());
        }

        public void Update()
        {
            checkSubCell();
            Steps++;
            if (Steps % SlowMo == 1)
                return;
            for(int i = 0; i < BotsNumber; i++)
            {
                Bots[i].NextStep();
            }
            for (int i = 0; i < BonusNumber; i++)
            {
                Bonuses[i].NextStep();
            }
            checkSubCell();
        }

        public void checkSubCell()
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
                Score += 50 * (BotsNumber - l);
                BotsNumber = l;
                Bots = kek;
            }

            if (field[Y, X].Char == '♣')
            {
                int l = 0;

                for (int i = 0; i < BonusNumber; i++)
                    if (Bonuses[i].Position.x != X || Bonuses[i].Position.y != Y)
                    {
                        l++;
                    }
                Bonsus[] kek = new Bonsus[l];
                l = 0;
                for (int i = 0; i < BonusNumber; i++)
                    if (Bonuses[i].Position.x != X || Bonuses[i].Position.y != Y)
                    {
                        kek[l] = Bonuses[i];
                        l++;
                    }
                Score += 100 * (BonusNumber - l);
                BonusNumber = l;
                Bonuses = kek;
            }

            if (field[Y, X].Char == '⌂')
            {
                int l = 0;
                for (int i = 0; i < PotionsNumber; i++)
                    if (Potions[i].Position.x != X || Potions[i].Position.y != Y)
                    {
                        l++;
                    }
                     else
                    {
                        Potions[i].UsePotion();
                    }
                
                Potion[] kek = new Potion[l];
                l = 0;
                for (int i = 0; i < PotionsNumber; i++)
                    if (Potions[i].Position.x != X || Potions[i].Position.y != Y)
                    {
                        kek[l] = Potions[i];
                        l++;
                    }
                Score += 10;
                PotionsNumber = l;
                Potions = kek;
            }
            if (field[Y, X].Char == 'A' && !Invulnerability)
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
