using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Equipable : Item
    {
        public int Slot { get; set; }
        public bool isEquipped { get; set; }

        public Equipable(int _id, string _name, string _namePlural, int _cost, Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            Equipable = true;
        }
        public Equipable(Equipable equ) :
            base(equ.ID, equ.Name, equ.NamePlural, equ.Cost, equ.Image)
        {
            Equipable = true;
        }
    }
    public class Weapon : Equipable
    {
        int minimumDamage;
        int maximumDamage;

        public Weapon(int _id, string _name, string _namePlural, int _minDamage, int _maxDamage, int _cost, int slot,  Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            minimumDamage = _minDamage;
            maximumDamage = _maxDamage;
        }

        public int MinimumDamage { get { return minimumDamage; } set { minimumDamage = value; } }
        public int MaximumDamage { get { return maximumDamage; } set { maximumDamage = value; } }
    }
}
