using System.Drawing;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using NUnit.Framework;

namespace DWarp_Tests
{
    [TestFixture]
    public class State_Should
    {
        private static string moveTestMap = @"
P..
...
...
";

        [Test]
        public void WarpBack()
        {
            var state = new State(new Level(moveTestMap, 100));
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 0, 1);
            state.DoWarp();
            state.CommandsStack.RollBack();
            Assert.AreEqual(new Point(0, 1), state.WarpedPlayer.Location);
            state.WarpPlayer();
            Assert.AreEqual(new Point(0, 1), state.Player.Location);
        }

        [Test]
        public void WarpForward()
        {
            var state = new State(new Level(moveTestMap, 100));
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