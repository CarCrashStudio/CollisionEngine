using LinkEngine.WorldGen;
using System.Collections.Generic;

namespace LinkEngine.Survival
{
    public class Survivalist : Entities.Player
    {
        // Character class is the the playable class of the game
        // Inherits from Entities.Player

        public short Hunger { get; set; }
        public bool Full { get; set; }

        /// <summary>
        /// The inventory of the player can be accessed by any gametype player class that inherits this class.
        /// </summary>
        public List<InventoryItem> Inventory;

        public Survivalist () : base (0, "Survivalist", 100, 100)
        {

        }

        public void Craft(Item itemToCraft)
        {
            if (HasAllCraftingRecipeItems(itemToCraft))
            {
                RemoveCraftingRecipeItems(itemToCraft);
                AddItemToInventory(itemToCraft);
            }
        }

        public void LoseHunger(short amount)
        {
            Hunger -= amount;
        }
        
        public void Eat (short amountToFeed)
        {
            Hunger += amountToFeed;
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
                if (ii.Details.Name == location.ItemRequiredToEnter)
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
                    if (ii.Details.Name == newLocation.ItemRequiredToEnter)
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
