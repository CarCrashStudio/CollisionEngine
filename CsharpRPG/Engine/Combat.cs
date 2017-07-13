using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Combat
    {
        World world;
        Random rand = new Random();

        public CombatForm combat;
        public Timer wait;
        public RichTextBox Output;
        public Panel CombatForm;
        
        Character player;
        Character partyMember1;
        Character partyMember2;
        Character partyMember3;

        public Monster monster;

        public bool Initiated { get; set; }

        public Combat(CombatForm _combat, Monster _monster, World _world, Character _player)
        {
            combat = _combat;
            Output = _combat.lblCombatOutput;
            player = _player;
            monster = _monster;
            world = _world;

            wait = _combat.wait;

            if (!(monster == null)) { InitiateCombat(); }
        }
        public Combat(Combat combat, Monster _monster)
        {
            this.combat = combat.combat;
            Output = combat.Output;
            CombatForm = combat.CombatForm;
            player = combat.player;
            monster = _monster;
            world = combat.world;

            wait = combat.wait;

            if (!(monster == null)) { InitiateCombat(); }
        }

        void InitiateCombat()
        {
            player.Facing = "South"; // Set the player so you see the front facing image
            player.Move(0, 0, "Player");
            
            combat.pbCurrentParty.Image = player.Image;
            //combat.pbCurrentMonster.Image = monster.Image;

            Initiated = true;
            world.HudForm.Visible = false;
            combat.Visible = true;
            Output.Text += Environment.NewLine + "Combat initiated";
        }
        
        public void Attack(Entity Attacker, Entity Defender, Skill skill)
        {
            int oldHealth = Defender.Health;
            skill.Use(Attacker, Defender);
            int newHealth = Defender.Health;

            Output.Text += Attacker.Name + " attacked " + Defender.Name + " for " + (oldHealth - newHealth).ToString() + " damage!" + Environment.NewLine;
            CheckDeath(Defender);
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
        void CheckDeath(Entity entity)
        {
            if(entity == monster)
            {
                if (entity.isDead())
                {
                    Output.Text += Environment.NewLine + "You beat " + entity.Name + ". You earned " + monster.RewardExperiencePoints + " exp and " + monster.RewardGold + " gold.";
                    player.Exp += monster.RewardExperiencePoints;
                    player.Gold += monster.RewardGold;

                    RewardLoot();

                    player.LevelUp();
                    Initiated = false;
                    world.HudForm.Visible = true;
                    combat.Visible = false;
                    monster.Location = new Point(11, 11);
                    monster.Health = monster.MaxHealth;
                }
            }
            if(entity == player)
            {
                if (entity.isDead())
                {
                    Initiated = false;
                    combat.Visible = false;
                }
            }
            if (entity == partyMember1 || entity == partyMember2 || entity == partyMember3)
            {
                if (entity.isDead())
                {

                }
            }
        }
    }
}
