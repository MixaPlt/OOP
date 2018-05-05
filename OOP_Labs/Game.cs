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
    
    static class Game
    {
        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;

        private static string path;

        private static GameField field;

        private static System.Windows.Threading.DispatcherTimer timer;

        public static void Build(string mapPath)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            MainCanvas.Children.Clear();
            path = mapPath;
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
            string sField = reader.ReadToEnd();

            field = new GameField(sField);

            field.Loose += loose;
            
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += update;
            timer.Start();
        }

        private static char v = 'L';
        private static bool lt = !Keyboard.IsKeyToggled(Key.Left), rt = !Keyboard.IsKeyToggled(Key.Right), ut = !Keyboard.IsKeyToggled(Key.Up), dt = !Keyboard.IsKeyToggled(Key.Down);

        private static void loose(object sender, EventArgs e)
        {
            MessageBox.Show("Вы проиграли!");
            Destruct();
            MainMenu.Build();
        }

        private static void update(object sender, EventArgs e)
        {
            if (field == null)
                return;
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
            switch (v)
            {
                case 'L':
                    field.MoveLeft();
                    break;
                case 'R':
                    field.MoveRight();
                    break;
                case 'U':
                    field.MoveUp();
                    break;
                case 'D':
                    field.MoveDown();
                    break;
            }

            field.Update();
            if (field == null)
                return;
            Console.SetCursorPosition(0, 2);
            if (field == null)
                return;
            field.Draw();
        }


        public static void Destruct()
        {
            timer.Tick -= update;
            timer.Start();
            timer = null;
            field = null;
        }
    }
}
