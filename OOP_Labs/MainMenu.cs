﻿using System;
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
        private static Button startGameButton;
        private static Button globalSettingsButton;
        private static Button copyrightsButton;
        private static Button exitGameButton;

        private static ConsoleMenu consoleMenu;

        public static void Build()
        {
            MainCanvas.Children.Clear();
            startGameButton = new Button() { Content = Lang.StartGame };
            globalSettingsButton = new Button() { Content = Lang.GlobalSettings };
            copyrightsButton = new Button() { Content = Lang.Copyrights };
            exitGameButton = new Button() { Content = Lang.ExitGame };
            //Add buttons
            MainCanvas.Children.Add(startGameButton);
            MainCanvas.Children.Add(globalSettingsButton);
            MainCanvas.Children.Add(copyrightsButton);
            MainCanvas.Children.Add(exitGameButton);

            startGameButton.FontWeight = FontWeights.Medium;
            globalSettingsButton.FontWeight = FontWeights.Medium;
            copyrightsButton.FontWeight = FontWeights.Medium;
            exitGameButton.FontWeight = FontWeights.Medium;

            windowSizeChanged(MainWindow, null);
            MainWindow.SizeChanged += windowSizeChanged;

            startGameButton.Click += startGame;
            globalSettingsButton.Click += startSettings;
            copyrightsButton.Click += startCopyright;
            exitGameButton.Click += exitGame;

            string[] consoleItems = { Lang.StartGame, Lang.GlobalSettings, Lang.Copyrights, Lang.ExitGame};
            consoleMenu = new ConsoleMenu(consoleItems, "");

            consoleMenu.FirstItemSelected += startGame;
            consoleMenu.SecondItemSelected += startSettings;
            consoleMenu.ThirdItemSelected += startCopyright;
            consoleMenu.FourthItemSelected += exitGame;

        }

        private static void windowSizeChanged(object sender, EventArgs e)
        {
            double windowHeight = MainCanvas.ActualHeight;
            double windowWidth = MainCanvas.ActualWidth;
            double h = Math.Min(windowHeight / 4.5, windowWidth / 3.5);
            h = Math.Max(h, 1);
            //margin
            Thickness margin = new Thickness((windowWidth - h * 3) / 2, windowHeight / 2 - h * 2, (windowWidth - h * 3) / 2, windowHeight / 2 - h * 2);
            startGameButton.Margin = margin;
            startGameButton.Height = h;
            startGameButton.Width = h * 3;
            startGameButton.FontSize = h / 3;
            margin.Top += h;

            globalSettingsButton.Margin = margin;
            globalSettingsButton.Height = h;
            globalSettingsButton.Width = h * 3;
            globalSettingsButton.FontSize = h / 3;
            margin.Top += h;

            copyrightsButton.Margin = margin;
            copyrightsButton.Height = h;
            copyrightsButton.Width = h * 3;
            copyrightsButton.FontSize = h / 3;
            margin.Top += h;

            exitGameButton.Margin = margin;
            exitGameButton.Height = h;
            exitGameButton.Width = h * 3;
            exitGameButton.FontSize = h / 3;
        }

        private static void startGame(object sender, EventArgs e)
        {
            Destuct();
            
        }

        private static void startSettings(object sender, EventArgs e)
        {

        }

        private static void startCopyright(object sender, EventArgs e)
        {

        }

        private static void exitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private static void Destuct()
        {
            MainWindow.SizeChanged -= windowSizeChanged;
            MainCanvas.Children.Clear();
            startGameButton = null;
            globalSettingsButton = null;
            copyrightsButton = null;
            exitGameButton = null;

            consoleMenu.Destruct();
            consoleMenu = null;

            StartGameMenu.Build();
        }
    }
}
