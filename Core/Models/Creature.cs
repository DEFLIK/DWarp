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

        public Creature(CreatureType type, Bitmap image)
        {
            Type = type;
            Sprite = new Sprite(new Rectangle(), image, this);
        }
    }
}
