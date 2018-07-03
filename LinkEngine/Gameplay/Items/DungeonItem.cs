using LinkEngine.Entities;
using LinkEngine.Gameplay.Items;

namespace LinkEngine.Dungeon
{
    public class DungeonItem
    {
        /// <summary>
        /// Bool flag indicating whether this item has been been discovered or is still unknown
        /// </summary>
        public bool HasBeenDiscovered { get; set; }
        
        public Item Details { get; set; }
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
        public DungeonItem(int _id, string _name, string _namePlural, bool equipable, bool consumable)
        {
            HasBeenDiscovered = false;
        }
    }
}
