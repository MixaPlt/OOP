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
        public static void Init(MainWindow mainWindow, Canvas mainCanvas)
        {
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            Lang.SelectedIndex = 0;
        }
    }
}
