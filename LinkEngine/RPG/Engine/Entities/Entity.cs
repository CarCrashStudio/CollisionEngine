﻿using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Entity
    {
        int id;
        string name;
        int hp;
        int maxHp;
        int mana;
        int maxMana;
        int locX;
        int locY;
        int locZ;

        public short Strength { get; set; }
        public short Perception { get; set; }
        public short Endurance { get; set; }
        public short Charisma { get; set; }
        public short Intelligence { get; set; }
        public short Agility { get; set; }
        public short Luck { get; set; }

        public List<Modifier> StrengthModifiers { get; set; }
        public List<Modifier> PerceptionModifiers { get; set; }
        public List<Modifier> EnduranceModifiers { get; set; }
        public List<Modifier> CharismaModifiers { get; set; }
        public List<Modifier> IntelligenceModifiers { get; set; }
        public List<Modifier> AgilityModifiers { get; set; }
        public List<Modifier> LuckModifiers { get; set; }

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Facing { get; set; } // The direction the player is facing (North, South, East, West)
        public int Health { get { return hp; } set { hp = value; } }
        public int MaxHealth { get { return maxHp; } set { maxHp = value; } }
        public int Mana { get { return mana; } set { mana = value; } }
        public int MaxMana { get { return maxMana; } set { maxMana = value; } }
        public List<Ability> Abilities { get; set; }
        public List<Entity> Party { get; set; }
        public List<Entity> PartyDead { get; set; }

        

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
        public Entity(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana)
        {
            id = _id;
            name = _name;
            hp = _hp;
            maxHp = _maxHp;
            mana = _mana;
            maxMana = _maxMana;
            Abilities = new List<Ability>();
            Party = new List<Entity>();
            PartyDead = new List<Entity>();
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

        /// <summary>
        /// StrengthCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the strength mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
        public bool StrengthCheck (short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int str = Strength;

            if(modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    str += modifiers[i].ModifierAmount;
                }
            }
            

            // if the check is less than check
            if (str < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;
            
        }
        public bool PerceptionCheck(short check, Modifier[] modifiers)
        {
            // sets the current perception mod
            int per = Perception;
            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to perception value
                    per += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (per < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool EnduranceCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int end = Endurance;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    end += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (end < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool CharismaCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int cha = Charisma;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    cha += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (cha < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool IntelligenceCheck(short check, Modifier[] modifiers)
        {
            // sets the current perception mod
            int intel = Intelligence;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to perception value
                    intel += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (intel < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool AgilityCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int agi = Agility;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    agi += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (agi < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool LuckCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int luck = Luck;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    luck += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (luck < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;
        }

        public void AddModifier (Modifier mod)
        {
            switch(mod.TargetVariable)
            {
                case "Strength":
                    StrengthModifiers.Add(mod);
                    break;
                case "Perception":
                    PerceptionModifiers.Add(mod);
                    break;
                case "Endurance":
                    EnduranceModifiers.Add(mod);
                    break;
                case "Charisma":
                    CharismaModifiers.Add(mod);
                    break;
                case "Intelligence":
                    IntelligenceModifiers.Add(mod);
                    break;
                case "Agility":
                    AgilityModifiers.Add(mod);
                    break;
                case "Luck":
                    LuckModifiers.Add(mod);
                    break;
            }
        }

        public void RemoveMod(string name, List<Modifier> mods)
        {
            foreach (Modifier mod in mods)
            {
                if (mod.Name == name)
                {
                    mods.Remove(mod);
                }
            }
        }
    }
}
