using CsharpRPG.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using _2D_Graphics_Engine.Engine;

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
        public CharacterForm charSheet;
        public InventoryForm inventory;
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
            world = new World(charSheet.dgvQuests, charSheet.rtbOutput, inventory, charSheet, pbMap, new Bitmap(Properties.Resources.CharStatBar), new Bitmap(Properties.Resources.CharImgBox), new Bitmap(Properties.Resources.strength), new Bitmap(Properties.Resources.defense), new Character(1, name, clss, loc, health, maxhealth, mana, maxmana, str, def, level, exp, maxexp, gold, slug, new Bitmap(Properties.Resources.PlayerDown)), offset); //You, the player, Character creation will be implemented later
            world.player.MoveTo(world.LocationByID(lastlocid));
        }
        
        void OpenBag()
        {
            world.HUD.UpdateBag(world.inventory);
            inventory.Show();        
        }
        void OpenStats()
        {
            world.HUD.UpdateCharSheet(charSheet);
            charSheet.Show();
        }

        #region EventHandlers
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyPressed = e.KeyCode.ToString();
            // MessageBox.Show(keyPressed);
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
            if(keyPressed == "Return")
            {
                foreach(Tile tile in world.map.DecosOnMap)
                {
                    if(tile.Location == world.player.CheckNextTile())
                        if(tile.ID == world.TILE_ID_WORKBENCH)
                        {
                            CraftingForm cf = new CraftingForm(world);
                            cf.Show();
                        }
                            
                    
                }
                foreach(NPC npc in world.HUD.NpcsHere)
                {
                    // check if the player is facing and standing next to an npc
                    if(npc.Location == world.player.CheckNextTile())
                    {
                        // Check if its a shop keeper or questgiver
                        if(npc.QuestAvailableHere != null)
                        {
                            //ScreenText text = new ScreenText(0, npc.Name, npc.QuestAvailableHere.Description, Properties.Resources.CharStatBar, this);
                            //text.Draw();
                            MessageBox.Show("I have a Quest");
                            world.player.RecieveQuest(npc);
                        }
                        if(npc.ShopAvailibleHere != null)
                        {
                            MessageBox.Show("I run a shop");
                            npc.ShopAvailibleHere.Open();
                        }
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
            if(Local != null)
            {
                Local.SaveLocal(Application.StartupPath + "\\Saves\\" + world.player.Name + ".txt");
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
