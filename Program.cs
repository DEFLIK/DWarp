using System;
using System.Linq;
using System.Windows.Forms;
using DWarp.Core;
using DWarp.Resources.Levels;

namespace DWarp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Game.Load(Presets.Levels.First().Value);
            Application.Run(new MainForm());
        }
    }
}
