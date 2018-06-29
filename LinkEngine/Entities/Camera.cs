using System.Drawing;

namespace LinkEngine.Entities
{
    public class Camera
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point[] Boundries { get; set; }

        public Image View { get; set; }

        public Camera ()
        {
            Boundries = new Point[2];
        }
    }
}
