//using System.Drawing;
//using DWarp.Core;
//using DWarp.Core.Controls;
//using DWarp.Core.Models;
//using DWarp.Resources.Levels;
//using NUnit.Framework;

//namespace DWarp
//{
//	[TestFixture]
//	public class Player_Should //ToRefactor...
//	{
//		private static string moveTestMap = @"
//P..
//...
//...
//";

//		private static string wireTestMap = @"
//PC.
//.BD
//...
//";
//		private static Wire[] wireTestMapWire = new Wire[] { new Wire(new Point(1, 1), new Point(2, 1)) };

//		[Test]
//		public void DoBasicMoves()
//		{
//			Game.LoadLevel(new Level(moveTestMap, 100));
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.Move(1, 0);
//			PlayerCommands.Move(1, 0);

//			Assert.AreEqual(new Point(2, 1), Game.Player.Location);
//		}

//		[Test]
//		public void MoveOutOfUpBounds()
//		{
//			Game.LoadLevel(new Level(moveTestMap, 100));
//			PlayerCommands.Move(-1, 0);
//			PlayerCommands.Move(0, -1);

//			Assert.AreEqual(new Point(0, 0), Game.Player.Location);
//		}

//		[Test]
//		public void WarpBack()
//		{
//			Game.LoadLevel(new Level(moveTestMap, 100));
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.Move(0, 1);
//			Game.DoWarp();
//			Game.CommandsStack.RollBack();
//			Assert.AreEqual(new Point(0, 1), Game.WarpedPlayer.Location);
//			Game.WarpPlayer();
//			Assert.AreEqual(new Point(0, 1), Game.Player.Location);
//		}

//		[Test]
//		public void WarpForward()
//		{
//			Game.LoadLevel(new Level(moveTestMap, 100));
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.Move(1, 0);
//			Game.DoWarp();
//			Game.CommandsStack.RollBack();
//			Game.CommandsStack.RollBack();
//			Game.CommandsStack.RollForward();
//			Assert.AreEqual(new Point(0, 2), Game.WarpedPlayer.Location);
//			Game.WarpPlayer();
//			Assert.AreEqual(new Point(0, 2), Game.Player.Location);
//		}

//		[Test]
//		public void MoveCube()
//		{
//			Game.LoadLevel(new Level(wireTestMap, 100, 0, wireTestMapWire));
//			var cube = Game.Cubes[1, 0];
//			PlayerCommands.Move(1, 0);
//			PlayerCommands.TakeCube();
//			Assert.AreEqual(Game.Cubes[1, 0], null);
//			Assert.AreEqual(Game.Player.PickedCube, cube);
//			PlayerCommands.Move(-1, 0);
//			PlayerCommands.PlaceCube();
//			Assert.AreEqual(Game.Cubes[0, 0], cube);
//			Assert.AreEqual(Game.Player.PickedCube, null);
//		}

//		[Test]
//		public void PressButton()
//		{
//			Game.LoadLevel(new Level(wireTestMap, 100, 0, wireTestMapWire));
//			PlayerCommands.Move(1, 0);
//			PlayerCommands.TakeCube();
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.PlaceCube();
//			Assert.AreEqual(((Button)Game.Map[1, 1]).Pressed, true);
//			PlayerCommands.TakeCube();
//			Assert.AreEqual(((Button)Game.Map[1, 1]).Pressed, false);
//		}

//		[Test]
//		public void ManipulateDoorByButtonWithButton()
//		{
//			Game.LoadLevel(new Level(wireTestMap, 100, 0, wireTestMapWire));
//			PlayerCommands.Move(1, 0);
//			PlayerCommands.TakeCube();
//			PlayerCommands.Move(0, 1);
//			PlayerCommands.PlaceCube();
//			Assert.AreEqual(((Door)Game.Map[2, 1]).Opened, true);
//			PlayerCommands.TakeCube();
//			Assert.AreEqual(((Door)Game.Map[2, 1]).Opened, false);
//		}
//	}
//}