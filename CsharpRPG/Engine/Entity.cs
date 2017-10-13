using System.Drawing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsharpRPG.Engine
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

        Random rand = new Random();

        public Bitmap Image { get { return img; } set { img = value; } }
        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Facing { get; set; } // The direction the player is facing (North, South, East, West)
        public Point NextTile { get; set; } //THe coordinate of the tile in front of the player
        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public Point Location { get { return location; } set { location = value; } }
        public List<Skill> Skills { get; set; }
        public List<Entity> Party { get; set; }
        public List<Entity> PartyDead { get; set; }

        public World world { get; set; }

        public int Strength { get { return maxDamage; } set { maxDamage = value; } }
        public int Defense { get { return maxDefense; } set { maxDefense = value; } }

        public Entity(int _id, string _name, Point _location, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense, Bitmap _img, World _world = null)
        {
            id = _id;
            name = _name;
            img = _img;
            hp = _hp;
            maxHp = _maxHp;
            mana = _mana;
            maxMana = _maxMana;
            maxDamage = _maximumDamage;
            maxDefense = _maxDefense;
            location = _location;
            world = _world;
            Skills = new List<Skill>();
            Party = new List<Entity>();
            PartyDead = new List<Entity>();

            Party.Add(this);
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
        public void SwitchFacing(string Entity)
        {
            switch (Facing)
            {
                case "North":
                    Image = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(Entity + "Up", Properties.Resources.Culture));
                    break;
                case "South":
                    Image = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(Entity + "Down", Properties.Resources.Culture));
                    break;
                case "East":
                    Image = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(Entity + "Right", Properties.Resources.Culture));
                    break;
                case "West":
                    Image = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(Entity + "Left", Properties.Resources.Culture));
                    break;
            }
        }
        public void Move(int x, int y)
        {
            Location = new Point(Location.X + x, Location.Y + y);
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

            foreach (Tile tile in world.map.DecosOnMap)
            {
                if (tile.Location == CheckNextTile())
                {
                    if (tile.Dense == 1)
                    {
                        return true;
                    }
                }
            }

            foreach (NPC npc in world.HUD.NpcsHere)
            {
                if(npc.Location == CheckNextTile())
                {
                    return true;
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
        public PropertyInfo FindVariable(string var)
        {
            Type mytype = typeof(Entity);
            PropertyInfo propInfo = mytype.GetProperty(var);
            return propInfo;
        }
        public void KillPartyMember(Entity partymember)
        {
            Party.Remove(partymember);
            PartyDead.Add(partymember);
        }
    }
}
