using LinkEngine;
using System.Drawing;

namespace LinkEngine
{
    public class Camera
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point[] Boundries { get; set; }

        public Bitmap View { get; set; }

        // public HUD UI { get; set; }

        public Screen Screen { get; set; }

        public Camera ()
        {
            Boundries = new Point[2];
        }
    }
}
