using System.Drawing;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Drawing;

namespace DWarp.Core.Models
{
    public class Button : Creature
    {
        private static readonly Bitmap pressedImage = Properties.Resources.ButtonT;
        private static readonly Bitmap unpressedImage = Properties.Resources.ButtonF;
        public Door LinkedDoor;
        public bool Pressed { get; private set; }
        public Button(Bitmap image) : base(CreatureType.Button, image, 1, true) 
        {
            pressedImage.MakeTransparent();
            unpressedImage.MakeTransparent();
        }

        public void Update(State state)
        {
            Pressed = state.Cubes[Location.X, Location.Y] is Cube;
            Sprite.Image = Pressed ? pressedImage : unpressedImage;
            LinkedDoor.Opened = Pressed;
            if (Pressed)
                Animations.Open(LinkedDoor, 1);
            else
                Animations.Close(LinkedDoor, 1);
        }
    }
}
