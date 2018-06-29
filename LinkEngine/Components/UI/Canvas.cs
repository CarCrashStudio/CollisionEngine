using LinkEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class Canvas
    {
        /// <summary>
        /// Holds all HudObjects on screen
        /// </summary>
        public List<HUDObject> Objects { get; set; }

        /// <summary>
        /// Redraw all HudObjects on the Screen
        /// </summary>
        /// <param name="imgWidth">The width of the object being drawn on</param>
        /// <param name="imgHeight">The height if the object being drawn on</param>
        /// <param name="currentImg">The image currently on display in the object being drawn on</param>
        public void UpdateHUD(ref Bitmap currentImg)
        {
            foreach (HUDObject ho in Objects)
            {
                DrawBars(ho, ref currentImg);
            }
        }

        /// <summary>
        /// Draw a hud object, or "Hud Bars"
        /// </summary>
        /// <param name="bar">The HudObject to draw</param>
        /// <param name="currentImage">The image being drawn on</param>
        public void DrawBars(HUDObject bar, ref Bitmap currentImage)
        {
            if (bar.Text != null)
            {
                if (bar.Image != null)
                {
                    currentImage = ScreenText.Draw(bar.Text, currentImage.Width, currentImage.Height, new Point(bar.X_1 + bar.Image.Width + 5, bar.Y_1), currentImage);
                }
                else
                {
                    currentImage = ScreenText.Draw(bar.Text, currentImage.Width, currentImage.Height, new Point(bar.X_1, bar.Y_1), currentImage);

                }
            }
            if (bar.Image != null)
            {
                currentImage = ScreenObject.Draw(currentImage.Width, currentImage.Height, new Point(bar.X_1, bar.Y_1), bar.Image, currentImage);
            }
        }

        public Canvas ()
        {
            Objects = new List<HUDObject>();
        }
    }
}
