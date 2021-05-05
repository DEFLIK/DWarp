using System;
using System.Windows.Forms;
using DWarp.Core;

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
            Game.Load(Game.CurrentLevel);
            Application.Run(new MainForm());
        }
    }
}
