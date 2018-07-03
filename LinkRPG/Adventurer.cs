using LinkEngine.Entities;
using LinkEngine.Gameplay.Items;

using System.Collections.Generic;

namespace RPG
{
    /// <summary>
    /// Character is the class that holds all the needed data for the player to use.
    /// </summary>
    public class Adventurer : Player
    {
        System.Random rand = new System.Random();
        short equip_size = 6;

        /// <summary>
        /// EQUIP_SIZE is the max equipment the player can equip.
        /// The order of the slots are [Head, Torso, Legs, Boots, Main Hand, Off Hand]
        /// </summary>
        public short EQUIP_SIZE { get { return equip_size; } set { equip_size = value; } }

        public Equipment[] Equipment { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        /// <summary>
        /// the slug of the player, used for image searching
        /// </summary>
        public string Slug { get; set; }

        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public List<Entity> Party { get; set; }
        public List<Entity> PartyDead { get; set; }

        /// <summary>
        /// The players Equipment list
        /// </summary>

        public List<Ability> Abilities { get; set; }

        /// <summary>
        /// Strength is used to calculate combat damage
        /// </summary>
        public short Strength { get; set; }
        /// <summary>
        /// Perception is used to discover the map more efficiently
        /// </summary>
        public short Perception { get; set; }
        /// <summary>
        /// Endurance is used when calculating blocking damage
        /// </summary>
        public short Endurance { get; set; }
        /// <summary>
        /// Charisma is used to gain dialoge options
        /// </summary>
        public short Charisma { get; set; }
        /// <summary>
        /// Intelligence is used to think up crafting recipes
        /// </summary>
        public short Intelligence { get; set; }
        /// <summary>
        /// Agility is used to calculate blocking ability
        /// </summary>
        public short Agility { get; set; }
        /// <summary>
        /// Luck is used to calculate Critical Hit chance and Blocking ability
        /// </summary>
        public short Luck { get; set; }

        public List<Modifier> StrengthModifiers { get; set; }
        public List<Modifier> PerceptionModifiers { get; set; }
        public List<Modifier> EnduranceModifiers { get; set; }
        public List<Modifier> CharismaModifiers { get; set; }
        public List<Modifier> IntelligenceModifiers { get; set; }
        public List<Modifier> AgilityModifiers { get; set; }
        public List<Modifier> LuckModifiers { get; set; }

        /// <summary>
        /// 
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
        public Adventurer(int _id, string _name, int _hp, int _maxHp, int _level, int _exp, int _maxExp, int _gold, string slug) : 
            base (_id, _name, _hp, _maxHp)
        {
            Level = _level;
            Exp = _exp;
            MaxExp = _maxExp;
            Gold = _gold;
        }

        public new void giveExperience(int exp)
        {
            if (Intelligence > 0)
            {
                Exp += (int)(exp + ((exp * 0.10) * Intelligence));
            }
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

        /// <summary>
        /// PerceptionCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the perception mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// EnduranceCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the endurance mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// CharismaCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the charisma mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// IntelligenceCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the intelligence mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// AgilityCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the agility mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// LuckCheck will take the current value of this.Strength, 
        /// add all given modifiers and check if it is greater, 
        /// less than or equal to the target 'check' value
        /// </summary>
        /// <param name="check">the value to check the luck mod against</param>
        /// <param name="modifiers">the list of values to add to the Ability check</param>
        /// <returns>Returns true if Ability check passes</returns>
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

        /// <summary>
        /// Adds the given mod into a list of skill mods
        /// </summary>
        /// <param name="mod">the mod to add</param>
        /// <param name="mods">the list to add the mod to</param>
        public void AddModifier(Modifier mod, List<Modifier> mods)
        {
            mods.Add(mod);
        }
        /// <summary>
        /// Removes the given mod from the given list of mods
        /// </summary>
        /// <param name="name">Mod to remove by name</param>
        /// <param name="mods">List to remove mod from</param>
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

        /// <summary>
        /// Removes a party member from the active list and places it in the dead list to be revived
        /// </summary>
        /// <param name="partymember">The party member to kill</param>
        public void KillPartyMember(Entity partymember)
        {
            Party.Remove(partymember);
            PartyDead.Add(partymember);
        }

        /// <summary>
        /// Will try to use the selected item. If its equippable it will try to equip it, and if its consumable the playe will consume it
        /// </summary>
        /// <param name="itemToUse"></param>
        public void UseItem(Item itemToUse)
        {
            // use the item
            // check if the item is equipment
            if (itemToUse.Equipable)
            {
                Equipment equ = (Equipment)itemToUse;
                // if the item is already eqipped it should be unequipped
                if (equ.IsEquipped)
                {
                    Unequip(equ);
                    ((Equipment)itemToUse).IsEquipped = false;
                }
                else
                {
                    // check if another item is equipped in that slot
                    if (Equipment[equ.Slot] == null)
                    {
                        Equip(equ);
                        ((Equipment)itemToUse).IsEquipped = true;
                    }
                }
            }

            if (itemToUse.Consumable)
            {
                // remove 1 from the inventory
                RemoveItemFromInventory(itemToUse);
            }
        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and place the item there.
        /// This will add the modifier to the player
        /// </summary>
        /// <param name="equ">The item to equip</param>
        public void Equip(Equipment equ)
        {
            Equipment[equ.Slot] = equ;

            switch (equ.Mod.TargetSkill)
            {
                case "Strength":
                    AddModifier(equ.Mod, StrengthModifiers);
                    break;
                case "Perception":
                    AddModifier(equ.Mod, PerceptionModifiers);
                    break;
                case "Endurance":
                    AddModifier(equ.Mod, EnduranceModifiers);
                    break;
                case "Charisma":
                    AddModifier(equ.Mod, CharismaModifiers);
                    break;
                case "Intelligence":
                    AddModifier(equ.Mod, IntelligenceModifiers);
                    break;
                case "Agility":
                    AddModifier(equ.Mod, AgilityModifiers);
                    break;
                case "Luck":
                    AddModifier(equ.Mod, LuckModifiers);
                    break;
            }

        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and set the slot to null.
        /// This will remove the modifier from the player
        /// </summary>
        /// <param name="equ">The item to unequip</param>
        public void Unequip(Equipment equ)
        {
            Equipment[equ.Slot] = null;

            switch (equ.Mod.TargetSkill)
            {
                case "Strength":
                    RemoveMod(equ.Mod.Name, StrengthModifiers);
                    break;
                case "Perception":
                    RemoveMod(equ.Mod.Name, PerceptionModifiers);
                    break;
                case "Endurance":
                    RemoveMod(equ.Mod.Name, EnduranceModifiers);
                    break;
                case "Charisma":
                    RemoveMod(equ.Mod.Name, CharismaModifiers);
                    break;
                case "Intelligence":
                    RemoveMod(equ.Mod.Name, IntelligenceModifiers);
                    break;
                case "Agility":
                    RemoveMod(equ.Mod.Name, AgilityModifiers);
                    break;
                case "Luck":
                    RemoveMod(equ.Mod.Name, LuckModifiers);
                    break;
            }
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
                            Inventory.Add(new InventoryItem(Quest.RewardItem, 1));
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
                Quests.Add(new PlayerQuest(Quest));
            }
            //World.HUD.Update();
        }

        public Equipment EquipmentByName(string name)
        {
            foreach (Equipment equ in Equipment)
            {
                if (equ.Name == name)
                {
                    return equ;
                }
            }
            return null;
        }
    }
}
