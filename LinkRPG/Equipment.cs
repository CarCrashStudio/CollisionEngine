namespace RPG
{
    public class Equipment : LinkEngine.Gameplay.Items.Item
    {
        public int StrengthBoost { get; set; }
        public int DefenseBoost { get; set; }

        /// <summary>
        /// Mod will be used to boost a player's skill only while it is equipped
        /// </summary>
        public Modifier Mod { get; set; }

        /// <summary>
        /// The index of where this item should go in the player's Equipment list
        /// </summary>
        public int Slot { get; set; }
        /// <summary>
        /// Boolean flag indicating whether this item is equipped or not
        /// </summary>
        public bool IsEquipped { get; set; }

        /// <summary>
        /// Creates a new Equipment from the parameters given
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_namePlural"></param>
        /// <param name="_cost"></param>
        public Equipment(int _id, string _name, string _namePlural, int cost, int slot, Modifier mod) :
            base(_id, _name, _namePlural, cost)
        {
            Slot = slot;
            Mod = mod;

            Equipable = true;
            
        }
        /// <summary>
        /// Creates a copy of a piece of Equipment
        /// </summary>
        /// <param name="equ">The Equipment to copy</param>
        public Equipment(Equipment equ) :
            base(equ.ID, equ.Name, equ.NamePlural, equ.Cost)
        {
            Slot = equ.Slot;
            Mod = equ.Mod;

            Equipable = true;
        }
    }
}
