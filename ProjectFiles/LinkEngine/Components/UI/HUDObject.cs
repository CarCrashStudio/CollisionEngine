using System.Collections.Generic;
using System.Drawing;

namespace LinkEngine
{
    public class HUDObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Shown = false;
        public int X_1 { get; set; }
        public int X_2 { get; set; }
        public int Y_1 { get; set; }
        public int Y_2 { get; set; }
        public string Text { get; set; }
        public Bitmap Image { get; set; }

        /// <summary>
        /// Creates a new object of the HudObject Class
        /// </summary>
        /// <param name="x1">The x value of the top left corner of the object</param>
        /// <param name="x2">The x value of the bottom right corner of the object</param>
        /// <param name="y1">The y value of the top left corner of the object</param>
        /// <param name="y2">The y value of the bottom right corner of the object</param>
        /// <param name="_Image">The image to be displayed on the object</param>
        /// <param name="_Text">The text to be displayed on the object</param>
        public HUDObject(int x1, int x2, int y1, int y2)
        {
            X_1 = x1;
            X_2 = x2;
            Y_1 = y1;
            Y_2 = y2;
        }
    }
}
