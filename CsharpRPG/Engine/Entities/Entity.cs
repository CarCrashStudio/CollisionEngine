using System.Drawing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RPG.Engine
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

        public Entity(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense, World _world = null)
        {
            id = _id;
            name = _name;
            hp = _hp;
            maxHp = _maxHp;
            mana = _mana;
            maxMana = _maxMana;
            maxDamage = _maximumDamage;
            maxDefense = _maxDefense;
            world = _world;
            Skills = new List<Skill>();
            Party = new List<Entity>();
            PartyDead = new List<Entity>();

            Party.Add(this);
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
