using DWarp.Core.Models;

namespace DWarp.Resources.Levels
{
    public class Level
    {
        public readonly string MapPreset;
        public readonly Wire[] Wires;
        public readonly int TimeLimit;
        public readonly int StepsLimit;
        public readonly string NextLevelName; //to change

        public Level(string mapPreset, int stepsLimit, string nextLevelName = null, int timeLimit = 0, Wire[] wires = null)
        {
            MapPreset = mapPreset;
            StepsLimit = stepsLimit;
            NextLevelName = nextLevelName;
            TimeLimit = timeLimit;
            Wires = wires;
        }
    }
}
