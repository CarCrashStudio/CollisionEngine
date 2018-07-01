namespace LinkEngine.Dungeon
{
    public class DungeonPotion : DungeonItem
    {
        public string TargetVariable { get; set; }
        public short ModifierAmount { get; set; }

        public DungeonPotion(int _id, string _name, string _namePlural, short modifier, string target) : base(_id, _name, _namePlural, false, true)
        {
            ModifierAmount = modifier;
            TargetVariable = target;
        }
    }
}
