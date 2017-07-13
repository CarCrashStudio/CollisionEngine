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

        private void wait_Tick(object sender, EventArgs e)
        {
            wait.Enabled = false;
            world.combat.Attack(world.player.CurrentLocation.MonsterLivingHere, world.player, world.player.CurrentLocation.MonsterLivingHere.Skills[rand.Next(world.player.CurrentLocation.MonsterLivingHere.Skills.Count)]);
        }
    }
}
