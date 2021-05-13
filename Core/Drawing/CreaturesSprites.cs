using System.Collections.Generic;
using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    public static class CreaturesSprites
    {
        public static List<Creature> Dynamic = new List<Creature>();
        public static List<Creature> Static = new List<Creature>();

        public static void Load()
        {
            Dynamic.Clear();
            Static.Clear();
            Dynamic.Add(Game.Player);
            Game.Player.Sprite.Image.MakeTransparent(Color.White);
            Dynamic.Add(Game.WarpedPlayer);
            SetAlphaBending(Game.WarpedPlayer.Sprite.Image, 140);
            Game.WarpedPlayer.Sprite.Image.MakeTransparent(Color.White);
            foreach (var cube in Game.Cubes)
                if (cube != null)
                {
                    Dynamic.Add(cube);
                    cube.Sprite.Image.MakeTransparent(Color.White);
                }
            foreach (var creature in Game.Map)
            {
                creature.Sprite.Image.MakeTransparent(Color.White);
                if (creature.IsDynamic)
                    Dynamic.Add(creature);
                else
                    Static.Add(creature);
            }
        }

        public static Bitmap SetAlphaBending(Bitmap image, int alpha)
        {
            for (var x = 0; x < image.Width; x++)
                for (var y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, Color.FromArgb(alpha, pixel));
                }
            return image;
        }
    }
}
