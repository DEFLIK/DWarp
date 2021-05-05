using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public class Move : ICommand
    {
        private readonly Creature warpedPlayer;
        private readonly int deltaX;
        private readonly int deltaY;

        public Move(Creature warpedPlayer, int deltaX, int deltaY)
        {
            this.warpedPlayer = warpedPlayer;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }

        public void Execute()
        {
            warpedPlayer.Location.X += deltaX;
            warpedPlayer.Location.Y += deltaY;
        }

        public void Undo()
        {
            warpedPlayer.Location.X -= deltaX;
            warpedPlayer.Location.Y -= deltaY;
        }
    }

    public class TakeCube : ICommand
    {
        private readonly Player warpedPlayer;
        public TakeCube(Player warpedPlayer)
        {
            this.warpedPlayer = warpedPlayer;
        }

        public void Execute()
        {
            CubeActions.Take(warpedPlayer);
        }

        public void Undo()
        {
            CubeActions.Place(warpedPlayer);
        }
    }
    public class PlaceCube : ICommand
    {
        private readonly Player warpedPlayer;
        public PlaceCube(Player warpedPlayer)
        {
            this.warpedPlayer = warpedPlayer;
        }

        public void Execute()
        {
            CubeActions.Place(warpedPlayer);
        }

        public void Undo()
        {
            CubeActions.Take(warpedPlayer);
        }
    }
}
