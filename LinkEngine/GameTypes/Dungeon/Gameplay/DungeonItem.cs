using LinkEngine.Entities;

namespace LinkEngine.Dungeon
{
    public class DungeonItem : Item
    {
        public bool HasBeenDiscovered { get; set; }

        public string ItemName { get; set; }
        public string ItemNamePlural { get; set; }

        // The location of the image currently being used
        // This will change from game to game because the items will be randomized
        public string ImageLocation { get; set; }

        public DungeonItem(int _id, string _name, string _namePlural, bool equipable, bool consumable) : base(_id, _name, _namePlural)
        {
            HasBeenDiscovered = false;

            ItemName = "???";
            ItemNamePlural = "???";

            Equipable = equipable;
            Consumable = consumable;
        }
    }
}
