using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;

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

        Random rand = new Random();

        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public Point Location { get { return location; } set { location = value; } }
        public List<Skill> Skills { get; set; }

        public World world { get; set; }

        public int MaximumDamage { get { return maxDamage; } set { maxDamage = value; } }
        public int MaximumDefense { get { return maxDefense; } set { maxDefense = value; } }

        public string Facing { get; set; } // The direction the player is facing (North, South, East, West)
        public Point NextTile { get; set; } //THe coordinate of the tile in front of the player

        public Entity(int _id, string _name, Point _location, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense, Bitmap _img, World _world = null) :
            base(_id,_name, _img)
        {
            hp = _hp;
            maxHp = _maxHp;
            mana = _mana;
            maxMana = _maxMana;
            maxDamage = _maximumDamage;
            maxDefense = _maxDefense;
            location = _location;
            world = _world;
            Skills = new List<Skill>();
        }
        
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
        public void Move(int x, int y, string Entity)
        {
            Location = new Point(Location.X + x, Location.Y + y);
            switch (Facing)
            {
                case "North":
                    Image = new Bitmap("icons/" + Entity + "States/" + Entity + "Up.bmp");
                    break;
                case "South":
                    Image = new Bitmap("icons/" + Entity + "States/" + Entity + "Down.bmp");
                    break;
                case "East":
                    Image = new Bitmap("icons/" + Entity + "States/" + Entity + "Right.bmp");
                    break;
                case "West":
                    Image = new Bitmap("icons/" + Entity + "States/" + Entity + "Left.bmp");
                    break;
            }           
        }
        public bool isColliding()
        {
            //If the X Coordinate exceeds 0 or max map size
            if (CheckNextTile().X == -1 || CheckNextTile().X == (world.MAX_MAP_SIZE + 1))
            {
                return true;
            }

            //If the Y Coordinate exceeds 0 or max map size
            if (CheckNextTile().Y == -1 || CheckNextTile().Y == (world.MAX_MAP_SIZE + 1))
            {
                return true;
            }

            foreach (Tile tile in world.map.TilesOnMap)
            {
                if (tile.Location == CheckNextTile())
                {
                    if (tile.Dense == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
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
