using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Equipment : Item
    {
        public int Slot { get; set; }
        public bool isEquipped { get; set; }

        public Equipment(int _id, string _name, string _namePlural, int _cost, Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            Equipable = true;
        }
        public Equipment(Equipment equ) :
            base(equ.ID, equ.Name, equ.NamePlural, equ.Cost, equ.Image)
        {
            Equipable = true;
            Slot = equ.Slot;
        }
    }
    public class Weapon : Equipment
    {
        int minimumDamage;
        int maximumDamage;

        public Weapon(int _id, string _name, string _namePlural, int _minDamage, int _maxDamage, int _cost, int slot,  Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            minimumDamage = _minDamage;
            maximumDamage = _maxDamage;

            Slot = slot;
        }

        public int MinimumDamage { get { return minimumDamage; } set { minimumDamage = value; } }
        public int MaximumDamage { get { return maximumDamage; } set { maximumDamage = value; } }
    }
    public class Armor : Equipment
    {
        public Armor(int _id, string _name, string _namePlural, int _maxDamgage, int _maxDefense, int _cost, int slot, Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            Equipable = true;
        }
    }
}
