using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpRPG.Engine
{
    public class ScreenObject
    {
        Bitmap img;
        int id;
        string name;

        public Bitmap Image { get { return img; } set { img = value; } }
        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }

        public ScreenObject(int _id, string _name, Bitmap _img)
        {
            id = _id;
            name = _name;
            img = _img;
        }

        public Bitmap Draw(int imgWidth, int imgHeight, Point drawLoc, Bitmap img = null)
        {
            var bitmap = new Bitmap(1,1);
            if (img == null) { bitmap = new Bitmap(imgWidth, imgHeight); }
            else { bitmap = new Bitmap(img, imgWidth, imgHeight); }
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(Image, drawLoc);

            return bitmap;
        }
    }
}
