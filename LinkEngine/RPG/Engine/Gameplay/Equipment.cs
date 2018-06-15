namespace LinkEngine.RPG
{
    public class Equipment : Entities.Item
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int MaximumDefense { get; set; }
        public int MinimumDefense { get; set; }

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
        public Weapon(int _id, string _name, string _namePlural, int _minDamage, int _maxDamage, int _cost, int slot) :
            base(_id, _name, _namePlural, _cost)
        {
            MinimumDamage = _minDamage;
            MaximumDamage = _maxDamage;

            Slot = slot;
            Equipable = true;
        }

        
    }
    public class Armor : Equipment
    {
        public Armor(int _id, string _name, string _namePlural, int _maxDefense, int _minDefense, int _cost, int slot) :
            base(_id, _name, _namePlural, _cost)
        {
            MaximumDefense = _maxDefense;
            MinimumDefense = _minDefense;

            Slot = slot;
            Equipable = true;
        }
    }
}
