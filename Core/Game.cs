using System;
using DWarp.Core.Drawing;
using DWarp.Core.Models;
using DWarp.Resources.Levels;

namespace DWarp.Core
{
    public static class Game
    {
        public static MainForm MainForm;
        /// <summary>
        /// Подготавливает игру и основную форму
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Форма, в которой отображается игра</returns>
        public static MainForm Initialize(Level level)
        {
            MainForm = new MainForm(new State(level));
            return MainForm;
        }

        public static void ChangeLevel(Level level)
        {
            if (MainForm.CurrentState != null)
                MainForm.CurrentState.Dispose();
            var newState = new State(level);
            MainForm.ChangeState(newState);
            GC.Collect();
        }
    }
}
