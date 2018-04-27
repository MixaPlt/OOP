using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP
{
    struct cell
    {
        public char Char;
        public char Standart;
        public UInt16 Type; //0 - wall 1 - empty 2 - dots 3 - energyzer
    }
    static class Game
    {
        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;

        static string sField;
        private static string path;

        private static UInt16 Height, Width;

        static cell[,] field;

        private static System.Windows.Threading.DispatcherTimer timer;

        public static void Build(string mapPath)
        {
            MainCanvas.Children.Clear();
            path = mapPath;
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
            sField = reader.ReadLine();
            Width = (ushort)sField.Length;
            sField += reader.ReadToEnd();
            string[] k = sField.Split('\n');
            sField = "";
            for (int i = 0; i < k.Length; i++)
                sField += k[i];
            Height = (ushort)(sField.Length / Width);
            field = new cell[Height, Width];

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    field[i, j].Char = sField[i * 13 + j];
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
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 2);
            timer.Tick += update;
            timer.Start();
        }

        private static char v = 'L';
        private static bool lt, rt, ut, dt;

        private static void update(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.Left) == lt)
            {
                v = 'L';
                lt = !lt;
            }
            if (Keyboard.IsKeyToggled(Key.Right) == rt)
            {
                v = 'R';
                rt = !rt;
            }
            if (Keyboard.IsKeyToggled(Key.Up) == ut)
            {
                v = 'U';
                ut = !ut;
            }
            if (Keyboard.IsKeyToggled(Key.Down) == dt)
            {
                v = 'D';
                dt = !dt;
            }
        }

        public static void Destruct()
        {
            field = null;
            timer.Tick -= update;
            timer.Start();
            timer = null;
        }
    }
}
