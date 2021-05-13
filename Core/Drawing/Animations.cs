using System.Timers;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
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
            sprite.SizePercent = (sprite.SizePercent.Width, 100);

            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, args) =>
            {
                if (-sprite.SizePercent.Height > 0)
                {
                    timer.Dispose();
                    return;
                }
                sprite.SizePercent.Height -= 2;
            };
            timer.Start();
        }

        public static void Close(Door door, int interval) // ToUpgrade..
        {
            var sprite = door.Sprite;
            sprite.SizePercent = (sprite.SizePercent.Width, 1);

            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, args) =>
            {
                if (sprite.SizePercent.Height >= 100)
                {
                    timer.Dispose();
                    return;
                }
                sprite.SizePercent.Height += 2;
            };
            timer.Start();
        }

        public static void Warp(int interval) // ToDo..
        {
            
        }
    }
}
