using DWarp.Core.Controls.Factorys;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public class Move : ICommand
    {
        private readonly State state;
        private readonly Creature warpedPlayer;
        private readonly int deltaX;
        private readonly int deltaY;

        public Move(State state, Creature warpedPlayer, int deltaX, int deltaY)
        {
            this.state = state;
            this.warpedPlayer = warpedPlayer;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }

        public bool Execute()
        {
            if (!CanStandAt(warpedPlayer.Location.X + deltaX, warpedPlayer.Location.Y + deltaY))
                return false;
            warpedPlayer.Location.X += deltaX;
            warpedPlayer.Location.Y += deltaY;
            state.soundPlayer.PlayAsync("WarpedStep");
            return true;
        }

        public bool Undo()
        {
            if (!CanStandAt(warpedPlayer.Location.X - deltaX, warpedPlayer.Location.Y - deltaY))
                return false;
            warpedPlayer.Location.X -= deltaX;
            warpedPlayer.Location.Y -= deltaY;
            state.soundPlayer.PlayAsync("WarpedStep");
            return true;
        }

        private bool CanStandAt(int x, int y)
        {
            return state.Map[x, y].Type == CreatureType.Door ? (state.Map[x, y] as Door).Opened : true;
        }
    }

    public class TakeCube : ICommand
    {
        private readonly State state;
        private readonly Player warpedPlayer;
        public TakeCube(State state, Player warpedPlayer)
        {
            this.state = state;
            this.warpedPlayer = warpedPlayer;
        }

        public bool Execute()
        {
            CubeActions.Take(state, warpedPlayer);
            return true;
        }

        public bool Undo()
        {
            CubeActions.Place(state, warpedPlayer);
            return true;
        }
    }
    public class PlaceCube : ICommand
    {
        private readonly State state;
        private readonly Player warpedPlayer;
        public PlaceCube(State state, Player warpedPlayer)
        {
            this.state = state;
            this.warpedPlayer = warpedPlayer;
        }

        public bool Execute()
        {
            CubeActions.Place(state, warpedPlayer);
            return true;
        }

        public bool Undo()
        {
            CubeActions.Take(state, warpedPlayer);
            return true;
        }
    }
}
