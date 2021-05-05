using System.Windows.Forms;
using DWarp.Core.Drawing;

namespace DWarp.Core.Controls
{
    static class InputControl
    {
        public static void ApllyMouseScroll(int deltaScroll) => Game.SpritesSize += deltaScroll > 0 ? 1 : -1;

        public static void ApplyKey(Keys key) //ToRefactor...
        {
            switch (key)
            {
                case Keys.Space:
                    if (Game.Player.PickedCube == null)
                        PlayerCommands.TakeCube();
                    else
                        PlayerCommands.PlaceCube();
                    break;
                case Keys.B:
                    Animations.Fall(Game.Player, 10);
                    break;
                case Keys.F:
                    Game.DoWarp();
                    break;
                case Keys.W:
                    if (!Game.IsWarped)
                        PlayerCommands.Move(0, -1);
                    break;
                case Keys.A:
                    if (!Game.IsWarped)
                        PlayerCommands.Move(-1, 0);
                    break;
                case Keys.S:
                    if (!Game.IsWarped)
                        PlayerCommands.Move(0, 1);
                    break;
                case Keys.D:
                    if (!Game.IsWarped)
                        PlayerCommands.Move(1, 0);
                    break;
                case Keys.Q:
                    if (Game.IsWarped)
                        Game.CommandsStack.RollBack();
                    break;
                case Keys.E:
                    if (Game.IsWarped)
                        Game.CommandsStack.RollForward();
                    break;
                case Keys.V:
                    if (Game.IsWarped)
                        Game.WarpPlayer();
                    break;
                //case Keys.Up:
                //    Game.SpritesSize += 5;
                //    break;
                //case Keys.Down:
                //    Game.SpritesSize -= 5;
                //    break;
                case Keys.R:
                    Game.Load(Game.CurrentLevel);
                    break;
            }
        }
    }
}
