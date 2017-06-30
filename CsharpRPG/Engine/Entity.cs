using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;

namespace CsharpRPG.Engine
{
    public class Entity : ScreenObject
    {
        Point location;
        
        int hp;
        int maxHp;
        int mana;
        int maxMana;
        int maxDamage;
        int maxDefense;
        PictureBox HudForm;

        Random rand = new Random();

        public Entity(int _id, string _name, Point _location, Bitmap _img, PictureBox _HudForm, World _world = null) :
            base(_id,_name, _img)
        {
            location = _location;
            world = _world;
            HudForm = _HudForm;
        }

        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public Point Location { get { return location; } set { location = value; } }
        
        public World world { get;  set; }

        public int MaximumDamage { get { return maxDamage; } set { maxDamage = value; } }
        public int MaximumDefense { get { return maxDefense; } set { maxDefense = value; } }

        public string Facing { get; set; } // The direction the player is facing (North, South, East, West)
        public Point NextTile { get; set; } //THe coordinate of the tile in front of the player
        
        public Point CheckNextTile()
        {
            Point tempNextTile;
            switch (Facing)
            {
                case "North":
                    tempNextTile = new Point(Location.X + 0, Location.Y - 1);
                    return tempNextTile;
                case "South":
                    tempNextTile = new Point(Location.X + 0, Location.Y + 1);
                    return tempNextTile;
                case "East":
                    tempNextTile = new Point(Location.X + 1, Location.Y + 0);
                    return tempNextTile;
                case "West":
                    tempNextTile = new Point(Location.X - 1, Location.Y + 0);
                    return tempNextTile;
            }
            return new Point(0, 0);
        }
        public bool isDead()
        {
            if (hp <= 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
