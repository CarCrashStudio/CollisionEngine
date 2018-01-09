using System.Drawing;

namespace RPG.Rendering
{
    public static class ScreenObject
    {
        public static Bitmap Draw(int imgWidth, int imgHeight, Point drawLoc, Bitmap Image, Bitmap img = null)
        {
            var bitmap = new Bitmap(1,1);
            if (img == null) { bitmap = new Bitmap(imgWidth, imgHeight); }
            else { bitmap = new Bitmap(img, imgWidth, imgHeight); }
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImageUnscaled(Image, drawLoc);

            return bitmap;
        }
        public static Bitmap DrawText(string str, int fontSize,int imgWidth, int imgHeight, Point drawLoc, Bitmap img = null)
        {
            Font font = new Font("Arial", fontSize);
            Brush brush = new SolidBrush(Color.Black);
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
