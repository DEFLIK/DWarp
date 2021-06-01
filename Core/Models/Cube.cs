using System.Drawing;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Models
{
    public class Cube : Creature // ToUpgrade...
    {
        public readonly Point RespawnLocation;
        public Cube(Bitmap image, Point respawnPoint) : base(CreatureType.CubeSpawn, image, 2, true) 
        {
            RespawnLocation = respawnPoint;
        }
    }
}
