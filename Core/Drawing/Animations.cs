using System;
using System.Timers;
using DWarp.Core.Controls;
using DWarp.Core.Models;

namespace DWarp.Core.Drawing
{
    static class Animations
    {
        private static AnimationControl fallingAnim = new AnimationControl();
        private static AnimationControl doorAnim = new AnimationControl();
        private static AnimationControl warpAnim = new AnimationControl();

        public static void Fall(State state, Creature creature)
        {
            var stepAction = new Action<int>(step => creature.Sprite.Offset.Y++);
            var stopAction = new Action(() => creature.Sprite.Offset = (0, 0));

            var startYOffset = -state.SpritesSize / 2;
            creature.Sprite.Offset = (0, startYOffset);
            fallingAnim.Initialize(-state.SpritesSize / 2, 0, stepAction, stopAction);
        }

        public static void Open(Door door)
        {
            var stepAction = new Action<int>(step => door.Sprite.SizePercent.Height -= 2);
            var stopAction = new Action(() => door.Sprite.SizePercent.Height = 0);

            door.Sprite.SizePercent.Height = 100;
            doorAnim.Initialize(0, 50, stepAction, stopAction);
        }

        public static void Close(Door door)
        {
            var stepAction = new Action<int>(step => door.Sprite.SizePercent.Height += 2);
            var stopAction = new Action(() => door.Sprite.SizePercent.Height = 100);

            door.Sprite.SizePercent.Height = 0;
            doorAnim.Initialize(0, 50, stepAction, stopAction);
        }

        public static void WarpIn(Sprite sprite, MainForm mainForm) // ToDo..
        {
            var stepCount = 20;
            var stepAction = new Action<int>(step => 
            {
                sprite.SizePercent = (100 + stepCount - step, 100 + stepCount - step);
                sprite.Rectangle.X = (mainForm.ClientSize.Width + 5 - sprite.Rectangle.Width) / 2;
                sprite.Rectangle.Y = (mainForm.ClientSize.Height + 5 - sprite.Rectangle.Height) / 2;
            });
            var stopAction = new Action(() =>
            {
                sprite.SizePercent = (100, 100);
                sprite.Rectangle.X = 0;
                sprite.Rectangle.Y = 0;
            });

            sprite.Visible = true;
            sprite.SizePercent = (100 + stepCount, 100 + stepCount);
            warpAnim.Initialize(0, stepCount, stepAction, stopAction);
        }

        public static void WarpOut(Sprite sprite, MainForm mainForm)
        {
            var stepCount = 20;
            var stepAction = new Action<int>(step =>
            {
                sprite.SizePercent = (100 + step, 100 + step);
                sprite.Rectangle.X = (mainForm.ClientSize.Width + 5 - sprite.Rectangle.Width) / 2;
                sprite.Rectangle.Y = (mainForm.ClientSize.Height + 5 - sprite.Rectangle.Height) / 2;
            });
            var stopAction = new Action(() =>
            {
                sprite.SizePercent = (100 + stepCount, 100 + stepCount);
                sprite.Rectangle.X = (mainForm.ClientSize.Width + 5 - sprite.Rectangle.Width) / 2;
                sprite.Rectangle.Y = (mainForm.ClientSize.Height + 5 - sprite.Rectangle.Height) / 2;
                sprite.Visible = false;
            });

            sprite.SizePercent = (100, 100);
            warpAnim.Initialize(0, stepCount, stepAction, stopAction);
        }
    }
}
