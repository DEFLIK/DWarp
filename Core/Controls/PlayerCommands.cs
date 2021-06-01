using DWarp.Core.Controls.Factorys;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public static class PlayerCommands
    {
        public static void Move(State state, int deltaX, int deltaY)
        {
            if (state.CommandsStack.Stack.Count == 0 && state.CurrentLevel.TimeLimit > 0)
                state.StartTimer();
            if (state.CommandsStack.Stack.Count >= state.CommandsStack.Limit)
                return;
            var resPosX = state.Player.Location.X + deltaX;
            var resPosY = state.Player.Location.Y + deltaY;
            var mapSize = state.Map.GetLength(0);
            if (resPosX < mapSize && resPosY < mapSize && resPosX >= 0 && resPosY >= 0 && state.Map[resPosX, resPosY].Type != CreatureType.Wall)
            {
                if (state.Map[resPosX, resPosY].Type == CreatureType.Door && !(state.Map[resPosX, resPosY] as Door).Opened)
                    return;
                state.CommandsStack.AddCommand(new Move(state, state.WarpedPlayer, deltaX, deltaY));
                state.Player.Location.X = resPosX;
                state.Player.Location.Y = resPosY;
                if (state.Map[resPosX, resPosY].Type == CreatureType.Exit)
                    Game.ChangeLevel(state.CurrentLevel);
            }
        }

        public static void TakeCube(State state)
        {
            var commandCompleted = CubeActions.Take(state, state.Player);

            if (commandCompleted && !state.IsWarped)
            {
                var warpedPlayeCommand = new TakeCube(state, state.WarpedPlayer);
                state.CommandsStack.AddCommand(warpedPlayeCommand);
            }
        }

        public static void PlaceCube(State state)
        {
            var commandCompleted = CubeActions.Place(state, state.Player);

            if (commandCompleted && !state.IsWarped)
            {
                var warpedPlayeCommand = new PlaceCube(state, state.WarpedPlayer);
                state.CommandsStack.AddCommand(warpedPlayeCommand);
            }
        }
    }
}
