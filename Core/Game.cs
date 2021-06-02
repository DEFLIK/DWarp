using System;
using System.Timers;
using DWarp.Core.Drawing;
using DWarp.Core.Models;
using DWarp.Resources.Levels;

namespace DWarp.Core
{
    public static class Game
    {
        public static MainForm mainForm;
        /// <summary>
        /// Подготавливает игру и основную форму
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Форма, в которой отображается игра</returns>
        public static MainForm Initialize(Level level)
        {
            mainForm = new MainForm(new State(level));
            return mainForm;
        }

        public static void ChangeLevel(Level level)
        {
            if (mainForm.CurrentState != null)
                mainForm.CurrentState.Dispose();
            var newState = new State(level);
            mainForm.ChangeState(newState);
            GC.Collect();
        }
    }
}
