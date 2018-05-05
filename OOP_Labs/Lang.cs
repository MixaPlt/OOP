using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    static class Lang
    {
        //A list of available languages
        public static readonly String[] Languages = { "English (UK)", "Russian" };
        //Selected language
        public static UInt16 SelectedIndex { get; set; }

        public static string StartGame { get { return getWord(0); } }
        public static string GlobalSettings { get { return getWord(1); } }
        public static string Copyrights { get { return getWord(2); } }
        public static string ExitGame { get { return getWord(3); } }
        public static string StartNewGame { get { return getWord(4); } }
        public static string LoadSave { get { return getWord(5); } }
        public static string Back { get { return getWord(6); } }
        public static string MapEditor { get { return getWord(7); } }
        public static string GenerateMap { get { return getWord(8); } }

        private static string getWord(UInt16 num)
        {
            return words[num, SelectedIndex];
        }
        private static readonly string[,] words = 
        {
            { "Play",           "Играть"},
            { "Settings",       "null"},
            { "Copyrights",     "null"},
            { "Exit game",      "null"},
            { "Start new game", "null"},
            { "Load save",      "null"},
            { "Back",           "null"},
            { "Map editor",     "null"},
            { "Generate map",   "null"},
        };

    }
}
