using System.Drawing;

namespace DWarp
{
    public class Player : Creature
    {
        public Cube PickedCube;
        public Player(Bitmap image) : base(CreatureType.Player, image) { }
    }
}
