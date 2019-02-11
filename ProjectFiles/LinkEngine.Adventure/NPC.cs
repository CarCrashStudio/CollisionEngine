using LinkEngine.Components;
using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.Adventure
{
    public class NPC : Entity
    {
        public bool Interactable { get; set; }
        public Shop Shop { get; set; }

        /// <summary>
        /// Creates a new NPC Object. NPCs can be used to create shops, quest givers, or just display a message when interacted with
        /// </summary>
        /// <param name="_id">The ID to give the NPC</param>
        /// <param name="_name">The Name of the npc</param>
        /// <param name="x">Starting X coordinate, most likely defaults to 0</param>
        /// <param name="y">Starting Y coordinate, most likely defaults to 0</param>
        public NPC(int _id, string _name, int x, int y, Shop _shopavailablehere) : base(_id, _name, 1, 1)
        {
            ((Collider2D)Components[0]).Transform.Position.X = x;
            ((Collider2D)Components[0]).Transform.Position.Y = y;
            Shop = _shopavailablehere;
        }
        /// <summary>
        /// Creates a copy of an NPC
        /// </summary>
        /// <param name="npc">The npc to copy</param>
        public NPC(NPC npc) : base (npc.ID, npc.Name, 1,1)
        {
            ((Collider2D)Components[0]).Transform.Position.X = ((Collider2D)npc.Components[0]).Transform.Position.X;
            ((Collider2D)Components[0]).Transform.Position.Y = ((Collider2D)npc.Components[0]).Transform.Position.Y;
            Shop = npc.Shop;
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
        public List<InventoryItem> Stock { get; set; }

        /// <summary>
        /// The gold that
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// Buy will take a player object and an RPGInventoryItem object and check that the player has enough gold to buy it, then add it to his inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="selectedItem"></param>
        public void Buy (Adventurer player, InventoryItem selectedItem)
        {
            // Check to see if the player has more gold than the selected item cost
            if (player.Gold > (selectedItem.Details.Cost * selectedItem.Quantity))
            {
                // The player can add the items to his inventory
                for(int i = 0; i < selectedItem.Quantity; i++)
                {
                    // add one item at a time
                    player.AddItemToInventory(selectedItem.Details);

                    // remove gold one item at a time
                    player.Gold -= selectedItem.Details.Cost;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="selectedItem"></param>
        public void Sell (Adventurer player, InventoryItem selectedItem)
        {
            // Check to see if the NPC has more gold than the selected item cost
            if (Gold > (selectedItem.Details.Cost * selectedItem.Quantity))
            {
                // The player can remove the items from his inventory
                for (int i = 0; i < selectedItem.Quantity; i++)
                {
                    // remove one item at a time
                    player.RemoveItemFromInventory(selectedItem.Details);

                    // add gold one item at a time
                    player.Gold += selectedItem.Details.Cost;
                }
            }
        }
    }
}
