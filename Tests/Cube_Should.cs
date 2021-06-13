using System.Drawing;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DWarp_Tests
{
    [TestClass]
    public class Cube_Should
    {
        private static string wiredTestMap = @"
PC.
.BD
...
";
        private static Wire[] wiredTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

        [TestMethod]
        public void Move()
        {
            var state = new State(new Level(wiredTestMap, 100, null, 0, wiredTestMapWire));
            var cube = state.Cubes[1, 0];
            CubeActions.Move(state, cube.Location, new Point(2, 2));
            Assert.AreEqual(cube.Location, new Point(2,2));
        }
    }
}