using System;
using System.Timers;

namespace DWarp.Core.Controls
{
    public class AnimationControl
    {
        private Timer timer = new Timer();
        private Action stopAction;

        public void Dispose() 
        { 
            timer.Dispose();
            stopAction();
        }

        public void Initialize(int startPos, int endPos, Action<int> stepAction, Action stopAction)
        {
            if (timer != null)
                timer.Dispose();
            timer = new Timer();
            timer.Interval = 1;
            this.stopAction = stopAction;

            var currentPos = startPos;
            timer.Elapsed += (sender, args) =>
            {
                if (currentPos < endPos)
                {
                    currentPos++;
                    stepAction(currentPos);
                }
                else
                {
                    timer.Stop();
                    Dispose();
                }
            };
            timer.Start();
        }
    }
}
