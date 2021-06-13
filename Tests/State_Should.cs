using System.Drawing;
using DWarp.Core;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DWarp_Tests
{
    [TestClass]
    public class State_Should
    {
        private static readonly string moveTestMap = @"
P..
...
...
";

        [TestMethod]
        public void WarpBack()
        {
            var state = new State(new Level(moveTestMap, 100));
            Game.Initialize(state.CurrentLevel);
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 0, 1);
            state.DoWarp();
            state.CommandsStack.RollBack();
            Assert.AreEqual(new Point(0, 1), state.WarpedPlayer.Location);
            state.WarpPlayer();
            Assert.AreEqual(new Point(0, 1), state.Player.Location);
        }

        [TestMethod]
        public void WarpForward()
        {
            var state = new State(new Level(moveTestMap, 100));
            Game.Initialize(state.CurrentLevel);
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 1, 0);
            state.DoWarp();
            state.CommandsStack.RollBack();
            state.CommandsStack.RollBack();
            state.CommandsStack.RollForward();
            Assert.AreEqual(new Point(0, 2), state.WarpedPlayer.Location);
            state.WarpPlayer();
            Assert.AreEqual(new Point(0, 2), state.Player.Location);
        }
    }
}