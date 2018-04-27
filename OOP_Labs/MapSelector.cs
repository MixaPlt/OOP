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
    static class MapSelector
    {
        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;

        private static Button startGameButton;
        private static Button backButton;

        private static ScrollViewer scrollViewer;
        private static string[] names;

        private static ListBox loadList;

        private static ConsoleMenu consoleMenu;

        public static void Build()
        {
            MainCanvas.Children.Clear();
            startGameButton = new Button() { Content = Lang.StartGame };
            backButton = new Button() { Content = Lang.Back };
            try
            {
                names = System.IO.Directory.GetFiles("maps");
            }
            catch
            {
                System.IO.Directory.CreateDirectory("maps");
            }

            for (int i = 0; i < names.GetLength(0); i++)
                names[i] = names[i].Split('\\', '.')[1];

            loadList = new ListBox() { ItemsSource = names };

            scrollViewer = new ScrollViewer() { Content = loadList };

            MainCanvas.Children.Add(scrollViewer);
            MainCanvas.Children.Add(backButton);
            MainCanvas.Children.Add(startGameButton);

            MainWindow.SizeChanged += windowSizeChanged;
            loadList.HorizontalContentAlignment = HorizontalAlignment.Center;
            windowSizeChanged(null, null);

            backButton.Click += back;
            startGameButton.Click += start;

            consoleMenu = new ConsoleMenu(names, "");
            consoleMenu.ItemSelected += consoleStart;
            
        }

        private static void back(object sender, EventArgs e)
        {
            Destruct();
            StartGameMenu.Build();
        }

        private static void consoleStart(object sender, EventArgs e)
        {
            loadList.SelectedIndex = consoleMenu.SelectedItem;
            start(consoleMenu, e);
        }

        private static void start(object sender, EventArgs e)
        {
            string s;
            if (loadList.SelectedItem == null)
                return;
            try
            {
                s = "maps\\" + loadList.SelectedItem + ".map";
            }
            catch
            {
                return;
            }
            Destruct();
            Game.Build(s);
        }

        private static void windowSizeChanged(object sender, EventArgs e)
        {
            double windowHeight = MainCanvas.ActualHeight;
            double windowWidth = MainCanvas.ActualWidth;
            double h = Math.Min(windowHeight / 5.5, windowWidth / 3.5);
            Thickness margin = new Thickness((windowWidth - h * 3) / 2, (windowHeight - h * 5) / 2, (windowWidth - h * 3) / 2, (windowHeight - h * 5) / 2);
            scrollViewer.Margin = margin;
            scrollViewer.Width = h * 3;
            scrollViewer.Height = h * 4;
            loadList.FontSize = h / 3;
            margin.Top += h * 4;

            backButton.Margin = margin;
            backButton.Height = h / 1.5;
            backButton.Width = h * 1.5;
            backButton.FontSize = h / 3;

            margin.Left += h * 1.5;
            startGameButton.Margin = margin;
            startGameButton.Height = h / 1.5;
            startGameButton.Width = h * 1.5;
            startGameButton.FontSize = h / 3;

        }

        public static void Destruct()
        {
            consoleMenu.Destruct();
            consoleMenu = null;
            MainCanvas.Children.Clear();
            MainWindow.SizeChanged -= windowSizeChanged;
            startGameButton = null;
            backButton = null;
            scrollViewer = null;
            loadList = null;
        }
    }
}
