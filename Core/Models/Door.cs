using System.Drawing;

namespace DWarp
{
    public class Door : Creature
    {
        public bool Opened;
        public Door(Bitmap image) : base(CreatureType.Door, image) { }
    }
}
