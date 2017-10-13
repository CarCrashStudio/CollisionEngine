using System.Drawing;
using _2D_Graphics_Engine.Engine;

namespace CsharpRPG.Engine
{
    public class NPC
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Shop ShopAvailibleHere { get; set; }
        public Bitmap Image { get; set; }

        public World world { get; set; }

        public NPC(int _id, string _name, Bitmap _img, Point _location, Quest _questAvailibleHere, Shop _shopAvailibleHere, World _world)
        {
            ID = _id;
            Name = _name;
            Image = _img;
            Location = _location;
            QuestAvailableHere = _questAvailibleHere;
            ShopAvailibleHere = _shopAvailibleHere;
            world = _world;

            ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(Location.X * 32, Location.Y * 32), Image, (Bitmap)world.HudForm.Image);
        }
        public NPC(NPC npc)
        {
            ID = npc.ID;
            Name = npc.Name;
            Image = npc.Image;
            Location = npc.Location;
            QuestAvailableHere = npc.QuestAvailableHere;
            ShopAvailibleHere = npc.ShopAvailibleHere;
            world = npc.world;

            ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(Location.X * 32, Location.Y * 32), Image, (Bitmap)world.HudForm.Image);
        }
    }
}
