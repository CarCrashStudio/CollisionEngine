using LinkEngine.Entities;

namespace LinkEngine.Dungeon
{
    public class DungeonItem : Item
    {
        /// <summary>
        /// Bool flag indicating whether this item has been been discovered or is still unknown
        /// </summary>
        public bool HasBeenDiscovered { get; set; }

        /// <summary>
        /// The Name of the Item. Defaults as "???" until it has been discovered
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// The plural Name of the Item. Defaults as "???" until it has been discovered
        /// </summary>
        public string ItemNamePlural { get; set; }

        /// <summary>
        /// The location of the image. image will be changed from game to game to create a randomized mechanic
        /// </summary>
        public string ImageLocation { get; set; }

        /// <summary>
        /// Creates a new DungeonItem
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_namePlural"></param>
        /// <param name="equipable"></param>
        /// <param name="consumable"></param>
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
