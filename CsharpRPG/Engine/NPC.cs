using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class NPC : ScreenObject
    {
        public Point Location { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public PictureBox HudForm{ get; set; }

        public World world { get; set; }

        public NPC(int _id, string _name, Bitmap _img, Point _location, Quest _questAvailibleHere, PictureBox _HudForm, World _world) : 
            base(_id, _name, _img)
        {
            Location = _location;
            QuestAvailableHere = _questAvailibleHere;
            HudForm = _HudForm;
            world = _world;

            Draw(world.HudForm.Width, world.HudForm.Height, new Point(Location.X * 32, Location.Y * 32), (Bitmap)world.HudForm.Image);
        }
        public NPC(NPC npc) : 
            base(npc.ID, npc.Name, npc.Image)
        {
            Location = npc.Location;
            QuestAvailableHere = npc.QuestAvailableHere;
            HudForm = npc.HudForm;

            Draw(world.HudForm.Width, world.HudForm.Height, new Point(Location.X * 32, Location.Y * 32), (Bitmap)world.HudForm.Image);
        }
    }
}
