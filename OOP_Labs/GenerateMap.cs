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
using System.IO;

namespace OOP
{
    static class GenerateMap
    {
        private static readonly int turn = 60;
        private static readonly int energyChance = 8;

        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;

        private static Button generateButton;
        private static Button backButton;

        public static void Build()
        {
            MainCanvas.Children.Clear();

            generateButton = new Button { Content = Lang.GenerateMap };
            backButton = new Button { Content = Lang.Back };

            MainCanvas.Children.Add(generateButton);
            generateButton.FontWeight = FontWeights.Medium;
            generateButton.Click += generate;

            MainCanvas.Children.Add(backButton);
            backButton.FontWeight = FontWeights.Medium;
            backButton.Click += back;

            MainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        private static void generate(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int height = rnd.Next(4, 8);
            int width = rnd.Next(4, 16);
            char[,] field = new char[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    field[i, j] = '#';
            int ry = rnd.Next(0, height);
            int rx = rnd.Next(0, width);

            int vx = 0, vy = 0;
            if (rnd.Next(0, 1) >= 1)
                vx = 1;
            else
                vy = 1;
            if (rnd.Next(0, 1) >= 1)
            {
                vx *= -1;
                vy *= -1;
            }
            Point[] way = new Point[height * width];

            for(int i = 0; i < height * width; i++)
            {
                field[ry, rx] = '·';
                if(rnd.Next(0, 100) < energyChance)
                    field[ry, rx] = '@';
                way[i] = new Point(rx, ry);

                rx = (rx + vx + width) % width;
                ry = (ry + vy + height) % height;
                if(rnd.Next(0, 99) < turn)
                {
                    int c = vx;
                    vx = vy;
                    vy = c;
                    if (rnd.Next(0, 1) == 1)
                        vx *= -1;
                    else
                        vy *= -1;
                }
            }
            field[ry, rx] = 'o';

            StreamWriter sw = new StreamWriter("maps\\Generated.map");
            sw.Write(height.ToString() + ' ' + width.ToString() + '\n');
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    sw.Write(field[i, j]);
                }
                sw.Write("\n");
            }
            sw.Write("1\nCycle ");
            for (int i = 0; i < way.Length; i++)
                sw.Write(way[i].x.ToString() + ' ' + way[i].y.ToString() + ' ');
            sw.Flush();
            sw.Close();
        }

        private static void back(object sender, EventArgs e)
        {
            Destuct();
            MainMenu.Build();
        }
        private static void windowSizeChanged(object sender, EventArgs e)
        {
            double windowHeight = MainCanvas.ActualHeight;
            double windowWidth = MainCanvas.ActualWidth;
            double h = (int)Math.Min(windowHeight / 5.5, windowWidth / 4.5);
            h = Math.Max(h, 1);
            Thickness margin = new Thickness() { Left = (windowWidth - h*3) / 2, Top = (windowHeight) / 2 - h};
            generateButton.Margin = margin;
            generateButton.Height = h;
            generateButton.Width = h * 3;
            generateButton.FontSize = h / 3;

            margin.Left = 0;
            margin.Top = windowHeight - h / 2;
            backButton.Margin = margin;
            backButton.Height = h / 2;
            backButton.Width = h * 2;
            backButton.FontSize = h / 4;
        }

        private static void Destuct()
        {
            MainWindow.SizeChanged -= windowSizeChanged;
            MainCanvas.Children.Clear();
            generateButton = null;
            backButton = null;
        }
    }
}
