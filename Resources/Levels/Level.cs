using DWarp.Core.Models;

namespace DWarp.Resources.Levels
{
    public class Level
    {
        public readonly string MapPreset;
        public readonly Wire[] Wires;
        public readonly int TimeLimit;
        public readonly int StepsLimit;

        public Level(string mapPreset, int stepsLimit, int timeLimit = 0, Wire[] wires = null)
        {
            MapPreset = mapPreset;
            StepsLimit = stepsLimit;
            Wires = wires;
            TimeLimit = timeLimit;
        }
    }
}
