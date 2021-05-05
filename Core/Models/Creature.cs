using System.Drawing;

namespace DWarp
{
    public class Creature
    {
        public Sprite Sprite;
        public Point Location = new Point();
        public readonly CreatureType Type;

        public Creature(CreatureType type, Bitmap image)
        {
            Type = type;
            Sprite = new Sprite(new Rectangle(), image, this);
        }
    }
}
