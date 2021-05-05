using System.Drawing;
using DWarp.Core;
using DWarp.Core.Controls;
using DWarp.Resources.Levels;
using NUnit.Framework;

namespace DWarp
{
    [TestFixture]
	public class Player_Should //ToRefactor...
	{
		private static string moveTestMap =  @"
P..
...
...
";

		[Test]
		public void DoBasicMoves()
		{
			Game.Load(new Level(moveTestMap,100));
			PlayerCommands.Move(0, 1);
			PlayerCommands.Move(1, 0);
			PlayerCommands.Move(1, 0);

			Assert.AreEqual(new Point(2, 1), Game.Player.Location);
		}

		[Test]
		public void TryMoveOutOfUpBounds()
		{
			Game.Load(new Level(moveTestMap, 100));
			PlayerCommands.Move(-1, 0);
			PlayerCommands.Move(0, -1);

			Assert.AreEqual(new Point(0, 0), Game.Player.Location);
		}
	}
}