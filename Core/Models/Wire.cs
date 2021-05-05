using System.Drawing;

namespace DWarp
{
    public class Wire
    {
        public readonly Point Button;
        public readonly Point Door;
        public Wire(Point button, Point door)
        {
            Button = button;
            Door = door;
        }
    }
}
