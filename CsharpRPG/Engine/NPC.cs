using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RPG_Engine
{
    public class NPC
    {
        int id;
        string name;
        Bitmap img;
        Point location;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public Point Location { get { return location; } set { location = value; } }
        public Bitmap Image { get { return img; } set { img = value; } }
        public Quest QuestAvailableHere { get; set; }
        public PictureBox charForm { get; set; }//Does the NPC Have a quest

        public NPC(int _id, string _name, Bitmap _img, Point _location, Quest _questAvailibleHere, PictureBox _charForm)
        {
            id = _id;
            name = _name;
            img = _img;
            location = _location;
            QuestAvailableHere = _questAvailibleHere;
            charForm = _charForm;

            Draw();
        }
        public NPC(NPC npc)
        {
            id = npc.ID;
            name = npc.Name;
            img = npc.Image;
            location = npc.Location;
            QuestAvailableHere = npc.QuestAvailableHere;
            charForm = npc.charForm;

            Draw();
        }
        public Bitmap Draw()
        {
            var bitmap = new Bitmap(charForm.Image, charForm.Width, charForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(img, new Point((location.X * 32), (location.Y * 32)));

            return bitmap;
        }
    }
}
