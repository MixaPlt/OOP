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
    static class EnterNameWindow
    {
        private static Canvas MainCanvas = OOP.Resources.MainCanvas;
        private static Window MainWindow = OOP.Resources.MainWindow;
        private static Button sumbitButton;
        private static TextBox nameBox;
        private static Label enterNameLabel;

        public static void Build()
        {
            MainCanvas.Children.Clear();

            enterNameLabel = new Label() { Content = Lang.EnterYourName, Foreground = Brushes.LawnGreen };
            MainCanvas.Children.Add(enterNameLabel);
            enterNameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;

            nameBox = new TextBox() { Foreground = Brushes.Green, Background = Brushes.Black, MaxLength = 15};
            MainCanvas.Children.Add(nameBox);
            nameBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            nameBox.VerticalAlignment = VerticalAlignment.Top;

            nameBox.Text = "MoyeimyaVanster!";

            sumbitButton = new Button() { Content = Lang.Sumbit };
            sumbitButton.Click += sumbit;
            MainCanvas.Children.Add(sumbitButton);

            MainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }
        private static void windowSizeChanged(object sender, EventArgs e)
        {
            double windowHeight = MainCanvas.ActualHeight;
            double windowWidth = MainCanvas.ActualWidth;
            double h = (int)Math.Min(windowHeight / 5.5, windowWidth / 4.5);
            h = Math.Max(h, 1);
            Thickness margin = new Thickness((windowWidth - h * 4) / 2, (windowHeight - h * 4) / 2, (windowWidth - h * 4) / 2, (windowHeight - h * 5) / 2);
            enterNameLabel.Margin = margin;
            enterNameLabel.Width = h * 4;
            enterNameLabel.FontSize = h / 2.2;
            margin.Top += h;
            nameBox.Margin = margin;
            nameBox.Height = h / 1.5;
            nameBox.FontSize = h / 2.2;
            nameBox.Width = h * 4;
            margin.Top += nameBox.Height;
            sumbitButton.Margin = margin;
            sumbitButton.Height = h / 1.7;
            sumbitButton.FontSize = h / 2.5;
            sumbitButton.Width = h * 4;
        }
        private static void sumbit(object sender, EventArgs e)
        {
            string name = nameBox.Text;
            if (name.Length == 0)
                return;
            Resources.PlayerName = name;
            Destruct();
            MainMenu.Build();
        }
        private static void Destruct()
        {
            MainWindow.SizeChanged -= windowSizeChanged;
            sumbitButton.Click -= sumbit;
            sumbitButton = null;
            enterNameLabel = null;
            nameBox = null;
        }
    }
}
