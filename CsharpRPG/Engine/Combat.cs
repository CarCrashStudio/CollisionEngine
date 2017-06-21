using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RPG_Engine
{
    public class Combat
    {
        Random rand = new Random();
        RichTextBox Output;
        Character player;
        public Monster monster;
        public bool Initiated { get; set; }

        public Combat(RichTextBox _output, Character _player, Monster _monster)
        {
            Output = _output;
            player = _player;
            monster = _monster;

            if (!(monster == null)) { if (player.CheckNextTile() == monster.Location) { InitiateCombat(); } }
        }

        void InitiateCombat()
        {
            Initiated = true;
            Output.Text += Environment.NewLine + "Combat initiated";
        }
        public void Attack(Monster defender)
        {
            int damage = Damage(player, defender);
            defender.Health -= damage;
            Output.Text += Environment.NewLine + Environment.NewLine + player.Name + " dealt " + damage + " damage to " + defender.Name + Environment.NewLine;
            if (defender.isDead())
            {
                Output.Text += Environment.NewLine + "You beat " + defender.Name + ". You earned " + defender.RewardExperiencePoints + " exp and " + defender.RewardGold + " gold.";
                player.Exp += defender.RewardExperiencePoints;
                player.Gold += defender.RewardGold;
                // Get random loot items from the monster
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                // Add items to the lootedItems list, comparing a random number to the drop percentage
                foreach (LootItem lootItem in defender.LootTable)
                {
                    if (rand.Next(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                // If no items were randomly selected, then add the default loot item(s).
                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in defender.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                // Add the looted items to the player's inventory
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        Output.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        Output.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }
                player.LevelUp();
                Initiated = false;
                defender.Location = new Point(11, 11);
                //updateScreen();
                defender.Health = defender.MaxHealth;
            }
            else
            {
                damage = Damage(defender, player);
                player.Health -= damage;
                Output.Text += defender.Name + " dealt " + damage + " damage to " + player.Name + Environment.NewLine;

                //updateScreen();
                if (player.isDead()) { Output.Text += Environment.NewLine + Environment.NewLine + "You lose." + Environment.NewLine; Initiated = false; }
            }
        }
        int Damage(Entity attacker, Entity defender)
        {
            int temp = rand.Next(attacker.MaximumDamage);
            temp -= rand.Next(defender.MaximumDefense);
            if (temp < 0)
            {
                temp = 1;
            }
            return temp;
        }
    }
}
