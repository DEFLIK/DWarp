using DWarp.Core.Controls.Factorys;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public static class PlayerCommands
    {
        public static void Move(int deltaX, int deltaY)
        {
            if (Game.CommandsStack.Stack.Count == 0 && Game.CurrentLevel.TimeLimit > 0)
                Game.StartTimer();
            if (Game.CommandsStack.Stack.Count >= Game.CommandsStack.Limit)
                return;
            var resPosX = Game.Player.Location.X + deltaX;
            var resPosY = Game.Player.Location.Y + deltaY;
            var mapSize = Game.Map.GetLength(0);
            if (resPosX < mapSize && resPosY < mapSize && resPosX >= 0 && resPosY >= 0 && Game.Map[resPosX, resPosY].Type != CreatureType.Wall)
            {
                if (Game.Map[resPosX, resPosY].Type == CreatureType.Door && !(Game.Map[resPosX, resPosY] as Door).Opened)
                    return;
                Game.CommandsStack.AddCommand(new Move(Game.WarpedPlayer, deltaX, deltaY));
                Game.Player.Location.X = resPosX;
                Game.Player.Location.Y = resPosY;
            }
        }

        public static void TakeCube()
        {
            var commandCompleted = CubeActions.Take(Game.Player);

            if (commandCompleted)
            {
                var warpedPlayeCommand = new TakeCube(Game.WarpedPlayer);
                Game.CommandsStack.AddCommand(warpedPlayeCommand);
            }
        }

        public static void PlaceCube()
        {
            var commandCompleted = CubeActions.Place(Game.Player);

            if (commandCompleted)
            {
                var warpedPlayeCommand = new PlaceCube(Game.WarpedPlayer);
                Game.CommandsStack.AddCommand(warpedPlayeCommand);
            }
        }
    }
}
