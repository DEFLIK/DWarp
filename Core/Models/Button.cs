using System.Drawing;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Drawing;

namespace DWarp.Core.Models
{
    public class Button : Creature
    {
        private static Bitmap pressedImage = Properties.Resources.ButtonT;
        private static Bitmap unpressedImage = Properties.Resources.ButtonF;
        public Door LinkedDoor;
        public bool Pressed { get; private set; }
        public Button(Bitmap image) : base(CreatureType.Button, image) { }

        public void Update()
        {
            Pressed = Game.Cubes[Location.X, Location.Y] is Cube;
            Sprite.Image = Pressed ? pressedImage : unpressedImage;
            LinkedDoor.Opened = Pressed;
            if (Pressed)
                Animations.Open(LinkedDoor, 10);
            else
                Animations.Close(LinkedDoor, 10);
        }
    }
}
