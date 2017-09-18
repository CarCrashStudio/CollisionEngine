using CsharpRPG.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG
{
    public partial class MainForm : Form
    {
        // Engine Object Classes
        public World world;
        public Online Online;
        public Local Local;
        Sql SQL;

        // Forms
        CharacterForm charSheet;
        InventoryForm inventory;
        CombatForm combat;

        // Object Classes
        Random rand = new Random();
        
        // Variables
        int offset;

        // Constants
        const string sqlID = "treyhall";
        const string sqlPass = "web.56066";
        const string sqlConnString =
            "Server=tcp:roguedatabase.database.windows.net,1433;" +
            "Initial Catalog=rogueDB;" +
            "Persist Security Info=False;" +
            "User ID={0};" +
            "Password={1};" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;";

        public MainForm()
        {
            InitializeComponent();
            Bounds = Screen.PrimaryScreen.Bounds;
            SetupGame();
        }
        void SetupGame()
        {
            inventory = new InventoryForm();
            charSheet = new CharacterForm(this);
            offset = -(pbMap.Location.Y);
            SQL = new Sql(String.Format(sqlConnString, sqlID, sqlPass));
            Login();
            charSheet.world = world;
            InitializeScreenControls();
            world.combat = new Combat(combat = new CombatForm(world), world.MonsterByLocation(world.player.NextTile), world, world.player);
            world.HUD.UpdateScreenSize();
            updateScreen();
            updateScreen();
        }

        #region OnlineMultiplayer
        void Login()
        {
            LoginForm login = new LoginForm(SQL, this);
            login.ShowDialog();
        }
        #endregion
        public int CalculateMaxHealth(string Class)
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
        public int CalculateMaxMana(string Class)
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
        public int CalculateMaxDamage(string Class)
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
        public int CalculateMaxDefense(string Class)
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

        void updateScreen()
        {
            world.HUD.Update();
            label1.Text = world.player.CurrentLocation.Name + " " + world.player.CountDown;
        }

        public void InitializePlayer(string name, string clss, Point loc, int health, int maxhealth, int mana, int maxmana, int str, int def, int level, int exp, int maxexp, int gold, string slug, int lastlocid)
        {
            world = new World(pbMap, new Bitmap(Properties.Resources.CharStatBar), new Bitmap(Properties.Resources.CharImgBox), new Bitmap(Properties.Resources.strength), new Bitmap(Properties.Resources.defense), new Character(1, name, clss, loc, health, maxhealth, mana, maxmana, str, def, level, exp, maxexp, gold, slug, new Bitmap(Properties.Resources.PlayerDown)), offset); //You, the player, Character creation will be implemented later
            world.player.MoveTo(world.LocationByID(lastlocid));
            //world.player.Skills.Add(new Skill(world.SkillByID(world.SKILL_ID_WEAKNESS)));
            //world.player.Inventory.Add(new InventoryItem(world.ItemByID(world.WEAPON_ID_RUSTY_SWORD), 1)); //Give 1 'Rusty Sword' to player
        }
       
        void InitializeScreenControls()
        {
            //world.Journal = charSheet.dgvQuests;
        }
        public void UpdateEquipment(Equipment equ)
        {
            world.player.Equipped.Add(equ);
            switch (equ.Slot)
            {
                case (int)Character.Slot.Head:
                    world.player.Head = equ;
                    charSheet.Slots[1].Name = equ.Name;
                    break;
                case (int)Character.Slot.Torso:
                    world.player.Torso = equ;
                    charSheet.Slots[2].Name = equ.Name;
                    break;
                case (int)Character.Slot.Legs:
                    world.player.Legs = equ;
                    charSheet.Slots[3].Name = equ.Name;
                    break;
                case (int)Character.Slot.Feet:
                    world.player.Feet = equ;
                    charSheet.Slots[4].Name = equ.Name;
                    break;
                case (int)Character.Slot.MainHand:
                    world.player.MainHand = equ;
                    charSheet.Slots[7].Name = equ.Name;
                    break;
                case (int)Character.Slot.OffHand:
                    world.player.OffHand = equ;
                    charSheet.Slots[6].Name = equ.Name;
                    break;
            }
            
        }

        void OpenBag()
        {
            UpdateBag();
            inventory.Show();        
        }
        void OpenStats()
        {
            UpdateStats();
            charSheet.Show();
        }

        public void UpdateBag()
        {
            int padding = 10;
            int i = 0;
            inventory.pnlSlotPanel.Controls.Clear();
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    PictureBox temp = new PictureBox();
                    Label lbl = new Label();

                    temp.Size = new Size(48, 48);
                    temp.Location = new Point((x * temp.Size.Width) + (padding * (x + 1)), (y * temp.Size.Width) + (padding * (y + 1)));
                    temp.BackgroundImage = Properties.Resources.CharImgBox;
                    temp.BackgroundImageLayout = ImageLayout.Stretch;
                    temp.DoubleClick += delegate
                    {
                        if (temp.Name != "")
                        {
                            InventoryItem ii = world.player.ItemByName(temp.Name);
                            if (ii.Details.Equipable)
                            {
                                world.player.Inventory.Remove(ii);
                                Equipment equ = (Equipment)ii.Details;
                                temp.Name = "";
                                temp.Image = null;
                                temp.Controls[0].Text = "0";
                                temp.Controls[0].Visible = false;
                                UpdateEquipment(equ);
                                UpdateStats();
                            }
                        }
                    };
                    temp.Visible = true;

                    lbl.Text = "0";
                    lbl.Size = new Size(13, 13);
                    lbl.AutoSize = true;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Location = new Point(temp.Width - lbl.Width, temp.Height - lbl.Height);
                    lbl.Visible = false;

                    try
                    {
                        temp.Image = world.player.Inventory[i].Details.Draw(48, 48, new Point((48 / 2) - (world.player.Inventory[i].Details.Image.Width / 2), (48 / 2) - (world.player.Inventory[i].Details.Image.Height / 2)));
                        temp.Name = world.player.Inventory[i].Details.Name;
                        lbl.Text = world.player.Inventory[i].Quantity.ToString();
                        if (world.player.Inventory[i].Quantity > 1)
                            lbl.Visible = true;
                    }
                    catch { }

                    temp.Controls.Add(lbl);
                    inventory.pnlSlotPanel.Controls.Add(temp);
                    i++;
                }
            }
        }
        public void UpdateStats()
        {
            
            charSheet.lblName.Text = "Name: " + world.player.Name;
            charSheet.lblClassLevel.Text = "Class (Level): " + world.player.Class + " (" + world.player.Level + ")";
            charSheet.lblStr.Text = "Strength: " + world.player.Strength.ToString();
            charSheet.lblDefense.Text = "Defense: " + world.player.Defense.ToString();

            try
            {
                charSheet.pbHead.Image = world.player.Head.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbHead.Width / 2) - world.player.Head.Image.Width / 2, (charSheet.pbHead.Height / 2) - world.player.Head.Image.Height / 2));

            }
            catch { }
            try
            {
                charSheet.pbTorso.Image = world.player.Torso.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbTorso.Width / 2) - world.player.Torso.Image.Width / 2, (charSheet.pbTorso.Height / 2) - world.player.Torso.Image.Height / 2));

            }
            catch { }
            try
            {
                charSheet.pbHead.Image = world.player.Legs.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbLegs.Width / 2) - world.player.Legs.Image.Width / 2, (charSheet.pbLegs.Height / 2) - world.player.Legs.Image.Height / 2));

            }
            catch { }
            try
            {
                charSheet.pbBoots.Image = world.player.Feet.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbBoots.Width / 2) - world.player.Feet.Image.Width / 2, (charSheet.pbBoots.Height / 2) - world.player.Feet.Image.Height / 2));

            }
            catch { }
            try
            {
                charSheet.pbRightHand.Image = world.player.MainHand.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbRightHand.Width / 2) - world.player.MainHand.Image.Width / 2, (charSheet.pbRightHand.Height / 2) - world.player.MainHand.Image.Height / 2));

            }
            catch { }
            try
            {
                charSheet.pbLeftHand.Image = world.player.OffHand.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbLeftHand.Width / 2) - world.player.OffHand.Image.Width / 2, (charSheet.pbLeftHand.Height / 2) - world.player.OffHand.Image.Height / 2));

            }
            catch { }
            charSheet.pbCharImg.Image = world.player.Draw(charSheet.pbCharImg.Width, charSheet.pbCharImg.Height, new Point((charSheet.pbCharImg.Width / 2) - world.player.Image.Width / 2, (charSheet.pbCharImg.Height / 2) - world.player.Image.Height / 2));

        }
        
        #region EventHandlers
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyPressed = e.KeyCode.ToString();
            if (keyPressed == "W" || keyPressed == "S" || keyPressed == "A" || keyPressed == "D")
            {
                if (!world.combat.Initiated)
                {              
                    switch (keyPressed)
                    {
                        case "W":
                            walkW.Enabled = true;
                            break;

                        case "S":
                            walkS.Enabled = true;
                            break;

                        case "A":
                            walkA.Enabled = true;
                            break;

                        case "D":
                            walkD.Enabled = true;
                            break;
                    }
                }
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            string keyPressed = e.KeyCode.ToString();
            switch (keyPressed)
            {
                case "W":
                    walkW.Enabled = false;
                    break;

                case "S":
                    walkS.Enabled = false;
                    break;

                case "A":
                    walkA.Enabled = false;
                    break;

                case "D":
                    walkD.Enabled = false;
                    break;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show(Session.GetType().Name);
            if(Online != null)
            {
                Online.SaveGame();
            }
        }
        private void pbMap_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (HUDObject button in world.Clickables)
            {
                if (button.IsInBounds(e))
                {
                    switch (button.Name)
                    {
                        case "Close":
                            if (MessageBox.Show("Are you sure you want to quit?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Close();
                            }
                            break;
                        case "Bag":
                            OpenBag();
                            break;
                        case "Stats":
                            OpenStats();
                            break;
                    }
                }
            }
        }
        private void walkW_Tick(object sender, EventArgs e)
        {
            walkW.Enabled = false;
            world.player.MovePlayer("W");
            updateScreen();
        }
        private void walkA_Tick(object sender, EventArgs e)
        {
            walkA.Enabled = false;
            world.player.MovePlayer("A");
            updateScreen();
        }
        private void walkS_Tick(object sender, EventArgs e)
        {
            walkS.Enabled = false;
            world.player.MovePlayer("S");
            updateScreen();
        }
        private void walkD_Tick(object sender, EventArgs e)
        {
            walkD.Enabled = false;
            world.player.MovePlayer("D");
            updateScreen();
        }
        #endregion
    }
}
