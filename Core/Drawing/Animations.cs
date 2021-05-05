using System.Timers;

namespace DWarp
{
    static class Animations
    {
        public static void Fall(Creature creature, int interval)
        {
            var sprite = creature.Sprite;
            var timer = new Timer();
            timer.Interval = interval;

            var startYOffset = -Game.SpritesSize / 2;
            sprite.Offset = (0, startYOffset);

            timer.Elapsed += (sender, args) =>
            {
                if (sprite.Offset.Y >= 0)
                {
                    timer.Dispose();
                    return;
                }
                sprite.Offset.Y++;
            };
            timer.Start();
        }

        public static void Open(Door door, int interval) // ToUpgrade...
        {
            var sprite = door.Sprite;
            var timer = new Timer();
            timer.Interval = interval;

            timer.Elapsed += (sender, args) =>
            {
                if (-sprite.SizeOffset.Height >= Game.SpritesSize)
                {
                    timer.Dispose();
                    return;
                }
                sprite.SizeOffset.Height--;
            };
            timer.Start();
        }

        public static void Close(Door door, int interval) // ToUpgrade..
        {
            var sprite = door.Sprite;
            var timer = new Timer();
            timer.Interval = interval;

            timer.Elapsed += (sender, args) =>
            {
                if (sprite.SizeOffset.Height >= 0)
                {
                    timer.Dispose();
                    return;
                }
                sprite.SizeOffset.Height++;
            };
            timer.Start();
        }
    }
}
