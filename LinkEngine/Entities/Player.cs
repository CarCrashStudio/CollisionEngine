using LinkEngine.Gameplay.Items;
using LinkEngine.WorldGen;
using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Player : Entity
    {
        System.Random rand = new System.Random();

        /// <summary>
        /// The inventory of the player can be accessed by any gametype player class that inherits this class.
        /// </summary>
        public List<InventoryItem> Inventory;



        /// <summary>
        /// The current level of the Player
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// The amassed experience of the Player
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// the required exp to level up
        /// </summary>
        public int MaxExp { get; set; }

        /// <summary>
        /// the amount of gold the player currently has
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The location the Player is currently in
        /// </summary>
        public Location CurrentLocation { get; set; }

        /// <summary>
        /// The camera object is what will be used to hold the image the player can see
        /// </summary>
        public Components.Camera camera { get; set; }

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
            // MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);
        }

        /// <summary>
        /// CalibrateMap will adjust the map location so that the player can be centered on the screen
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        void CalibrateMap(int centerX, int centerY)
        {
            //MapLoc = new Point(
            //        // X Coordinate
            //        centerX - camera.X,

            //        // Y Coordinate
            //        centerY - camera.Y
            //    );
        }

        public void giveExperience(int exp)
        {
            Exp += exp;
        }
        public void LevelUp()
        {
            if (Exp >= MaxExp)
            {
                Level++;
                MaxExp += rand.Next(100, MaxExp);
            }
        }

        /// <summary>
        /// Checks to see if the Items in the PLayer's Inventory match the Item to craft's recipe
        /// </summary>
        /// <param name="craft"></param>
        /// <returns>Returns true if the player has the correct items</returns>
        public bool HasAllCraftingRecipeItems(Item craft)
        {
            // See if the player has all the items needed to complete the recipe here
            foreach (CraftingItem ci in craft.Recipe)
            {
                bool foundItemInPlayersInventory = false;

                // Check each item in the player's inventory, to see if they have it, and enough of it
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == ci.Details.ID) // The player has the item in their inventory
                    {
                        foundItemInPlayersInventory = true;

                        if (ii.Quantity < ci.Quantity) // The player does not have enough of this item to complete the recipe
                        {
                            return false;
                        }
                    }
                }

                // The player does not have any of this recipe completion item in their inventory
                if (!foundItemInPlayersInventory)
                {
                    return false;
                }
            }

            // If we got here, then the player must have all the required items, and enough of them, to complete the recipe.
            return true;
        }
        /// <summary>
        /// Removes the items needed to craft the item
        /// </summary>
        /// <param name="craft"></param>
        public void RemoveCraftingRecipeItems(Item craft)
        {
            foreach (CraftingItem ci in craft.Recipe)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == ci.Details.ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the recipe
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




        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                // There is no required item for this location, so return "true"
                return true;
            }

            // See if the player has the required item in their inventory
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    // We found the required item, so return "true"
                    return true;
                }
            }

            // We didn't find the required item in their inventory, so return "false"
            return false;
        }
        public void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if (newLocation.ItemRequiredToEnter != null)
            {
                // See if the player has the required item in their inventory
                bool playerHasRequiredItem = false;

                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == newLocation.ItemRequiredToEnter.ID)
                    {
                        // We found the required item
                        playerHasRequiredItem = true;
                        break; // Exit out of the foreach loop
                    }
                }

                if (!playerHasRequiredItem)
                {
                    // We didn't find the required item in their inventory, so display a message and stop trying to move
                    return;
                }
            }
            CurrentLocation = newLocation;
            try
            {
                CurrentLocation.MonsterLivingHere = newLocation.MonsterLivingHere;
            }
            catch { CurrentLocation.MonsterLivingHere = null; }

            //World.HUD.UpdateNPCs();
        }

        public InventoryItem ItemByName(string name)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.Name == name)
                {
                    return ii;
                }
            }
            return null;
        }

    }
}
