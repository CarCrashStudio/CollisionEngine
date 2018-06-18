namespace LinkEngine.Dungeon
{
    public class DungeonEquipment : DungeonItem
    {
        public short Slot { get; set; }

        public short Strength { get; set; }
        public short Defense { get; set; }

        public bool Equipped { get; set; }

        public DungeonEquipment (int _id, string _name, string _namePlural, short str, short def) : base(_id, _name, _namePlural, true, false)
        {
            Strength = str;
            Defense = def;
        }
    }
}
