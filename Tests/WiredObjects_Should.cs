using System.Drawing;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DWarp_Tests
{

    [TestClass]
    public class WiredObjects_Should
    {
        private static readonly string wiredTestMap = @"
PC.
.BD
...
";
        private static Wire[] wiredTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

        [TestMethod]
        public void PressButton()
        {
            var state = new State(new Level(wiredTestMap, 100, null, 0, wiredTestMapWire));
            PlayerCommands.Move(state, 1, 0);
            PlayerCommands.TakeCube(state);
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.PlaceCube(state);
            Assert.AreEqual(((Button)state.Map[1, 1]).Pressed, true);
            PlayerCommands.TakeCube(state);
            Assert.AreEqual(((Button)state.Map[1, 1]).Pressed, false);
        }

        [TestMethod]
        public void ManipulateDoorByButtonWithButton()
        {
            var state = new State(new Level(wiredTestMap, 100, null, 0, wiredTestMapWire));
            PlayerCommands.Move(state, 1, 0);
            PlayerCommands.TakeCube(state);
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.PlaceCube(state);
            Assert.AreEqual(((Door)state.Map[2, 1]).Opened, true);
            PlayerCommands.TakeCube(state);
            Assert.AreEqual(((Door)state.Map[2, 1]).Opened, false);
        }
    }
}
