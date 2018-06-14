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
    static class Resources
    {
        public static Window MainWindow { get; private set; }
        public static Canvas MainCanvas { get; private set; }
        public static string PlayerName { get; set; }
        public static BitmapImage[] imageSources = new BitmapImage[9];
        private static Random random;
        public static int Random(int MaxValue)
        {
            return random.Next(0, MaxValue);
        }
        public static void Init(MainWindow mainWindow, Canvas mainCanvas)
        {
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            random = new Random();
            Lang.SelectedIndex = 0;
            Potion.bTimer.Tick += Potion.reBackGround;

            imageSources[0] = new BitmapImage();
            imageSources[0].BeginInit();
            imageSources[0].UriSource = new Uri("Images/Wall.png", UriKind.Relative);
            imageSources[0].EndInit();

            imageSources[1] = new BitmapImage();
            imageSources[1].BeginInit();
            imageSources[1].UriSource = new Uri("Images/pacman.png", UriKind.Relative);
            imageSources[1].EndInit();

            imageSources[2] = new BitmapImage();
            imageSources[2].BeginInit();
            imageSources[2].UriSource = new Uri("Images/pust.png", UriKind.Relative);
            imageSources[2].EndInit();

            imageSources[3] = new BitmapImage();
            imageSources[3].BeginInit();
            imageSources[3].UriSource = new Uri("Images/enemy.png", UriKind.Relative);
            imageSources[3].EndInit();

            imageSources[4] = new BitmapImage();
            imageSources[4].BeginInit();
            imageSources[4].UriSource = new Uri("Images/dot.png", UriKind.Relative);
            imageSources[4].EndInit();

            imageSources[5] = new BitmapImage();
            imageSources[5].BeginInit();
            imageSources[5].UriSource = new Uri("Images/energyzer.png", UriKind.Relative);
            imageSources[5].EndInit();

            imageSources[6] = new BitmapImage();
            imageSources[6].BeginInit();
            imageSources[6].UriSource = new Uri("Images/scared.png", UriKind.Relative);
            imageSources[6].EndInit();

            imageSources[7] = new BitmapImage();
            imageSources[7].BeginInit();
            imageSources[7].UriSource = new Uri("Images/bonus.png", UriKind.Relative);
            imageSources[7].EndInit();

            imageSources[8] = new BitmapImage();
            imageSources[8].BeginInit();
            imageSources[8].UriSource = new Uri("Images/potion.png", UriKind.Relative);
            imageSources[8].EndInit();
        }
    }
}
