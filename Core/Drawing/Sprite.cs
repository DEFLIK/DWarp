using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    public class Sprite
    {
        //public static (int X, int Y) GlobalOffset = (0, 0);
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
