using CsharpRPG.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpRPG
{
    public partial class CombatForm : Form
    {
        Random rand = new Random();
        World world;
        public CombatForm(World world)
        {
            InitializeComponent();
            this.world = world;
        }

        public string skill1;
        public string skill2;
        public string skill3;
        public string skill4;

        private void wait_Tick(object sender, EventArgs e)
        {
            wait.Enabled = false;
            world.combat.Attack(world.combat.monster.Party[0], world.player.Party[0], world.combat.monster.Party[0].Skills[rand.Next(world.combat.monster.Party[0].Skills.Count)]);
            if (world.combat.monster.Party.Count > 1) { world.combat.Attack(world.combat.monster.Party[1], world.player.Party[0], world.combat.monster.Party[1].Skills[rand.Next(world.combat.monster.Party[1].Skills.Count)]); }
            if (world.combat.monster.Party.Count > 2) { world.combat.Attack(world.combat.monster.Party[2], world.player.Party[0], world.combat.monster.Party[2].Skills[rand.Next(world.combat.monster.Party[2].Skills.Count)]); }
            if (world.combat.monster.Party.Count > 3) { world.combat.Attack(world.combat.monster.Party[3], world.player.Party[0], world.combat.monster.Party[3].Skills[rand.Next(world.combat.monster.Party[3].Skills.Count)]); }
        }

        private void lstSkills_DoubleClick(object sender, EventArgs e)
        {
            skill1 = lstSkills.SelectedItem.ToString();
            if (world.player.Party.Count > 1)
            {
                lstParty1.Items.Clear();
                foreach (Skill skill in world.player.Party[1].Skills)
                {
                    lstParty1.Items.Add(skill.Name);
                }
                lstParty1.Visible = true;
                lstSkills.Visible = false;
            }
            else { world.combat.SelectSkills(skill1); }
        }
        private void lstParty1_DoubleClick(object sender, EventArgs e)
        {
            skill2 = lstParty1.SelectedItem.ToString();
            if (world.player.Party.Count > 2)
            {
                lstParty2.Items.Clear();
                foreach (Skill skill in world.player.Party[2].Skills)
                {
                    lstParty2.Items.Add(skill.Name);
                }
                lstParty2.Visible = true;
                lstParty1.Visible = false;
            }
            else { world.combat.SelectSkills(skill1, skill2); }
        }
        private void lstParty2_DoubleClick(object sender, EventArgs e)
        {
            skill3 = lstParty2.SelectedItem.ToString();
            if (world.player.Party.Count > 3)
            {
                lstParty3.Items.Clear();
                foreach (Skill skill in world.player.Party[3].Skills)
                {
                    lstParty3.Items.Add(skill.Name);
                }
                lstParty3.Visible = true;
                lstParty2.Visible = false;
            }
            else { world.combat.SelectSkills(skill1, skill2, skill3); }
        }
        private void lstParty3_DoubleClick(object sender, EventArgs e)
        {
            skill4 = lstSkills.SelectedItem.ToString();
            world.combat.SelectSkills(skill1, skill2, skill3, skill4);
        }

        private void btnATK_Click(object sender, EventArgs e)
        {
            lstSkills.Items.Clear();
            foreach (Skill skill in world.player.Skills)
            {
                lstSkills.Items.Add(skill.Name);
            }
            lstSkills.Visible = true;
        }

        private void lstParty3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
