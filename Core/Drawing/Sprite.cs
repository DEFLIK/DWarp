using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    public class Sprite
    {
        public Rectangle Rectangle;
        public Bitmap Image;
        public readonly Creature Owner;
        public bool Visible = true;
        public (int X, int Y) Offset = (0, 0);
        public (int Width, int Height) SizePercent = (100, 100);
        public readonly int DrawingLayer;

        public Sprite(Rectangle rec, Bitmap img, Creature owner, int drawingLayer = 0)
        {
            Rectangle = rec;
            Image = img;
            Owner = owner;
            DrawingLayer = drawingLayer;
        }
    }
}
