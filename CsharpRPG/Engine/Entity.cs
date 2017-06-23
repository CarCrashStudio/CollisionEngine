using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;

namespace RPG_Engine
{
    public class Entity
    {
        Point location;
        Bitmap img;
        int id;
        string name;
        int hp;
        int maxHp;
        int mana;
        int maxMana;
        int maxDamage;
        int maxDefense;
        PictureBox HudForm;

        Random rand = new Random();

        public Entity(int _id, string _name, Point _location,  Bitmap _img, World _world, PictureBox _HudForm)
        {
            id = _id;
            name = _name;
            location = _location;
            img = _img;

            world = _world;

            HudForm = _HudForm;
        }
        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public Point Location { get { return location; } set { location = value; } }
        public Bitmap Image { get { return img; } set { img = value; } }

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
