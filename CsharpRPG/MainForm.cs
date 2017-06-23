using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Engine;

namespace CsharpRPG
{
    public partial class MainForm : Form
    {
        // Engine Object Classes
        Character player;
        World world;
        Combat combat;

        // Forms
        Journal journal = new Journal();
        CreatorWindow creator;

        // Object Classes
        Random rand = new Random();

        public MainForm()
        {
            InitializeComponent();
            SetupGame();
        }
        void SetupGame()
        {
            world = new World(pbMap); //Create the World Object that holds all needed objects

            InitializeScreenControls();

            creator = new CreatorWindow();
            creator.ShowDialog();

            InitializePlayer();

            creator.Close();

            combat = new Combat(lblCombatOutput, panCombat, player, pbPHealth, pbPlayer, player.CurrentLocation.MonsterLivingHere, pbDHealth, pbDefender, world, wait);
            world.combat = combat;

            updateScreen();
        }

        void updateScreen()
        {
            world.HUD.Update();
            ScrollToBottomOfMessages();
        }

        //void UseItem()
        //{
        //    string item = string.Empty;
        //    int x;
        //    if (!(lstInventory.SelectedIndex == -1))
        //    {
        //        x = 0;
        //        item = "";
        //        while (lstInventory.SelectedItem.ToString()[x] != '(')
        //        {
        //            item += lstInventory.SelectedItem.ToString()[x];
        //            x++;
        //        }
        //    }

        //    try
        //    {
        //        if (!(item.Contains("Potion")))
        //        {
        //            try
        //            {
        //                Weapon weapon = world.WeaponByName(item);
        //                if (weapon.Equipped == true)
        //                {
        //                    player.MaximumDamage -= weapon.MaximumDamage;
        //                    weapon.Equipped = false;
        //                    if (weapon.OffHand == true)
        //                    {
        //                        player.OffHandEquipped = false;
        //                    }
        //                    if (weapon.MainHand == true)
        //                    {
        //                        player.MainHandEquipped = false;
        //                    }
        //                    weapon.EquipTag = "";
        //                    updateScreen();
        //                }
        //                else
        //                {
        //                    if (weapon.MainHand == true)
        //                    {
        //                        if (player.MainHandEquipped == false)
        //                        {
        //                            player.MaximumDamage += weapon.MaximumDamage;
        //                            weapon.Equipped = true;
        //                            player.MainHandEquipped = true;
        //                            weapon.EquipTag = "- Equipped";
        //                            updateScreen();
        //                        }
        //                        else {
        //                            //lblOutput.Text += "Main Hand is full!" + Environment.NewLine + Environment.NewLine;
        //                        }
        //                    }
        //                    if (weapon.OffHand == true)
        //                    {
        //                        if (player.OffHandEquipped == false)
        //                        {
        //                            player.MaximumDamage += weapon.MaximumDamage;
        //                            weapon.Equipped = true;
        //                            player.OffHandEquipped = true;
        //                            weapon.EquipTag = "- Equipped";
        //                            updateScreen();
        //                        }
        //                        else { 
        //                            //lblOutput.Text += "Off Hand is full!" + Environment.NewLine + Environment.NewLine; 
        //                        }
        //                    }
        //                }
        //            }
        //            catch { }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                Potion potion = world.PotionByName(item);
        //                if (item.Contains("Health"))
        //                {
        //                    player.Health += potion.AmountToBuff;
        //                    if (player.Health > player.MaxHealth)
        //                    {
        //                        player.Health = player.MaxHealth;
        //                    }
        //                    player.RemoveItemFromInventory(potion);
        //                }
        //                if (item.Contains("Mana"))
        //                {
        //                    player.Mana += potion.AmountToBuff;
        //                    if (player.Mana > player.MaxMana)
        //                    {
        //                        player.Mana = player.Mana;
        //                    }
        //                    player.RemoveItemFromInventory(potion);
        //                }
        //                updateScreen();
        //            }
        //            catch { }
        //        }
        //    }
        //    catch { 
        //        //lblOutput.Text += item + " cannot be used or equipped!"; 
        //    }
        //}
        void ScrollToBottomOfMessages()
        {
            //lblOutput.SelectionStart = lblOutput.Text.Length;
            //lblOutput.ScrollToCaret();
        }

        void InitializePlayer()
        {
            player = new Character(1, creator.txtName.Text, creator.cmbClass.SelectedItem.ToString(),  new Point(0, 9), 1, 0, 100, 10, new Bitmap("icons/PlayerStates/PlayerDown.bmp"), world, pbMap); //You, the player, Character creation will be implemented later
            world.player = player;
            player.MoveTo(world.LocationByID(world.LOCATION_ID_HOUSE));
            InitializeHUD();
        }
        void InitializeHUD()
        {
            world.HUD = new Hud(new Bitmap("icons/HUDBars/CharStatBar.png"), new Bitmap("icons/HUDBars/CharImgBox.png"), new Bitmap("icons/HUDBars/strength.png"), new Bitmap("icons/HUDBars/defense.png"), world);
        }
        void InitializeScreenControls()
        {
            //world.Stats = lblStats;
            //world.Output = lblOutput;
            //world.Inventory = lstInventory;
            world.Journal = journal.dgvQuests;
        }
        
        #region EventHandlers
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyPressed = e.KeyCode.ToString();
            if (keyPressed == "Up" || keyPressed == "Down" || keyPressed == "Left" || keyPressed == "Right")
            {
                if (!world.combat.Initiated)
                {
                    player.MovePlayer(keyPressed);
                    updateScreen();
                }
            }
            else
            {
                //switch (keyPressed)
                //{
                //    case "Enter":
                //        if (txtInput.Text != string.Empty)
                //        {
                //            lblOutput.Text += player.Name + ": " + txtInput.Text + Environment.NewLine; txtInput.Clear();
                //        }
                //        else
                //        {
                //            if (npc.Location == player.CheckNextTile())
                //            {
                //                if (npc.QuestAvailableHere != null)
                //                {
                //                    player.RecieveQuest(npc);
                //                }
                //            }
                //        }
                //        break;
                //}
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString() + " was pressed."); //DEBUG PURPOSES
        }
        private void lblJournal_Click(object sender, EventArgs e)
        {
            journal.Show();
        }
        private void lblNotes_Click(object sender, EventArgs e)
        {
            //notebook = new Notebook();

            //notebook.lblTitle.Text = player.Name + "'s Notebook";
            //notebook.lstNotes.Items.Add("This is a note");

            //notebook.Show();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            
        }
        private void pbMap_Click(object sender, EventArgs e)
        {
            
            if (world.combat.Initiated == true)
            {
                //world.combat.Attack();
                //HUD.Update();
            }
        }
        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void lstInventory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lstInventory_DoubleClick(object sender, EventArgs e)
        {

        }
        private void btnUse_Click(object sender, EventArgs e)
        {
            //UseItem();
            //lstInventory.SelectedIndex = -1;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            world.Save();
        }
        private void pbComb_Click(object sender, EventArgs e)
        {

        }
        private void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            //Point tempPoint = HUD.GetCursorPos(e);
            //lblOutput.Text += tempPoint.ToString();
            //lblOutput.Text += Environment.NewLine;
            //ScrollToBottomOfMessages();
        }
        #endregion

        private void pbMap_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (HUDObject button in world.HUD.Clickables)
            {
                if (e.Location.X > button.Boundries[0].X && e.Location.X < button.Boundries[1].X)
                {
                    if (e.Location.Y > button.Boundries[0].Y && e.Location.Y < button.Boundries[1].Y)
                    {
                        switch (button.Name)
                        {
                            case "Close":
                                if (MessageBox.Show("Are you sure you want to quit?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Close();
                                }
                                break;
                        }
                    }
                }
            }           
        }

        private void btnATK_Click(object sender, EventArgs e)
        {
            world.combat.PlayerAttack();
            wait.Enabled = true;
        }

        private void wait_Tick(object sender, EventArgs e)
        {
            wait.Enabled = false;
            world.combat.DefenderAttack();
        }
    }
}
