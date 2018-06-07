using LinkEngine.Rendering;
using System.Collections.Generic;
using System.Drawing;

namespace LinkEngine.GUI
{
    public class Hud
    {
        /// <summary>
        /// Holds all HudObjects on screen
        /// </summary>
        public List<HudObject> Objects = new List<HudObject>();
        
        /// <summary>
        /// Redraw all HudObjects on the Screen
        /// </summary>
        /// <param name="imgWidth">The width of the object being drawn on</param>
        /// <param name="imgHeight">The height if the object being drawn on</param>
        /// <param name="currentImg">The image currently on display in the object being drawn on</param>
        public void UpdateHUD(ref Bitmap currentImg)
        {
            foreach(HudObject ho in Objects)
            {
                DrawBars(ho, ref currentImg);
            }
        }

        /// <summary>
        /// Draw a hud object, or "Hud Bars"
        /// </summary>
        /// <param name="bar">The HudObject to draw</param>
        /// <param name="currentImage">The image being drawn on</param>
        public void DrawBars(HudObject bar, ref Bitmap currentImage)
        {
            if (bar.Text != null)
            {
                if (bar.Image != null)
                {
                    currentImage = ScreenObject.DrawText(bar.Text, 16, currentImage.Width, currentImage.Height, new Point(bar.X_1 + bar.Image.Width + 5, bar.Y_1), currentImage);
                }
                else
                {
                    currentImage = ScreenObject.DrawText(bar.Text, 16, currentImage.Width, currentImage.Height, new Point(bar.X_1, bar.Y_1), currentImage);

                }
            }
            if (bar.Image != null)
            {
                currentImage = ScreenObject.Draw(currentImage.Width, currentImage.Height, new Point(bar.X_1, bar.Y_1), bar.Image, currentImage);
            }
        }
    }
    public class HudObject
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
        public HudObject(int x1, int x2, int y1, int y2, Bitmap _Image = null, string _Text = null)
        {
            X_1 = x1;
            X_2 = x2;
            Y_1 = y1;
            Y_2 = y2;
            Image = _Image;
            Text = _Text;
        }
    }
}
