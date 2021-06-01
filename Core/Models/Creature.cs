using System.Drawing;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Drawing;

namespace DWarp.Core.Models
{
    public class Creature
    {
        public Sprite Sprite;
        public Point Location = new Point();
        public readonly CreatureType Type;
        public readonly bool IsDynamic;

        public Creature(CreatureType type, Bitmap image, int drawingLayer = 0, bool isDynamic = false)
        {
            Type = type;
            Sprite = new Sprite(new Rectangle(), image, this, drawingLayer);
            IsDynamic = isDynamic;
        }
    }
}
