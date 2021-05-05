using System.Drawing;

namespace DWarp
{
    public class Sprite
    {
        public Rectangle Rectangle;
        public Bitmap Image;
        public readonly Creature Owner;
        public (int X, int Y) Offset = (0, 0);
        public (int Width, int Height) SizeOffset = (0, 0);

        public Sprite(Rectangle rec, Bitmap img, Creature owner)
        {
            Rectangle = rec;
            Image = img;
            Owner = owner;
        }
    }
}
