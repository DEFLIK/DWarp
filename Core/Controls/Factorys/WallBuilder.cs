using System.Drawing;
using System.Text;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    public static class WallBuilder
    {
        public static void SetWallsSprite(State state)
        {
            var offsets = new Point[]
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0)
            };

            foreach (var creature in state.Map)
                if (creature.Type == CreatureType.Wall)
                {
                    //Соседи записываются логическими 1 и 0 для быстрого преобразования в спрайты
                    var stateBuilder = new StringBuilder();
                    stateBuilder.Append("Wall"); // Цифры после "Wall" отображают значения нахождения стен по соседству в порядке Up Down Left Right
                    foreach (var offset in offsets)
                    {
                        if (
                            creature.Location.X + offset.X < state.Map.GetLength(0)
                            && creature.Location.X + offset.X >= 0
                            && creature.Location.Y + offset.Y < state.Map.GetLength(1)
                            && creature.Location.Y + offset.Y >= 0)
                        {
                            if (state.Map[creature.Location.X + offset.X, creature.Location.Y + offset.Y].Type is CreatureType.Wall)
                                stateBuilder.Append("0");
                            else
                                stateBuilder.Append("1");
                        }
                        else
                            stateBuilder.Append("1");
                    }
                    creature.Sprite.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(stateBuilder.ToString(), Properties.Resources.Culture);
                }
        }
    }
}
