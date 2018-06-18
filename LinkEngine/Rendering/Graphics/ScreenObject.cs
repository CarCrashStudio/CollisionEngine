using System.Drawing;

namespace LinkEngine.Rendering
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

    }
}
