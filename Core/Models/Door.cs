using System.Drawing;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Models
{
    public class Door : Creature
    {
        public bool Opened;
        public Door(Bitmap image) : base(CreatureType.Door, image) { }
    }
}
