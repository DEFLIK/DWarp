using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    public class CreaturesSprites //ToFix "MakeTransparent" threading exception
    {
        public List<Creature> Dynamic = new List<Creature>();
        public List<Creature> Static = new List<Creature>();

        public CreaturesSprites(State state)
        {
            Dynamic.Add(state.Player);
            state.Player.Sprite.Image.MakeTransparent(Color.White);
            Dynamic.Add(state.WarpedPlayer);
            SetAlphaBending(state.WarpedPlayer.Sprite.Image, 140);
            state.WarpedPlayer.Sprite.Image.MakeTransparent(Color.White);
            if (state.Dummy != null)
            {
                Dynamic.Add(state.Dummy);
                state.Dummy.Sprite.Image.MakeTransparent(Color.White);
            }
            foreach (var cube in state.Cubes)
                if (cube != null)
                {
                    Dynamic.Add(cube);
                    cube.Sprite.Image.MakeTransparent(Color.White);
                }
            foreach (var creature in state.Map)
            {
                creature.Sprite.Image.MakeTransparent(Color.White);
                if (creature.IsDynamic)
                    Dynamic.Add(creature);
                else
                    Static.Add(creature);
            }
            Dynamic = Dynamic.OrderBy(creature => creature.Sprite.DrawingLayer).ToList();
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
