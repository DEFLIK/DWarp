using System.Timers;
using System.Drawing;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace DWarp_Tests
{
    [TestFixture]
    public class Dummy_Should //ToRefactor...
    {

        private static string wiredTestMap = @"
PC.
.BD
K..
";
        private static Wire[] wiredTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

        [Test]
        public void ReturnCubeToInitialPosition()
        {
            var state = new State(new Level(wiredTestMap, 100, 0, wiredTestMapWire));
            Assert.IsTrue(CubeActions.Move(state, new Point(1, 0), new Point(2, 2)));
            Assert.AreEqual(state.Cubes[1, 0], null);
            state.Dummy.BeginWalk(state, 1);
            CheckForCubeAndDummyReturned(state, state.Cubes[1, 0]);
        }

        private void CheckForCubeAndDummyReturned(State state, Cube cube)
        {
            Assert.AreNotEqual(cube, null);
            Assert.AreEqual(state.Dummy.Location, state.Dummy.RespawnLocation);
        }
    }
}