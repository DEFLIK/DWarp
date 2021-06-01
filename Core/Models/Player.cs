using System.Drawing;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Models
{
    public class Player : Creature
    {
        public Cube PickedCube;
        public Player(Bitmap image, int drawingLayer = 4) : base(CreatureType.Player, image, drawingLayer, true) { }
    }
}
