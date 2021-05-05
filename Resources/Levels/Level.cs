using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWarp
{
    public class Level
    {
        public readonly string Map;
        public readonly Wire[] Wires;
        public readonly int TimeLimit;
        public readonly int StepsLimit;

        public Level(string map, int stepsLimit, int timeLimit = 0, Wire[] wires = null)
        {
            Map = map;
            StepsLimit = stepsLimit;
            Wires = wires;
            TimeLimit = timeLimit;
        }
    }
}
