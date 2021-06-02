using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using System.Drawing;

namespace DWarp_Tests
{
    [TestClass]
    public class Player_Should
    {
        private static readonly string moveTestMap = @"
P..
...
...
";

        private static readonly string wiredTestMap = @"
PC.
.BD
...
";
        private static readonly Wire[] wiredTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

        [TestMethod]
        public void DoBasicMoves()
        {
            var state = new State(new Level(moveTestMap, 100));
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 1, 0);
            PlayerCommands.Move(state, 1, 0);

            Assert.AreEqual(new Point(2, 1), state.Player.Location);
        }

        [TestMethod]
        public void MoveOutOfUpBounds()
        {
            var state = new State(new Level(moveTestMap, 100));
            PlayerCommands.Move(state, -1, 0);
            PlayerCommands.Move(state, 0, -1);

            Assert.AreEqual(new Point(0, 0), state.Player.Location);
        }

        [TestMethod]
        public void MoveCube()
        {
            var state = new State(new Level(wiredTestMap, 100, 0, wiredTestMapWire));
            var cube = state.Cubes[1, 0];
            PlayerCommands.Move(state, 1, 0);
            PlayerCommands.TakeCube(state);
            Assert.AreEqual(state.Cubes[1, 0], null);
            Assert.AreEqual(state.Player.PickedCube, cube);
            PlayerCommands.Move(state, -1, 0);
            PlayerCommands.PlaceCube(state);
            Assert.AreEqual(state.Cubes[0, 0], cube);
            Assert.AreEqual(state.Player.PickedCube, null);
        }
    }
}