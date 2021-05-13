using System.Drawing;
using System.Text;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Drawing
{
    public static class WallBuilder
    {
        public static void SetWallsSprite()
        {
            var offsets = new Point[]
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0)
            };

            foreach (var creature in Game.Map)
                if (creature.Type == CreatureType.Wall)
                {
                    //Соседи записываются логическими 1 и 0 для быстрого преобразования в спрайты
                    var builder = new StringBuilder();
                    builder.Append("Wall"); // Цифры после "Wall" отображают значения нахождения стен по соседству в порядке Up Down Left Right
                    foreach (var offset in offsets)
                    {
                        if (
                            creature.Location.X + offset.X < Game.Map.GetLength(0) // ToOtherMethod...
                            && creature.Location.X + offset.X >= 0
                            && creature.Location.Y + offset.Y < Game.Map.GetLength(1)
                            && creature.Location.Y + offset.Y >= 0)
                        {
                            if (Game.Map[creature.Location.X + offset.X, creature.Location.Y + offset.Y].Type is CreatureType.Wall)
                                builder.Append("0");
                            else
                                builder.Append("1");
                        }
                        else
                            builder.Append("1");
                    }
                    var result = builder.ToString();
                    creature.Sprite.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(builder.ToString(), Properties.Resources.Culture);
                }
        }
    }
}
