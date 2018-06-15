namespace LinkEngine.RPG
{
    public class Equipment : RPGItem
    {
        public int StrengthBoost { get; set; }
        public int DefenseBoost { get; set; }

        public int Slot { get; set; }
        public bool IsEquipped { get; set; }

        public Equipment(int _id, string _name, string _namePlural, int _cost) :
            base(_id, _name, _namePlural, _cost)
        {
            Equipable = true;
            
        }
        public Equipment(Equipment equ) :
            base(equ.ID, equ.Name, equ.NamePlural, equ.Cost)
        {
            Equipable = true;
            Slot = equ.Slot;
        }
    }
    public class Weapon : Equipment
    {
        public Weapon(int _id, string _name, string _namePlural, int strength, int _cost, int slot) :
            base(_id, _name, _namePlural, _cost)
        {
            StrengthBoost = strength;

            Slot = slot;
            Equipable = true;
        }        
    }
    public class Armor : Equipment
    {
        public Armor(int _id, string _name, string _namePlural, int defense, int _cost, int slot) :
            base(_id, _name, _namePlural, _cost)
        {
            DefenseBoost = defense;

            Slot = slot;
            Equipable = true;
        }
    }
}
