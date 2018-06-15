using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Character : Player
    {
        System.Random rand = new System.Random();

        public Location CurrentLocation { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public int Gold { get; set; }
        public string Slug { get; set; }

        public List<Equipment> Equipped { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public List<Ability> Abilities { get; set; }

        public short Strength { get; set; }
        public short Perception { get; set; }
        public short Endurance { get; set; }
        public short Charisma { get; set; }
        public short Intelligence { get; set; }
        public short Agility { get; set; }
        public short Luck { get; set; }

        public List<Modifier> StrengthModifiers { get; set; }
        public List<Modifier> PerceptionModifiers { get; set; }
        public List<Modifier> EnduranceModifiers { get; set; }
        public List<Modifier> CharismaModifiers { get; set; }
        public List<Modifier> IntelligenceModifiers { get; set; }
        public List<Modifier> AgilityModifiers { get; set; }
        public List<Modifier> LuckModifiers { get; set; }

        /// <summary>
        /// The Player class
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="clss"></param>
        /// <param name="_hp"></param>
        /// <param name="_maxHp"></param>
        /// <param name="_maximumDamage"></param>
        /// <param name="_maxDefense"></param>
        /// <param name="_level"></param>
        /// <param name="_exp"></param>
        /// <param name="_maxExp"></param>
        /// <param name="_gold"></param>
        /// <param name="slug"></param>
        public Character(int _id, string _name, int _hp, int _maxHp, int _level, int _exp, int _maxExp, int _gold, string slug) :
            base(_id, _name, _hp, _maxHp)
        {
            Level = _level;
            Exp = _exp;
            MaxExp = _maxExp;
            Gold = _gold;
            Slug = slug;

            Inventory = new List<InventoryItem>();
            Equipped = new List<Equipment>();
            Quests = new List<PlayerQuest>();

            Party.Add(this);
        }

        public void giveExperience (int exp)
        {
            if (Intelligence > 0)
            {
                Exp += (int)(exp + ((exp * 0.10) * Intelligence));
            }
        }
        public void LevelUp()
        {
            if (Exp >= MaxExp)
            {
                Level++;
                MaxExp += rand.Next(100, MaxExp);
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
        public void RecieveQuest(NPC npc)
        {
            // See if the player already has the quest, and if they've completed it
            bool playerAlreadyHasQuest = false;
            bool playerAlreadyCompletedQuest = false;

            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == npc.QuestAvailableHere.ID)
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

                    foreach (QuestCompletionItem qci in npc.QuestAvailableHere.QuestCompletionItems)
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
                        foreach (QuestCompletionItem qci in npc.QuestAvailableHere.QuestCompletionItems)
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
                        giveExperience(npc.QuestAvailableHere.RewardExperiencePoints);
                        Gold += npc.QuestAvailableHere.RewardGold;

                        // Add the reward item to the player's inventory
                        bool addedItemToPlayerInventory = false;

                        foreach (InventoryItem ii in Inventory)
                        {
                            if (ii.Details.ID == npc.QuestAvailableHere.RewardItem.ID)
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
                            Inventory.Add(new InventoryItem(npc.QuestAvailableHere.RewardItem, 1));
                        }

                        // Mark the quest as completed
                        // Find the quest in the player's quest list
                        foreach (PlayerQuest pq in Quests)
                        {
                            if (pq.Details.ID == npc.QuestAvailableHere.ID)
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
                Quests.Add(new PlayerQuest(npc.QuestAvailableHere));
            }
            //World.HUD.Update();
        }

        public InventoryItem ItemByName(string name)
        {
            foreach(InventoryItem ii in Inventory)
            {
                if(ii.Details.Name == name)
                {
                    return ii;
                }
            }
            return null;
        }
        public Equipment EquipmentByName(string name)
        {
            foreach (Equipment equ in Equipped)
            {
                if (equ.Name == name)
                {
                    return equ;
                }
            }
            return null;
        }

        /// <summary>
        /// StrengthCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the strength mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
        public bool StrengthCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int str = Strength;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    str += modifiers[i].ModifierAmount;
                }
            }


            // if the check is less than check
            if (str < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool PerceptionCheck(short check, Modifier[] modifiers)
        {
            // sets the current perception mod
            int per = Perception;
            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to perception value
                    per += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (per < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool EnduranceCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int end = Endurance;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    end += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (end < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool CharismaCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int cha = Charisma;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    cha += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (cha < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool IntelligenceCheck(short check, Modifier[] modifiers)
        {
            // sets the current perception mod
            int intel = Intelligence;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to perception value
                    intel += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (intel < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool AgilityCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int agi = Agility;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    agi += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (agi < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;

        }
        public bool LuckCheck(short check, Modifier[] modifiers)
        {
            // sets the current strength mod
            int luck = Luck;

            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    // adds modifiers to strength value
                    luck += modifiers[i].ModifierAmount;
                }
            }

            // if the check is less than check
            if (luck < check)
                return false;
            else
                // If the check is equal to or greater than check
                return true;
        }

        public void AddModifier(Modifier mod, List<Modifier> mods)
        {
            mods.Add(mod);
        }
        public void RemoveMod(string name, List<Modifier> mods)
        {
            foreach (Modifier mod in mods)
            {
                if (mod.Name == name)
                {
                    mods.Remove(mod);
                }
            }
        }
    }
}
