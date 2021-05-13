using System;
using System.Threading;

namespace DWarp.Core.Controls
{
    public static class FpsCounter
    {
        private static int frameCount;
        private static DateTime lastCheckTime = DateTime.Now;

        public static void Elapsed() =>
            Interlocked.Increment(ref frameCount);

        public static int GetFps()
        {
            var result = (int)(frameCount / (DateTime.Now - lastCheckTime).TotalSeconds);
            Interlocked.Exchange(ref frameCount, 0);
            lastCheckTime = DateTime.Now;
            return result;
        }
    }
}
