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

        public string skill1 = "", skill2 = "", skill3 = "", skill4 = "";
        public bool Party1IsAttacking = false, Party2IsAttacking = false, Party3IsAttacking = false, Party4IsAttacking = false;

       

        int currentPartyMember = 0;

        private void wait_Tick(object sender, EventArgs e)
        {
            wait.Enabled = false;

        }

        private void lstSkills_DoubleClick(object sender, EventArgs e)
        {
            switch (currentPartyMember)
            {
                case 0:
                    skill1 = lstSkills.SelectedItem.ToString();
                    Party1IsAttacking = true;
                    break;
                case 1:
                    skill2 = lstSkills.SelectedItem.ToString();
                    Party2IsAttacking = true;
                    break;
                case 2:
                    skill3 = lstSkills.SelectedItem.ToString();
                    Party3IsAttacking = true;
                    break;
                case 3:
                    skill4 = lstSkills.SelectedItem.ToString();
                    Party4IsAttacking = true;
                    break;
            }
            
        }

        private void btnATK_Click(object sender, EventArgs e)
        {
            lstSkills.Items.Clear();
            
            foreach (Skill skill in world.player.Party[currentPartyMember].Skills)
            {
                lstSkills.Items.Add(skill.Name);
            }
            lstSkills.Visible = true;
        }

        private void lstParty3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnITEM_Click(object sender, EventArgs e)
        {
            lstItems.Items.Clear();
            foreach(InventoryItem ii in world.player.Inventory)
            {
                lstItems.Items.Add(ii.Details.Name);
            }
            lstItems.Visible = true;
        }
        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryItem ii = world.player.ItemByName(lstItems.SelectedItem.ToString());
            switch (currentPartyMember)
            {
                case 0:
                    ii.Details.Consume(ii, world.player.Party[currentPartyMember]);
                    Party1IsAttacking = false;
                    break;
                case 1:
                    ii.Details.Consume(ii, world.player.Party[currentPartyMember]);
                    Party2IsAttacking = false;
                    break;
                case 2:
                    ii.Details.Consume(ii, world.player.Party[currentPartyMember]);
                    Party3IsAttacking = false;
                    break;
                case 3:
                    ii.Details.Consume(ii, world.player.Party[currentPartyMember]);
                    Party4IsAttacking = false;
                    break;
            }
        }
    }
}
