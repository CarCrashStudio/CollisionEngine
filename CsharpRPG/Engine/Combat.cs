using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RPG_Engine
{
    public class Combat
    {
        Random rand = new Random();

        public Timer wait;
        public RichTextBox Output;
        public Panel CombatForm;
        public PictureBox PHealth;
        public PictureBox DHealth;
        public PictureBox PImg;
        public PictureBox DImg;

        World world;
        Character player;
        public Monster monster;
        public bool Initiated { get; set; }

        public Combat(RichTextBox _output, Panel _CombatForm, Character _player, PictureBox _PHealth, PictureBox _PImg, Monster _monster, PictureBox _DHealth, PictureBox _DImg, World _world, Timer _wait)
        {
            Output = _output;
            CombatForm = _CombatForm;
            player = _player;
            monster = _monster;
            world = _world;

            PHealth = _PHealth;
            PImg = _PImg;
            DHealth = _DHealth;
            DImg = _DImg;

            wait = _wait;

            if (!(monster == null)) { InitiateCombat(); }
        }

        void InitiateCombat()
        {
            player.Facing = "South"; // Set the player so you see the front facing image
            player.Draw(); // Draw the front facing image

            PImg.Image = player.Image;
            DImg.Image = monster.Image;

            Initiated = true;
            world.charForm.Visible = false;
            CombatForm.Visible = true;
            Output.Text += Environment.NewLine + "Combat initiated";
        }
        public void DefenderAttack()
        {
            int damage = Damage(monster, player);
            player.Health -= damage;
            Output.Text += monster.Name + " dealt " + damage + " damage to " + player.Name + Environment.NewLine;
            world.HUD.Update();
            //updateScreen();
            if (player.isDead()) { Output.Text += Environment.NewLine + Environment.NewLine + "You lose." + Environment.NewLine; Initiated = false; }
        }
        public void PlayerAttack()
        {
            int damage = Damage(player, monster);
            monster.Health -= damage;
            world.HUD.Update();
            Output.Text += Environment.NewLine + Environment.NewLine + player.Name + " dealt " + damage + " damage to " + monster.Name + Environment.NewLine;
            CheckDeath();
        }
        void RewardLoot()
        {
            // Get random loot items from the monster
            List<InventoryItem> lootedItems = new List<InventoryItem>();

            // Add items to the lootedItems list, comparing a random number to the drop percentage
            foreach (LootItem lootItem in monster.LootTable)
            {
                if (rand.Next(1, 100) <= lootItem.DropPercentage)
                {
                    lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                }
            }

            // If no items were randomly selected, then add the default loot item(s).
            if (lootedItems.Count == 0)
            {
                foreach (LootItem lootItem in monster.LootTable)
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
        }
        void CheckDeath()
        {
            if (monster.isDead())
            {
                Output.Text += Environment.NewLine + "You beat " + monster.Name + ". You earned " + monster.RewardExperiencePoints + " exp and " + monster.RewardGold + " gold.";
                player.Exp += monster.RewardExperiencePoints;
                player.Gold += monster.RewardGold;

                RewardLoot();

                player.LevelUp();
                Initiated = false;
                world.charForm.Visible = true;
                CombatForm.Visible = false;
                //monster.Location = new Point(11, 11);
                monster.Health = monster.MaxHealth;
            }
            else
            {
                wait.Enabled = true;
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
