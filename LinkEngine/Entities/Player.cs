using LinkEngine.Rendering;
using System.Collections.Generic;
using System.Drawing;

namespace LinkEngine.Entities
{
    public class Player : Entity
    {
        /// <summary>
        /// The inventory of the player can be accessed by any gametype player class that inherits this class.
        /// </summary>
        public List<InventoryItem> Inventory;
        public Point MapLoc { get; set; }

        /// <summary>
        /// The camera object is what will be used to hold the image the player can see
        /// </summary>
        public Camera camera { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="maxHealth"></param>
        public Player(int id, string name, int health, int maxHealth) : base (id, name, health, maxHealth)
        {

        }

        /// <summary>
        /// ShiftMap moves the map in any direction
        /// Used when keeping the player center screen
        /// </summary>
        /// <param name="x">the amount to shift the map on the X axis</param>
        /// <param name="y">the amount to shift the map on the Y axis</param>
        public void ShiftMap(int x, int y)
        {
            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);
        }
        /// <summary>
        /// SetMap returns the drawn map to use in pictureboxes
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public void SetMap(int Width, int Height)
        {
            camera.View = ScreenObject.Draw(Width, Height, new Point(MapLoc.X * 32, (MapLoc.Y - 10) * 32), null);
        }
        /// <summary>
        /// CalibrateMap will adjust the map location so that the player can be centered on the screen
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        void CalibrateMap(int centerX, int centerY)
        {
            MapLoc = new Point(
                    // X Coordinate
                    centerX - camera.X,

                    // Y Coordinate
                    centerY - camera.Y
                );
        }

        public bool HasAllCraftingRecipeItems(Item craft)
        {
            // See if the player has all the items needed to complete the quest here
            foreach (CraftingItem ci in craft.Recipe)
            {
                bool foundItemInPlayersInventory = false;

                // Check each item in the player's inventory, to see if they have it, and enough of it
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == ci.Details.ID) // The player has the item in their inventory
                    {
                        foundItemInPlayersInventory = true;

                        if (ii.Quantity < ci.Quantity) // The player does not have enough of this item to complete the quest
                        {
                            return false;
                        }
                    }
                }

                // The player does not have any of this quest completion item in their inventory
                if (!foundItemInPlayersInventory)
                {
                    return false;
                }
            }

            // If we got here, then the player must have all the required items, and enough of them, to complete the quest.
            return true;
        }
        public void RemoveCraftingRecipeItems(Item craft)
        {
            foreach (CraftingItem ci in craft.Recipe)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == ci.Details.ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        ii.Quantity -= ci.Quantity;
                        break;
                    }
                }
            }
        }
        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Quantity++;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }
        public void RemoveItemFromInventory(Item itemToRemove)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToRemove.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Quantity--;
                    if (ii.Quantity <= 0)
                    {
                        Inventory.Remove(ii);
                    }
                    return; // We added the item, and are done, so get out of this function
                }
            }
        }
    }
}
