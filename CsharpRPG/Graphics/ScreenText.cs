using System.Drawing;
using System.Windows.Forms;

namespace RPG.Rendering
{
    public static class ScreenText
    {
        static Bitmap BackgroundImage;
        static PictureBox Canvas;
        static string Text;
        static Form form;

        public static void Draw()
        {
            Bitmap Image = ScreenObject.DrawText(Text, 16, Canvas.Width, Canvas.Height, new Point(Canvas.Width, Canvas.Height), null);
            Canvas.Image = ScreenObject.Draw(Canvas.Width, Canvas.Height, new Point((Canvas.Width / 2) - (Image.Width / 2), Canvas.Height / 2), null);
            form.ShowDialog();
        }

        static void CreateNewCanvas(Form parent)
        {
            int width = 400;
            int height = 200;

            form = new Form
            {
                Size = new Size(width, height),
                FormBorderStyle = FormBorderStyle.None
            };

            Canvas = new PictureBox
            {
                Size = new Size(width, height),
                BackgroundImage = BackgroundImage,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            form.Controls.Add(Canvas);
        }
    }
}
