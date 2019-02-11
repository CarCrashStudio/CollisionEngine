using LinkEngine.Components;
using System.Drawing;

namespace LinkEngine.Components
{
    public class Camera
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point[] Boundries { get; set; }

        public Bitmap View { get; set; }

        // public HUD UI { get; set; }

        //public Rendering.Screen Screen { get; set; }

        public Camera ()
        {
            Boundries = new Point[2];
        }
    }
}
