using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Collision2D;
using Collision2D.RPG.Models;
using Microsoft.Xna.Framework;

namespace Collision2D.RPG.Entities
{
    public class Adventurer : Entity
    {
        private ulong experience;
        private ulong max_experience;
        private byte level;
        public byte Level { get => level; }

        /// <summary>
        /// A list of all Items and the Quantities of those items collected by the player
        /// </summary>
        public List<InventoryItem> Inventory { get; set; }

        /// <summary>
        /// A list of all quests the player has undertaken
        /// </summary>
        public List<PlayerQuest> Quests { get; set; }
        /// <summary>
        /// The controls available to the Adventurer
        /// </summary>
        public Input Input { get; set; }

        /// <summary>
        /// The total amount of money the player has.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The amount of experience earned by the Adventurer. Runs an Intelligence Check to determine whether to boost the exp.
        /// </summary>
        public ulong Experience { get => experience; set => experience += GiveExperience(value, 20); }
        /// <summary>
        /// The amount of EXP needed to advance to the next level.
        /// </summary>
        public ulong MaxExperience { get => max_experience; }
 
        /// <summary>
        /// Creates a new Adventurer object with a determined set of stats. Uses a static Texture2D image as it's sprite.
        /// </summary>
        public Adventurer(int _id, string _name, Texture2D texture, Vector2 pos, Race _race = null, Class _class = null, int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0) :
            base(texture, pos)
        {
            equipped = new List<Equipment>();
            Inventory = new List<InventoryItem>();
            Class = _class;

            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Constitution = constitution;
            Charisma = charisma;

            if (_class != null)
            {
                Strength += _class.Strength;
                Dexterity += _class.Dexterity;
                Constitution += _class.Constitution;
                Intelligence += _class.Intelligence;
                Wisdom += _class.Wisdom;
                Charisma += _class.Charisma;
            }
            if (_race != null)
            {
                Strength += _race.Strength;
                Dexterity += _race.Dexterity;
                Constitution += _race.Constitution;
                Intelligence += _race.Intelligence;
                Wisdom += _race.Wisdom;
                Charisma += _race.Charisma;
            }

            BaseAttributes = new Attributes();
            AttributeModifiers = new List<Attributes>();
            Buffs = new Managers.BuffManager();

            BaseAttributes.HP = 100;
            BaseAttributes.CurrentHP = 100;

            Currency = new Currency(0, 0, 0);

            // players start with a proficiency bonus of 2
            BaseAttributes.Proficiency = 2;

            // set the base modifiers based on the current ability score
            get_strength_base_modifier();
            get_dexterity_base_modifier();
            get_constitution_base_modifier();
            get_intelligence_base_modifier();
            get_wisdom_base_modifier();
            get_charisma_base_modifier();

            level = 1;
        }

        /// <summary>
        /// Creates a new Adventurer object with a determined set of stats. Uses a predetermined set of animations as its sprite.
        /// </summary>
        public Adventurer(int _id, string _name, Dictionary<string, Utils.Animation> animations, Vector2 pos, Race _race = null, Class _class = null, int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)  :
            base(animations, pos)
        {
            equipped = new List<Equipment>();
            Inventory = new List<InventoryItem>();
            Class = _class;
            Race = _race;

            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Constitution = constitution;
            Charisma = charisma;

            if(_class != null)
            {
                Strength += _class.Strength;
                Dexterity += _class.Dexterity;
                Constitution += _class.Constitution;
                Intelligence += _class.Intelligence;
                Wisdom += _class.Wisdom;
                Charisma += _class.Charisma;
            }
            if (_race != null)
            {
                Strength += _race.Strength;
                Dexterity += _race.Dexterity;
                Constitution += _race.Constitution;
                Intelligence += _race.Intelligence;
                Wisdom += _race.Wisdom;
                Charisma += _race.Charisma;
            }

            BaseAttributes = new Attributes();
            AttributeModifiers = new List<Attributes>();
            Buffs = new Managers.BuffManager();

            Currency = new Currency(0, 0, 0);

            BaseAttributes.HP = 100;
            BaseAttributes.CurrentHP = 100;

            // players start with a proficiency bonus of 2
            BaseAttributes.Proficiency = 2;

            // set the base modifiers based on the current ability score
            get_strength_base_modifier();
            get_dexterity_base_modifier();
            get_constitution_base_modifier();
            get_intelligence_base_modifier();
            get_wisdom_base_modifier();
            get_charisma_base_modifier();

            level = 1;
        }

        /// <summary>
        /// Runs a skill check in Intelligence and determines whether or not to give a boosted experience amount.
        /// </summary>
        /// <param name="exp">The current exp amount the Adventurer will receive</param>
        /// <param name="target">The target score the skill rolls needs to beat</param>
        private ulong GiveExperience(ulong exp, int target)
        {
            if (this.IntelligenceCheck() >= target)
                return exp + (ulong)Math.Round(exp * 0.5, 0, MidpointRounding.AwayFromZero);
            else return exp;
        }

        public void LevelUp(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
        {
            // increase the ability scores
            Strength += strength;
            Dexterity += dexterity;
            Constitution += constitution;
            Intelligence += intelligence;
            Wisdom += wisdom;
            Charisma += charisma;

            // set the base modifiers based on the current ability score
            get_strength_base_modifier();
            get_dexterity_base_modifier();
            get_constitution_base_modifier();
            get_intelligence_base_modifier();
            get_wisdom_base_modifier();
            get_charisma_base_modifier();

            if (level < 255) 
                level++;

            // Increase the Maximum Exp requirement
            max_experience += (ulong)(1000 * level);
        }

        #region QuestFunctions
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
                        Experience += (ulong)(Quest.RewardExperiencePoints);

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
        #endregion
        #region InventroyFunctions
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
            bool added = false;
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Quantity++;
                    added = true;
                    return; // We added the item, and are done, so get out of this function
                }
            }
            if (!added)
            {
                // They didn't have the item, so add it to their inventory, with a quantity of 1
                Inventory.Add(new InventoryItem(itemToAdd, 1));
            }
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
        #endregion
        #region QueryFunctions
        public InventoryItem ItemByName(string name)
        {
            return Inventory.Where(x => x.Details.Name == name).FirstOrDefault();
        }
        public Equipment EquipmentByName(string name)
        {
            return equipped.Where(x => x.Details.Name == name).FirstOrDefault();
        }
        #endregion
    }
}
