using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoLink2D;
using LinkEngine.RPG2D.Models;
using Microsoft.Xna.Framework;

namespace LinkEngine.RPG2D.Entities
{
    /// <summary>
    /// Character is the class that holds all the needed data for the player to use.
    /// </summary>
    public class Adventurer : Entity
    {
        /// <summary>
        /// Invenotry is a list of all items the player has collected this game
        /// </summary>
        public IEnumerable<InventoryItem> Inventory { get; set; }
        /// <summary>
        /// Equipment is an array of the items the player has equipped. Default maximum size of 6
        /// </summary>
        public IEnumerable<Equipment> Equipment { get; set; }
        /// <summary>
        /// A list of all quests the player has, both completed and in progress
        /// </summary>
        public IEnumerable<PlayerQuest> Quests { get; set; }

        public new Input Input { get; set; }

        
        public int Gold { get; set; }

        /// <summary>
        /// Creates a new adventurer, the player class of the RPG Library. Inherits Entities.Player in LinkEngine
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_clss"></param>
        /// <param name="_hp"></param>
        /// <param name="_maxHp"></param>
        /// <param name="_maximumDamage"></param>
        /// <param name="_maxDefense"></param>
        /// <param name="_level"></param>
        /// <param name="_exp"></param>
        /// <param name="_maxExp"></param>
        /// <param name="_gold"></param>
        /// <param name="slug"></param>
        public Adventurer(int _id, string _name, int _level, int _gold, Texture2D texture, Vector2 pos) : 
            base(texture, pos)
        {
        }

        /// <summary>
        /// Overrides Player GiveExperience to use the Intelligence Modifier
        /// </summary>
        /// <param name="exp"></param>
        public void giveExperience(int exp)
        {
            //if (Intelligence > 0)
            //{
            //    Exp += (int)(exp + ((exp * 0.10) * Intelligence));
            //}
        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and place the item there.
        /// This will add the modifier to the player
        /// </summary>
        /// <param name="equ">The item to equip</param>
        public void Equip(Equipment equ)
        {

        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and set the slot to null.
        /// This will remove the modifier from the player
        /// </summary>
        /// <param name="equ">The item to unequip</param>
        public void Unequip(Equipment equ)
        {
            
        }
        public bool HasThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }

            return false;
        }
        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }
        public bool HasAllQuestCompletionItems(Quest quest)
        {
            // See if the player has all the items needed to complete the quest here
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayersInventory = false;

                // Check each item in the player's inventory, to see if they have it, and enough of it
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID) // The player has the item in their inventory
                    {
                        foundItemInPlayersInventory = true;

                        if (ii.Quantity < qci.Quantity) // The player does not have enough of this item to complete the quest
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
        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }
        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            foreach (PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID)
                {
                    // Mark it as completed
                    pq.IsCompleted = true;

                    return; // We found the quest, and marked it complete, so get out of this function
                }
            }
        }
        public void RecieveQuest(Quest Quest)
        {
            // See if the player already has the quest, and if they've completed it
            bool playerAlreadyHasQuest = false;
            bool playerAlreadyCompletedQuest = false;

            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == Quest.ID)
                {
                    playerAlreadyHasQuest = true;

                    if (playerQuest.IsCompleted)
                    {
                        playerAlreadyCompletedQuest = true;
                    }
                }
            }

            // See if the player already has the quest
            if (playerAlreadyHasQuest)
            {
                // If the player has not completed the quest yet
                if (!playerAlreadyCompletedQuest)
                {
                    // See if the player has all the items needed to complete the quest
                    bool playerHasAllItemsToCompleteQuest = true;

                    foreach (QuestCompletionItem qci in Quest.QuestCompletionItems)
                    {
                        bool foundItemInPlayersInventory = false;

                        // Check each item in the player's inventory, to see if they have it, and enough of it
                        foreach (InventoryItem ii in Inventory)
                        {
                            // The player has this item in their inventory
                            if (ii.Details.ID == qci.Details.ID)
                            {
                                foundItemInPlayersInventory = true;

                                if (ii.Quantity < qci.Quantity)
                                {
                                    // The player does not have enough of this item to complete the quest
                                    playerHasAllItemsToCompleteQuest = false;

                                    // There is no reason to continue checking for the other quest completion items
                                    break;
                                }

                                // We found the item, so don't check the rest of the player's inventory
                                break;
                            }
                        }

                        // If we didn't find the required item, set our variable and stop looking for other items
                        if (!foundItemInPlayersInventory)
                        {
                            // The player does not have this item in their inventory
                            playerHasAllItemsToCompleteQuest = false;

                            // There is no reason to continue checking for the other quest completion items
                            break;
                        }
                    }

                    // The player has all items required to complete the quest
                    if (playerHasAllItemsToCompleteQuest)
                    {
                        // Display message
                        // World.Output.Text += Environment.NewLine;
                        // World.Output.Text += "You complete the '" + npc.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                        // Remove quest items from inventory
                        foreach (QuestCompletionItem qci in Quest.QuestCompletionItems)
                        {
                            foreach (InventoryItem ii in Inventory)
                            {
                                if (ii.Details.ID == qci.Details.ID)
                                {
                                    // Subtract the quantity from the player's inventory that was needed to complete the quest
                                    ii.Quantity -= qci.Quantity;
                                    break;
                                }
                            }
                        }

                        // Give quest rewards
                        giveExperience(Quest.RewardExperiencePoints);

                        // Add the reward item to the player's inventory
                        bool addedItemToPlayerInventory = false;

                        foreach (InventoryItem ii in Inventory)
                        {
                            if (ii.Details.ID == Quest.RewardItem.ID)
                            {
                                // They have the item in their inventory, so increase the quantity by one
                                ii.Quantity++;

                                addedItemToPlayerInventory = true;

                                break;
                            }
                        }

                        // They didn't have the item, so add it to their inventory, with a quantity of 1
                        if (!addedItemToPlayerInventory)
                        {
                            //Inventory.Add(new InventoryItem(Quest.RewardItem, 1));
                        }

                        // Mark the quest as completed
                        // Find the quest in the player's quest list
                        foreach (PlayerQuest pq in Quests)
                        {
                            if (pq.Details.ID == Quest.ID)
                            {
                                // Mark it as completed
                                pq.IsCompleted = true;

                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                // The Player does not already have the quest
                // Add the quest to the player's quest list
                //Quests.Add(new PlayerQuest(Quest));
            }
            //World.HUD.Update();
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
            //Inventory.Add(new InventoryItem(itemToAdd, 1));
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
                        //Inventory.Remove(ii);
                    }
                    return; // We added the item, and are done, so get out of this function
                }
            }
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
        public Equipment EquipmentByName(string name)
        {
            foreach (Equipment equ in Equipment)
            {
                if (equ.Details.Name == name)
                {
                    return equ;
                }
            }
            return null;
        }
    }
}
