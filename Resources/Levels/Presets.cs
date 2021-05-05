using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Resources.Levels
{
    public static class Presets
    {
        public static readonly Level TestLevel = new Level
            (
@"
...+..+.
..B+..+.
...+++++
...D..D.
...+++++
..B+..+.
.PC+..+.
...+..+.
", 15, 20,
                new Wire[]
                {
                    new Wire(new Point(2, 1), new Point(3, 3)),
                    new Wire(new Point(2, 5), new Point(6, 3))
                }
            );

        public static readonly Level HardLevel = new Level
            (
@"
....+....
....D....
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
            );
    }
}
