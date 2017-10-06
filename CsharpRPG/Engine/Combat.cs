using System;
using System.Collections.Generic;
using System.Drawing;
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

            try
            {
                partyMember1 = (Character)player.Party[1];
            }
            catch { partyMember1 = null; }
            try
            {
                partyMember2 = (Character)player.Party[2];
            }
            catch { partyMember2 = null; }
            try
            {
                partyMember3 = (Character)player.Party[3];
            }
            catch { partyMember3 = null; }
            
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

            try
            {
                partyMember1 = (Character)player.Party[1];
            }
            catch { partyMember1 = null; }
            try
            {
                partyMember2 = (Character)player.Party[2];
            }
            catch { partyMember2 = null; }
            try
            {
                partyMember3 = (Character)player.Party[3];
            }
            catch { partyMember3 = null; }

            if (!(monster == null)) { InitiateCombat(); }
        }

        void InitiateCombat()
        {
            player.Facing = "South"; // Set the player so you see the front facing image
            player.SwitchFacing("Player");
            
            combat.pbCurrentParty.Image = player.Image;
            combat.pbCurrentMonster.Image = monster.Image;

            Initiated = true;
            world.HudForm.Visible = false;
            combat.Show();
            Output.Text += Environment.NewLine + "Combat initiated";
        }
        
        public void SelectSkills(string PlayerSkill, string PartySkill1 = null, string PartySkill2 = null, string PartySkill3 = null)
        {
            Skill skll = new Skill();
            foreach (Skill skill in world.player.Skills)
            {
                if(skill.Name == PlayerSkill)
                {
                    skll = skill;
                    break;
                }
            }
            Attack(player, monster.Party[0], skll);
            if(partyMember1 != null)
            {
                foreach (Skill skill in partyMember1.Skills)
                {
                    if (skill.Name == PartySkill1)
                    {
                        skll = skill;
                    }
                }
                Attack(partyMember1, monster.Party[0], skll);
            }
            if (partyMember2 != null)
            {
                foreach (Skill skill in partyMember2.Skills)
                {
                    if (skill.Name == PartySkill2)
                    {
                        skll = skill;
                    }
                }
                Attack(partyMember2, monster.Party[0], skll);
            }
            if (partyMember3 != null)
            {
                foreach (Skill skill in partyMember3.Skills)
                {
                    if (skill.Name == PartySkill3)
                    {
                        skll = skill;
                    }
                }
                Attack(partyMember3, monster.Party[0], skll);
            }

            if (Initiated)
            {
                wait.Enabled = true;
            }
        }
        public void Attack(Entity Attacker, Entity Defender, Skill skill)
        {
            int oldHealth = Defender.Health;
            skill.Use(Attacker, Defender);
            int newHealth = Defender.Health;

            Output.Text += Attacker.Name + " attacked " + Defender.Name + " for " + (oldHealth - newHealth).ToString() + " damage!" + Environment.NewLine;
            CheckDeath(Defender);
        }

        public void UseItem(Character target, InventoryItem Item)
        {
            Potion pot = (Potion)Item.Details;
            pot.Consume(Item, target);
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
                    monster.KillPartyMember(entity);

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
                    combat.Hide();
                    MessageBox.Show("You Lose!");
                }
            }
            if (entity == partyMember1 || entity == partyMember2 || entity == partyMember3)
            {
                if (entity.isDead())
                {
                    player.KillPartyMember(entity);
                }
            }
        }
    }
}
