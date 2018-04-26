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
    static class StartGameMenu
    {
        private static Canvas MainCanvas;
        private static Window MainWindow;

        private static Button newGameButton;
        private static Button loadButton;
        private static Button backButton;

        private static ConsoleMenu consoleMenu;

        public static void Build()
        {
            MainCanvas = OOP.Resources.MainCanvas;
            MainWindow = OOP.Resources.MainWindow;

            newGameButton = new Button() { Content = Lang.StartNewGame };
            loadButton = new Button() { Content = Lang.LoadSave };
            backButton = new Button() { Content = Lang.Back };

            newGameButton.FontWeight = FontWeights.Medium;
            loadButton.FontWeight = FontWeights.Medium;
            backButton.FontWeight = FontWeights.Medium;

            MainCanvas.Children.Add(newGameButton);
            MainCanvas.Children.Add(loadButton);
            MainCanvas.Children.Add(backButton);

            MainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);

            newGameButton.Click += newGame;
            loadButton.Click += load;
            backButton.Click += back;

            string[] consoleItems = { Lang.StartNewGame, Lang.LoadSave, Lang.Back };
            consoleMenu = new ConsoleMenu(consoleItems, "");

            consoleMenu.FirstItemSelected += newGame;
            consoleMenu.SecondItemSelected += load;
            consoleMenu.ThirdItemSelected += back;
        }

        private static void newGame(object sender, EventArgs e)
        {

        }

        private static void load(object sender, EventArgs e)
        {

        }

        private static void back(object sender, EventArgs e)
        {
            Destruct();
            MainMenu.Build();
        }

        private static void windowSizeChanged(object sender, EventArgs e)
        {
            double windowHeight = MainCanvas.ActualHeight;
            double windowWidth = MainCanvas.ActualWidth;
            double h = Math.Min(windowHeight / 4.5, windowWidth / 3.5);
            h = Math.Max(h, 1);

            Thickness margin = new Thickness((windowWidth - h * 3) / 2, (windowHeight - h * 3) / 2, (windowWidth - h * 3) / 2, (windowHeight - h * 3) / 2);

            newGameButton.Margin = margin;
            newGameButton.Height = h;
            newGameButton.Width = h * 3;
            newGameButton.FontSize = h / 3;
            margin.Top += h;

            loadButton.Margin = margin;
            loadButton.Height = h;
            loadButton.Width = h * 3;
            loadButton.FontSize = h / 3;
            margin.Top += h;

            backButton.Margin = margin;
            backButton.Height = h;
            backButton.Width = h * 3;
            backButton.FontSize = h / 3;
        }

        static public void Destruct()
        {
            MainWindow.SizeChanged -= windowSizeChanged;

            consoleMenu.Destruct();
            consoleMenu = null;

            newGameButton = null;
            loadButton = null;
            backButton = null;
        }
    }
}
