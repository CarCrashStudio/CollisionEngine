using System.Drawing;

namespace LinkEngine.Rendering
{
    public static class ScreenText
    {
        /// <summary>
        /// Takes a canvas size and draw location and draws a string onto the canvas
        /// </summary>
        /// <param name="str">The string to draw on screen</param>
        /// <param name="imgWidth">The width of the canvas</param>
        /// <param name="imgHeight">the Height of the canvas</param>
        /// <param name="drawLoc">The point at which the text should start</param>
        /// <param name="img">An already established image to draw over. Can be null</param>
        /// <returns></returns>
        public static Bitmap Draw (string str, int imgWidth, int imgHeight, Point drawLoc, Bitmap img = null)
        {
            // Create a new font to write in
            Font font = new Font("Arial", 16);

            // create a new brush for the coloring of the text
            Brush brush = new SolidBrush(Color.Black);

            // create a new blank bitmap set to the determined width and height
            var bitmap = new Bitmap(imgWidth, imgHeight);

            // if img was provided to mark on, create a new bitmap using img
            if (img != null)
            {
                bitmap = new Bitmap(img, imgWidth, imgHeight);
            }

            // create a new graphics object to draw with
            var graphics = Graphics.FromImage(bitmap);

            // draw the text to bitmap using graphics
            graphics.DrawString(str, font, brush, drawLoc);

            // return the new bitmap
            return bitmap;
        }

        /// <summary>
        /// Takes a canvas size and draw location and draws a string onto the canvas. 
        /// The font is all chosen by you.
        /// </summary>
        /// <param name="str">The string to draw on screen</param>
        /// <param name="imgWidth">The width of the canvas</param>
        /// <param name="imgHeight">the Height of the canvas</param>
        /// <param name="drawLoc">The point at which the text should start</param>
        /// <param name="fontName">The font you wish to use as a string</param>
        /// <param name="fontSize">the size of the font you wish to use</param>
        /// <param name="fontColor">The color of the font you wish to use</param>
        /// <param name="img">An already established image to draw over. Can be null</param>
        /// <returns></returns>
        public static Bitmap Draw(string str, int imgWidth, int imgHeight, Point drawLoc, string fontName, short fontSize, Color fontColor, Bitmap img = null)
        {
            Font font = new Font(fontName, fontSize);
            Brush brush = new SolidBrush(fontColor);

            var bitmap = new Bitmap(imgWidth, imgHeight);
            if (img != null)
            {
                bitmap = new Bitmap(img, imgWidth, imgHeight);
            }

            var graphics = Graphics.FromImage(bitmap);

            graphics.DrawString(str, font, brush, drawLoc);
            return bitmap;
        }
    }
}
