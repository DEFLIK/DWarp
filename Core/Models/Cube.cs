using System.Drawing;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Models
{
    public class Cube : Creature // ToUpgrade...
    {
        public Cube(Bitmap image) : base(CreatureType.CubeSpawn, image) { }
    }
}
