using System.Drawing;

namespace DWarp.Core.Drawing
{
    public static class UISprites
    {
        public static Sprite WarpVignette = new Sprite(new Rectangle(0, 0, Game.MainForm.ClientSize.Width, Game.MainForm.ClientSize.Height), Properties.Resources.Warp, null)
            { Visible = false };
        public static Sprite Vignette = new Sprite(new Rectangle(0, 0, Game.MainForm.ClientSize.Width, Game.MainForm.ClientSize.Height), Properties.Resources.Vignette, null);
    }
}
