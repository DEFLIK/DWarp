using System.Timers;
using System.Drawing;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using NUnit.Framework;

namespace DWarp_Tests
{
    [TestFixture]
    public class Player_Should //ToRefactor...
    {
        private static string moveTestMap = @"
P..
...
...
";

        private static string wireTestMap = @"
PC.
.BD
...
";
        private static Wire[] wireTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

        [Test]
        public void DoBasicMoves()
        {
            var state = new State(new Level(moveTestMap, 100));
            PlayerCommands.Move(state, 0, 1);
            PlayerCommands.Move(state, 1, 0);
            PlayerCommands.Move(state, 1, 0);

            Assert.AreEqual(new Point(2, 1), state.Player.Location);
        }

        [Test]
        public void MoveOutOfUpBounds()
        {
            var state = new State(new Level(moveTestMap, 100));
            PlayerCommands.Move(state, -1, 0);
            PlayerCommands.Move(state, 0, -1);

            Assert.AreEqual(new Point(0, 0), state.Player.Location);
        }

        [Test]
        public void MoveCube()
        {
            var state = new State(new Level(wireTestMap, 100, 0, wireTestMapWire));
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