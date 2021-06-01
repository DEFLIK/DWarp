using System.Windows.Forms;
using DWarp.Core.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    static class InputControl
    {
        public static void ApllyMouseScroll(State state, int deltaScroll) => state.SpritesSize += deltaScroll > 0 ? 1 : -1; 

        public static void ApplyKey(State state, Keys key) //ToRefactor...
        {
            switch (key)
            {
                case Keys.Space:
                    if (state.Player.PickedCube == null)
                        PlayerCommands.TakeCube(state);
                    else
                        PlayerCommands.PlaceCube(state);
                    break;
                case Keys.B:
                    Animations.Fall(state, state.Player, 10);
                    break;
                case Keys.F:
                    state.DoWarp();
                    break;
                case Keys.W:
                    if (!state.IsWarped)
                        PlayerCommands.Move(state, 0, -1);
                    break;
                case Keys.A:
                    if (!state.IsWarped)
                        PlayerCommands.Move(state, -1, 0);
                    break;
                case Keys.S:
                    if (!state.IsWarped)
                        PlayerCommands.Move(state, 0, 1);
                    break;
                case Keys.D:
                    if (!state.IsWarped)
                        PlayerCommands.Move(state, 1, 0);
                    break;
                case Keys.Q:
                    if (state.IsWarped)
                        state.CommandsStack.RollBack();
                    break;
                case Keys.E:
                    if (state.IsWarped)
                        state.CommandsStack.RollForward();
                    break;
                case Keys.V:
                    if (state.IsWarped)
                        state.WarpPlayer();
                    break;
                case Keys.R:
                    Game.ChangeLevel(state.CurrentLevel);
                    break;
                case Keys.Escape:
                    Game.mainForm.MenuPanel.SwitchVisibility();
                    break;
            }
        }
    }
}
