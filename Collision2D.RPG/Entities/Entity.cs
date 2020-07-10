using Collision2D.RPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collision2D.RPG.Managers;
using Microsoft.Xna.Framework.Input;

namespace Collision2D.RPG.Entities
{
    public class Entity : Collision2D.Utils.Entities.Entity
    {
        private MouseState _currentState;
        private MouseState _previousState;

        public event EventHandler Click;
        public event EventHandler MouseDown;
        public event EventHandler MouseUp;
        public event EventHandler MouseHover;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;

        public bool IsUnarmed { get; set; }
        protected List<Equipment> equipped { get; set; }

        protected float _layer { get; set; }
        protected Vector2 _origin { get; set; }
        protected Vector2 _position { get; set; }
        protected float _rotation { get; set; }
        protected Texture2D _texture;
        public Color Colour { get; set; }

        public BuffManager Buffs { get; set; }
        /// <summary>
        /// Weapon Proficiencies increase Attack Check rolls by 2 if the entity is using a weapon they are profficient with.
        /// </summary>
        public IEnumerable<WeaponType> WeaponProficiencies { get; set; }
        public IEnumerable<Skills> SkillProficiencies { get; set; }
        public IEnumerable<Skills> SkillMasteries{ get; set; }

        public Equipment Head
        {
            get
            {
                return equipped[(int)Slots.Head];
            }
            set
            {
                equipped[(int)Slots.Head] = value;
            }
        }
        public Equipment Torso
        {
            get
            {
                return equipped[(int)Slots.Torso];
            }
            set
            {
                equipped[(int)Slots.Torso] = value;
            }
        }
        public Equipment Legs
        {
            get
            {
                return equipped[(int)Slots.Legs];
            }
            set
            {
                equipped[(int)Slots.Legs] = value;
            }
        }
        public Equipment Boots
        {
            get
            {
                return equipped[(int)Slots.Boots];
            }
            set
            {
                equipped[(int)Slots.Boots] = value;
            }
        }
        public Equipment MainHand
        {
            get
            {
                return equipped[(int)Slots.Main];
            }
            set
            {
                equipped[(int)Slots.Legs] = value;
            }
        }
        public Equipment OffHand
        {
            get
            {
                return equipped[(int)Slots.Off];
            }
            set
            {
                equipped[(int)Slots.Off] = value;
            }
        }

        public Race Race { get; set; }
        public Class Class { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Speed { get; set; }

        public new Attributes BaseAttributes { get; set; }
        public new IEnumerable<Attributes> AttributeModifiers { get; set; }
        public new Attributes TotalAttributes
        {
            get
            {
                return BaseAttributes + AttributeModifiers.Sum() + Buffs.InEffect.Sum().Mod;
            }
        }

        public new bool IsDead { get { return TotalAttributes.CurrentHP <= 0; } }

        protected void get_strength_base_modifier ()
        {
            if (Strength == 1)
                BaseAttributes.Strength = -5;
            else if (Strength >= 2 && Strength <= 3)
                BaseAttributes.Strength = -4;
            else if (Strength >= 4 && Strength <= 5)
                BaseAttributes.Strength = -3;
            else if (Strength >= 6 && Strength <= 7)
                BaseAttributes.Strength = -2;
            else if (Strength >= 8 && Strength <= 9)
                BaseAttributes.Strength = -1;
            else if (Strength >= 10 && Strength <= 11)
                BaseAttributes.Strength = 0;
            else if (Strength >= 12 && Strength <= 13)
                BaseAttributes.Strength = 1;
            else if (Strength >= 14 && Strength <= 15)
                BaseAttributes.Strength = 2;
            else if (Strength >= 16 && Strength <= 17)
                BaseAttributes.Strength = 3;
            else if (Strength >=18 && Strength <= 19)
                BaseAttributes.Strength = 4;
            else if (Strength >= 20 && Strength <= 21)
                BaseAttributes.Strength = 5;
            else if (Strength >= 22 && Strength <= 23)
                BaseAttributes.Strength = 6;
            else if (Strength >= 24 && Strength <= 25)
                BaseAttributes.Strength = 7;
            else if (Strength >= 26 && Strength <= 27)
                BaseAttributes.Strength = 8;
            else if (Strength >= 28 && Strength <= 29)
                BaseAttributes.Strength = 9;
            else if (Strength >= 30)
                BaseAttributes.Strength = 10;
        }
        protected void get_dexterity_base_modifier()
        {
            if (Dexterity == 1)
                BaseAttributes.Dexterity = -5;
            else if (Dexterity >= 2 && Dexterity <= 3)
                BaseAttributes.Dexterity = -4;
            else if (Dexterity >= 4 && Dexterity <= 5)
                BaseAttributes.Dexterity = -3;
            else if (Dexterity >= 6 && Dexterity <= 7)
                BaseAttributes.Dexterity = -2;
            else if (Dexterity >= 8 && Dexterity <= 9)
                BaseAttributes.Dexterity = -1;
            else if (Dexterity >= 10 && Dexterity <= 11)
                BaseAttributes.Dexterity = 0;
            else if (Dexterity >= 12 && Dexterity <= 13)
                BaseAttributes.Dexterity = 1;
            else if (Dexterity >= 14 && Dexterity <= 15)
                BaseAttributes.Dexterity = 2;
            else if (Dexterity >= 16 && Dexterity <= 17)
                BaseAttributes.Dexterity = 3;
            else if (Dexterity >= 18 && Dexterity <= 19)
                BaseAttributes.Dexterity = 4;
            else if (Dexterity >= 20 && Dexterity <= 21)
                BaseAttributes.Dexterity = 5;
            else if (Dexterity >= 22 && Dexterity <= 23)
                BaseAttributes.Dexterity = 6;
            else if (Dexterity >= 24 && Dexterity <= 25)
                BaseAttributes.Dexterity = 7;
            else if (Dexterity >= 26 && Dexterity <= 27)
                BaseAttributes.Dexterity = 8;
            else if (Dexterity >= 28 && Dexterity <= 29)
                BaseAttributes.Dexterity = 9;
            else if (Dexterity >= 30)
                BaseAttributes.Dexterity = 10;
        }
        protected void get_constitution_base_modifier()
        {
            if (Constitution == 1)
                BaseAttributes.Constitution = -5;
            else if (Constitution >= 2 && Constitution <= 3)
                BaseAttributes.Constitution = -4;
            else if (Constitution >= 4 && Constitution <= 5)
                BaseAttributes.Constitution = -3;
            else if (Constitution >= 6 && Constitution <= 7)
                BaseAttributes.Constitution = -2;
            else if (Constitution >= 8 && Constitution <= 9)
                BaseAttributes.Constitution = -1;
            else if (Constitution >= 10 && Constitution <= 11)
                BaseAttributes.Constitution = 0;
            else if (Constitution >= 12 && Constitution <= 13)
                BaseAttributes.Constitution = 1;
            else if (Constitution >= 14 && Constitution <= 15)
                BaseAttributes.Constitution = 2;
            else if (Constitution >= 16 && Constitution <= 17)
                BaseAttributes.Constitution = 3;
            else if (Constitution >= 18 && Constitution <= 19)
                BaseAttributes.Constitution = 4;
            else if (Constitution >= 20 && Constitution <= 21)
                BaseAttributes.Constitution = 5;
            else if (Constitution >= 22 && Constitution <= 23)
                BaseAttributes.Constitution = 6;
            else if (Constitution >= 24 && Constitution <= 25)
                BaseAttributes.Constitution = 7;
            else if (Constitution >= 26 && Constitution <= 27)
                BaseAttributes.Constitution = 8;
            else if (Constitution >= 28 && Constitution <= 29)
                BaseAttributes.Constitution = 9;
            else if (Constitution >= 30)
                BaseAttributes.Constitution = 10;
        }
        protected void get_intelligence_base_modifier()
        {
            if (Intelligence == 1)
                BaseAttributes.Intelligence = -5;
            else if (Intelligence >= 2 && Intelligence <= 3)
                BaseAttributes.Intelligence = -4;
            else if (Intelligence >= 4 && Intelligence <= 5)
                BaseAttributes.Intelligence = -3;
            else if (Intelligence >= 6 && Intelligence <= 7)
                BaseAttributes.Intelligence = -2;
            else if (Intelligence >= 8 && Intelligence <= 9)
                BaseAttributes.Intelligence = -1;
            else if (Intelligence >= 10 && Intelligence <= 11)
                BaseAttributes.Intelligence = 0;
            else if (Intelligence >= 12 && Intelligence <= 13)
                BaseAttributes.Intelligence = 1;
            else if (Intelligence >= 14 && Intelligence <= 15)
                BaseAttributes.Intelligence = 2;
            else if (Intelligence >= 16 && Intelligence <= 17)
                BaseAttributes.Intelligence = 3;
            else if (Intelligence >= 18 && Intelligence <= 19)
                BaseAttributes.Intelligence = 4;
            else if (Intelligence >= 20 && Intelligence <= 21)
                BaseAttributes.Intelligence = 5;
            else if (Intelligence >= 22 && Intelligence <= 23)
                BaseAttributes.Intelligence = 6;
            else if (Intelligence >= 24 && Intelligence <= 25)
                BaseAttributes.Intelligence = 7;
            else if (Intelligence >= 26 && Intelligence <= 27)
                BaseAttributes.Intelligence = 8;
            else if (Intelligence >= 28 && Intelligence <= 29)
                BaseAttributes.Intelligence = 9;
            else if (Intelligence >= 30)
                BaseAttributes.Intelligence = 10;
        }
        protected void get_wisdom_base_modifier()
        {
            if (Wisdom == 1)
                BaseAttributes.Wisdom = -5;
            else if (Wisdom >= 2 && Wisdom <= 3)
                BaseAttributes.Wisdom = -4;
            else if (Wisdom >= 4 && Wisdom <= 5)
                BaseAttributes.Wisdom = -3;
            else if (Wisdom >= 6 && Wisdom <= 7)
                BaseAttributes.Wisdom = -2;
            else if (Wisdom >= 8 && Wisdom <= 9)
                BaseAttributes.Wisdom = -1;
            else if (Wisdom >= 10 && Wisdom <= 11)
                BaseAttributes.Wisdom = 0;
            else if (Wisdom >= 12 && Wisdom <= 13)
                BaseAttributes.Wisdom = 1;
            else if (Wisdom >= 14 && Wisdom <= 15)
                BaseAttributes.Wisdom = 2;
            else if (Wisdom >= 16 && Wisdom <= 17)
                BaseAttributes.Wisdom = 3;
            else if (Wisdom >= 18 && Wisdom <= 19)
                BaseAttributes.Wisdom = 4;
            else if (Wisdom >= 20 && Wisdom <= 21)
                BaseAttributes.Wisdom = 5;
            else if (Wisdom >= 22 && Wisdom <= 23)
                BaseAttributes.Wisdom = 6;
            else if (Wisdom >= 24 && Wisdom <= 25)
                BaseAttributes.Wisdom = 7;
            else if (Wisdom >= 26 && Wisdom <= 27)
                BaseAttributes.Wisdom = 8;
            else if (Wisdom >= 28 && Wisdom <= 29)
                BaseAttributes.Wisdom = 9;
            else if (Wisdom >= 30)
                BaseAttributes.Wisdom = 10;
        }
        protected void get_charisma_base_modifier()
        {
            if (Charisma == 1)
                BaseAttributes.Charisma = -5;
            else if (Charisma >= 2 && Charisma <= 3)
                BaseAttributes.Charisma = -4;
            else if (Charisma >= 4 && Charisma <= 5)
                BaseAttributes.Charisma = -3;
            else if (Charisma >= 6 && Charisma <= 7)
                BaseAttributes.Charisma = -2;
            else if (Charisma >= 8 && Charisma <= 9)
                BaseAttributes.Charisma = -1;
            else if (Charisma >= 10 && Charisma <= 11)
                BaseAttributes.Charisma = 0;
            else if (Charisma >= 12 && Charisma <= 13)
                BaseAttributes.Charisma = 1;
            else if (Charisma >= 14 && Charisma <= 15)
                BaseAttributes.Charisma = 2;
            else if (Charisma >= 16 && Charisma <= 17)
                BaseAttributes.Charisma = 3;
            else if (Charisma >= 18 && Charisma <= 19)
                BaseAttributes.Charisma = 4;
            else if (Charisma >= 20 && Charisma <= 21)
                BaseAttributes.Charisma = 5;
            else if (Charisma >= 22 && Charisma <= 23)
                BaseAttributes.Charisma = 6;
            else if (Charisma >= 24 && Charisma <= 25)
                BaseAttributes.Charisma = 7;
            else if (Charisma >= 26 && Charisma <= 27)
                BaseAttributes.Charisma = 8;
            else if (Charisma >= 28 && Charisma <= 29)
                BaseAttributes.Charisma = 9;
            else if (Charisma >= 30)
                BaseAttributes.Charisma = 10;
        }

        protected Entity(Texture2D texture, Vector2 pos)
        {
            Sprite = new Utils.Sprite(texture);
            Sprite.Position = pos * Sprite.IconSize;
        }
        protected Entity(Dictionary<string, Utils.Animation> animations, Vector2 pos)
        {
            Sprite = new Utils.Sprite(animations);
            Sprite.Position = pos * Sprite.IconSize;
        }

        public void Equip (Equipment equipment, int ChosenSlot)
        {
            if (equipment.Slots.Contains((Slots)ChosenSlot))
            {
                // the equipment can go into the selected slot
                equipped[ChosenSlot] = equipment;

                // add the attribute modifiers of the equipment to the entity
                AttributeModifiers.Append(equipment.Mod);
            }
        }
        public void Unequip(Equipment equipment)
        {
                // the equipment can go into the selected slot
                equipped.Remove(equipment);

                // add the attribute modifiers of the equipment to the entity
                AttributeModifiers = AttributeModifiers.Where(x=>x!=equipment.Mod);
        }

        public void Update(GameTime gameTime)
        {
            // set the previous state to the mouse state of the previous frame
            _previousState = _currentState;

            // set the currentState to the mouse State of the current frame
            _currentState = Mouse.GetState();

            var currentMouseRectangle = new Rectangle(_currentState.X, _currentState.Y, 1, 1);
            var previousMouseRectangle = new Rectangle(_previousState.X, _previousState.Y, 1, 1);

            // check if the mouse is over the control
            if (currentMouseRectangle.Intersects(Sprite.Hitbox))
            {
                // check if the mouse was clicked
                if (_currentState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
                {
                    // the mouse was clicked
                    // lets invoke the click method
                    Click?.Invoke(this, new EventArgs());
                }
                else
                {
                    // check if the mouse was over the control previously
                    if (previousMouseRectangle.Intersects(Sprite.Hitbox))
                    {
                        // the mouse is hovering over the control
                        // so lets invoke the hover event
                        MouseHover?.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        // the mouse has entered the control
                        // so lets invoke the MouseEnter event
                        MouseEnter?.Invoke(this, new EventArgs());
                    }
                }
            }
            // check if the mouse left the control
            else if (!currentMouseRectangle.Intersects(Sprite.Hitbox) && previousMouseRectangle.Intersects(Sprite.Hitbox))
            {
                // Mouse has left the control bounds
                // Lets invoke the control leave event
                MouseLeave?.Invoke(this, new EventArgs());
            }
        }
    }
}
