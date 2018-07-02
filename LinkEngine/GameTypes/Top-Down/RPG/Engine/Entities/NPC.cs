using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class NPC : Entities.Entity
    {
        public bool Interactable { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Shop ShopavailableHere { get; set; }

        /// <summary>
        /// Creates a new NPC Object. NPCs can be used to create shops, quest givers, or just display a message when interacted with
        /// </summary>
        /// <param name="_id">The ID to give the NPC</param>
        /// <param name="_name">The Name of the npc</param>
        /// <param name="x">Starting X coordinate, most likely defaults to 0</param>
        /// <param name="y">Starting Y coordinate, most likely defaults to 0</param>
        public NPC(int _id, string _name, int x, int y, Quest _questavailableHere, Shop _shopavailablehere) : base(_id, _name, 1, 1)
        {
            collider.Transform.X = x;
            collider.Transform.Y = y;
            QuestAvailableHere = _questavailableHere;
            ShopavailableHere = _shopavailablehere;
        }
        /// <summary>
        /// Creates a copy of an NPC
        /// </summary>
        /// <param name="npc">The npc to copy</param>
        public NPC(NPC npc) : base (npc.ID, npc.Name, 1,1)
        {
            collider.Transform.X = npc.collider.Transform.X;
            collider.Transform.Y = npc.collider.Transform.Y;
            QuestAvailableHere = npc.QuestAvailableHere;
            // ShopavailableHere = npc.ShopavailableHere;
        }
    }

    /// <summary>
    /// Shop holds the inventory of all items that can be bought at this vendor
    /// Contains an empty default contructor
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// The list of items that can be bought at this vendor
        /// </summary>
        public List<RPGItem> Inventory { get; set; }
    }
}
