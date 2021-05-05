using DWarp.Core.Controls.Factorys;
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

        public bool Execute()
        {
            if (!CanStandAt(warpedPlayer.Location.X + deltaX, warpedPlayer.Location.Y + deltaY))
                return false;
            warpedPlayer.Location.X += deltaX;
            warpedPlayer.Location.Y += deltaY;
            return true;
        }

        public bool Undo()
        {
            if (!CanStandAt(warpedPlayer.Location.X - deltaX, warpedPlayer.Location.Y - deltaY))
                return false;
            warpedPlayer.Location.X -= deltaX;
            warpedPlayer.Location.Y -= deltaY;
            return true;
        }

        private static bool CanStandAt(int x, int y)
        {
            return Game.Map[x, y].Type == CreatureType.Door ? (Game.Map[x, y] as Door).Opened : true;
        }
    }

    public class TakeCube : ICommand
    {
        private readonly Player warpedPlayer;
        public TakeCube(Player warpedPlayer)
        {
            this.warpedPlayer = warpedPlayer;
        }

        public bool Execute()
        {
            CubeActions.Take(warpedPlayer);
            return true;
        }

        public bool Undo()
        {
            CubeActions.Place(warpedPlayer);
            return true;
        }
    }
    public class PlaceCube : ICommand
    {
        private readonly Player warpedPlayer;
        public PlaceCube(Player warpedPlayer)
        {
            this.warpedPlayer = warpedPlayer;
        }

        public bool Execute()
        {
            CubeActions.Place(warpedPlayer);
            return true;
        }

        public bool Undo()
        {
            CubeActions.Take(warpedPlayer);
            return true;
        }
    }
}
