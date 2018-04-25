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
    static class MainMenu
    {
        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;
        private static Button StartGame;
        private static Button GlobalSettings;
        private static Button AuthorLicense;
        private static Button ExitGame;


        public static void Build()
        {
            MainCanvas.Children.Clear();
            StartGame = new Button();
            GlobalSettings = new Button();
            AuthorLicense = new Button();
            ExitGame = new Button();
        }

        private static void Destuct()
        {
            MainCanvas.Children.Clear();
            StartGame = null;
        }
    }
}
