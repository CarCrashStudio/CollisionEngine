using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace RPG_Engine
{ 
    public class Character : Entity
    {
        //const int STEP_SIZE = 8;    

        int level;
        int exp;
        int maxExp;
        int gold;
        Location currentLocation;

        Random rand = new Random();

        public Location CurrentLocation { get { return currentLocation; } set { currentLocation = value; } }
        public int Level { get { return level; } set { level = value; } }
        public int Exp { get { return exp; } set { exp = value; } }
        public int MaxExp { get { return maxExp; } set { maxExp = value; } }
        public int Gold { get { return gold; } set { gold = value; } }

        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        public bool HeadEquipped { get; set; }
        public bool TorsoEquipped { get; set; }
        public bool LegsEquipped { get; set; }
        public bool FeetEquipped { get; set; }
        public bool MainHandEquipped { get; set; }
        public bool OffHandEquipped { get; set; }

        public string Class { get; set; }

        public int CountDown { get; set; }
        public int MAX_COUNTDOWN { get { return 100; } }

        public Character(int _id, string _name, Point _location, int _level, int _exp, int _maxExp, int _gold, Bitmap _img, World _world, PictureBox _charForm) :
            base(_id, _name,_location,_img,_world,_charForm)
        {
            level = _level;
            exp = _exp;
            maxExp = _maxExp;
            gold = _gold;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public Bitmap Draw()
        {
            var bitmap = new Bitmap(world.charForm.Image, world.WIDTH, world.HEIGHT);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(Image, new Point(world.WIDTH / 2, world.HEIGHT / 2));

            return bitmap;
        }

        public void Move(int x, int y)
        {
            Location = new Point(Location.X + x, Location.Y + y);
            switch (Facing)
            {
                case "North":
                    Image = new Bitmap("icons/PlayerStates/PlayerUp.bmp");
                    break;
                case "South":
                    Image = new Bitmap("icons/PlayerStates/PlayerDown.bmp");
                    break;
                case "East":
                    Image = new Bitmap("icons/PlayerStates/PlayerRight.bmp");
                    break;
                case "West":
                    Image = new Bitmap("icons/PlayerStates/PlayerLeft.bmp");
                    break;
            }
            world.charForm.Image = Draw();
        }

        public bool isColliding()
        {
            WorldMap map = world.map;

            //If the X Coordinate exceeds 0 or max map size
            if (CheckNextTile().X == -1 || CheckNextTile().X == (world.MAX_MAP_SIZE + 1))
            {
                return true;
            }

            //If the Y Coordinate exceeds 0 or max map size
            if (CheckNextTile().Y == -1 || CheckNextTile().Y == (world.MAX_MAP_SIZE + 1))
            {
                return true;
            }

            foreach (Tile tile in map.TilesOnMap)
            {
                if (tile.Location == CheckNextTile())
                {
                    if (tile.Dense == 1)
                    {
                        return true;
                    }
                }
            }
            if (CurrentLocation.MonsterLivingHere != null)
            {
                if (CheckNextTile() == CurrentLocation.MonsterLivingHere.Location)
                {
                    world.combat = new Combat(world.Output, this, CurrentLocation.MonsterLivingHere);
                    return true;
                }
            }
            return false;
        }
        void SpawnMonsterLivingHere()
        {
            if (CurrentLocation.MonsterLivingHere != null)
            {
                if (CountDown != 0)
                {
                    CountDown--;
                }
                if (CountDown == 0)
                {
                    int spawn = rand.Next(100) + 1;
                    if (spawn < CurrentLocation.MonsterLivingHere.SpawnChance)
                    {
                        CurrentLocation.MonsterLivingHere.Location = CheckNextTile();
                        world.combat = new Combat(world.Output, this, CurrentLocation.MonsterLivingHere);
                    }
                    CountDown = rand.Next(MAX_COUNTDOWN);
                }
            }
        }
        public void MovePlayer(string keyPressed)
        {
            switch (keyPressed)
            {
                case "Up":
                    Facing = "North";
                    break;
                case "Down":
                    Facing = "South";
                    break;
                case "Left":
                    Facing = "West";
                    break;
                case "Right":
                    Facing = "East";
                    break;
            }
            CheckCurrentLocation();
            if (!isColliding())
            {
                switch (Facing)
                {
                    case "North":
                        Move(0, -1);
                        world.map.ShiftMap(0, 1);
                        break;

                    case "South":
                        Move(0, 1);
                        world.map.ShiftMap(0, -1);
                        break;

                    case "East":
                        Move(1, 0);
                        world.map.ShiftMap(-1, 0);
                        break;

                    case "West":
                        Move(-1, 0);
                        world.map.ShiftMap(1, 0);
                        break;
                }
                SpawnMonsterLivingHere();
            }
        }

        public void LevelUp()
        {
            if (exp >= maxExp)
            {
                level++;
            }
        }

        public int CalculateMaxHealth()
        {
            switch (Class)
            {
                case "Rogue":
                    return 50;
                case "Mage":
                    return 60;
                case "Warrior":
                    return 100;
                case "Scout":
                    return 70;
            }
            return 0;
        }
        public int CalculateMaxMana()
        {
            switch (Class)
            {
                case "Rogue":
                    return 10;
                case "Mage":
                    return 25;
                case "Warrior":
                    return 5;
                case "Scout":
                    return 5;
            }
            return 0;
        }
        public int CalculateMaxDamage()
        {
            switch (Class)
            {
                case "Rogue":
                    return 2;
                case "Mage":
                    return 3;
                case "Warrior":
                    return 5;
                case "Scout":
                    return 2;
            }
            return 0;
        }
        public int CalculateMaxDefense()
        {
            switch (Class)
            {
                case "Rogue":
                    return 5;
                case "Mage":
                    return 3;
                case "Warrior":
                    return 8;
                case "Scout":
                    return 6;
            }
            return 0;
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

                    return; // We added the item, and are done, so get out of this function
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

        void CheckCurrentLocation()
        {
            foreach (Transition transition in CurrentLocation.Transitions)
            {
                if (CheckNextTile() == transition.Location && Facing == transition.RequiredFacingDirection)
                {
                    world.player.Location = transition.NextLocation.Transitions[0].Location;
                    MoveTo(transition.NextLocation);
                }
                //world.HUD.Update();
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
                    world.Output.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                    return;
                }
            }
            CurrentLocation = newLocation;
            try
            {
                CurrentLocation.MonsterLivingHere = newLocation.MonsterLivingHere;
                CurrentLocation.MonsterLivingHere.Location = new Point(11, 11);
            }
            catch { CurrentLocation.MonsterLivingHere = null; }

            CountDown = MAX_COUNTDOWN; //Countdown resets upon entering a new location

            world.map = new WorldMap(CurrentLocation.Name, world.charForm, world);
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
                        world.Output.Text += Environment.NewLine;
                        world.Output.Text += "You complete the '" + npc.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

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
                        world.Output.Text += "You receive: " + Environment.NewLine;
                        world.Output.Text += npc.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                        world.Output.Text += npc.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                        world.Output.Text += npc.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                        world.Output.Text += Environment.NewLine;

                        Exp += npc.QuestAvailableHere.RewardExperiencePoints;
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
                // The player does not already have the quest

                // Display the messages
                world.Output.Text += "You receive the " + npc.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                world.Output.Text += npc.QuestAvailableHere.Description + Environment.NewLine;
                world.Output.Text += "To complete it, return with:" + Environment.NewLine;
                foreach (QuestCompletionItem qci in npc.QuestAvailableHere.QuestCompletionItems)
                {
                    if (qci.Quantity == 1)
                    {
                        world.Output.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        world.Output.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                    }
                }
                world.Output.Text += Environment.NewLine;

                // Add the quest to the player's quest list
                Quests.Add(new PlayerQuest(npc.QuestAvailableHere));
            }
            //world.HUD.Update();
        }
    }
}
