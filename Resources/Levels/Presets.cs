using System.Collections.Generic;
using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Resources.Levels
{
    public static class Presets
    {
        public static readonly Dictionary<string, Level> Levels = new Dictionary<string,Level>();

        static Presets()
        {
            Levels.Add("defaultLevel", new Level
            (
@"
K..+..+.
..B+..+.
...+++++
...D..DE
...+++++
..B+..+.
.PC+..+.
...+..+.
", 15, 30,
                new Wire[]
                {
                    new Wire(new Point(2, 1), new Point(3, 3)),
                    new Wire(new Point(2, 5), new Point(6, 3))
                }
            ));

            Levels.Add("hardLevel", new Level
            (
@"
....+....
....D.E..
+D+++++++
..+C.B+C.
..+.P.+..
+D++D++D+
.........
.B..B..B.
....B....
", 28, 0,
                new Wire[]
                {
                    new Wire(new Point(4, 7), new Point(1, 2)),
                    new Wire(new Point(5, 3), new Point(4, 5)),
                    new Wire(new Point(1, 7), new Point(1, 5)),
                    new Wire(new Point(7, 7), new Point(7, 5)),
                    new Wire(new Point(4, 8), new Point(4, 1))
                }
            ));
        }
    }
}
