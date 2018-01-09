﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace RPG.Engine
{
    public class Entity
    {
        int id;
        string name;
        int hp;
        int maxHp;
        int mana;
        int maxMana;
        int maxDamage;
        int maxDefense;
        int locX;
        int locY;
        int locZ;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Facing { get; set; } // The direction the player is facing (North, South, East, West)
        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public List<Skill> Skills { get; set; }
        public List<Entity> Party { get; set; }
        public List<Entity> PartyDead { get; set; }

        public int Strength { get { return maxDamage; } set { maxDamage = value; } }
        public int Defense { get { return maxDefense; } set { maxDefense = value; } }

        public int Location_X { get { return locX; } set { locX = value; } }
        public int Location_Y { get { return locY; } set { locY = value; } }
        public int Location_Z { get { return locZ; } set { locZ = value; } }

        /// <summary>
        /// Creates a new object of the Entity class
        /// </summary>
        /// <param name="_id">The id for the object</param>
        /// <param name="_name">The name of the object</param>
        /// <param name="_hp">The staring amount of health the object has</param>
        /// <param name="_maxHp">The maximum amount of hp this object can have</param>
        /// <param name="_mana">Starting amount of Mana</param>
        /// <param name="_maxMana">Max amount of mana</param>
        /// <param name="_maximumDamage">Starting amount of strength</param>
        /// <param name="_maxDefense">Starting amount of defense</param>
        public Entity(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense)
        {
            id = _id;
            name = _name;
            hp = _hp;
            maxHp = _maxHp;
            mana = _mana;
            maxMana = _maxMana;
            maxDamage = _maximumDamage;
            maxDefense = _maxDefense;
            Skills = new List<Skill>();
            Party = new List<Entity>();
            PartyDead = new List<Entity>();

            Party.Add(this);
        }

        /// <summary>
        /// Change the entities X and Y coordinates
        /// </summary>
        /// <param name="x">The value to change the X coordinate</param>
        /// <param name="y">The Value to change the Y coordinate</param>
        public void Move(int x, int y)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            locX += x;
            locY += y;
        }

        /// <summary>
        /// Check if the entity health is 0
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsDead()
        {
            if (hp <= 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Removes a party member from the active list and places it in the dead list to be revived
        /// </summary>
        /// <param name="partymember">The party member to kill</param>
        public void KillPartyMember(Entity partymember)
        {
            Party.Remove(partymember);
            PartyDead.Add(partymember);
        }
    }
}
