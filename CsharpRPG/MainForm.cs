using CsharpRPG.Engine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG
{
    public partial class MainForm : Form
    {
        // Engine Object Classes
        World world;
        Sql SQL;

        // Forms
        Journal journal = new Journal();
        CreatorWindow creator;

        // Object Classes
        Random rand = new Random();

        // Variables
        string sqlID = "treyhall";
        string sqlPass = "web.56066";

        public MainForm()
        {
            InitializeComponent();
            SetupGame();
        }

        void Login()
        {
            try
            {
                LoginForm login = new LoginForm(SQL, this);
                login.ShowDialog();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void CheckForProfile(string arg)
        {
            try
            {
                object[] obj = SQL.ExecuteSELECTWHERE("Screenname", arg, "UserData");
                string screen = obj.GetValue(0).ToString();

                arg = String.Format("Screenname = '{0}'", screen);
                obj = SQL.ExecuteSELECTWHERE("Screenname", arg, "CharacterData");
                if (obj.Length == 0)
                {
                    CreateCharacter(screen);
                    SQL.ExecuteINSERT("CharacterData", world.player.Name, world.player.Class, world.player.Health, world.player.Exp, world.player.MaxExp, world.player.Level, world.player.Gold, world.player.Location.X, world.player.Location.Y, world.player.CurrentLocation.ID);
                }
                else { LoadCharacter(screen); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }           
        }
        void CreateCharacter(string screenname)
        {
            creator = new CreatorWindow();
            creator.txtName.Text = screenname;
            creator.txtName.Enabled = false;
            creator.ShowDialog();

            InitializePlayer();

            creator.Close();
        }
        void LoadCharacter(string screename)
        {
            try
            {
                string arg = String.Format("Screenname = '{0}'", screename);
                string clss = SQL.ExecuteSELECTWHERE("Class", arg, "CharacterData").GetValue(0).ToString();
                int level = int.Parse(SQL.ExecuteSELECTWHERE("Level", arg, "CharacterData").GetValue(0).ToString());
                int exp = int.Parse(SQL.ExecuteSELECTWHERE("Exp", arg, "CharacterData").GetValue(0).ToString());
                int gold = int.Parse(SQL.ExecuteSELECTWHERE("Gold", arg, "CharacterData").GetValue(0).ToString());
                int maxExp = int.Parse(SQL.ExecuteSELECTWHERE("MaxExp", arg, "CharacterData").GetValue(0).ToString());
                int locX = int.Parse(SQL.ExecuteSELECTWHERE("LocX", arg, "CharacterData").GetValue(0).ToString());
                int locY = int.Parse(SQL.ExecuteSELECTWHERE("LocY", arg, "CharacterData").GetValue(0).ToString());
                string slug = SQL.ExecuteSELECTWHERE("Slug", arg, "CharacterData").GetValue(0).ToString();
                world.player = new Character(1, screename, clss, new Point(locX, locY), level, exp, maxExp, gold, slug, new Bitmap("icons/" + slug + ".png"), world, pbMap);
                world.player.MoveTo(world.LocationByID(int.Parse(SQL.ExecuteSELECTWHERE("LastLocation", arg, "CharacterData").GetValue(0).ToString())));

                InitializeHUD();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void SaveCharacter(string screenname)
        {
            try
            {
                SQL.Open();

                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Class = '" + world.player.Class + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Gold = '" + world.player.Gold + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Exp = '" + world.player.Exp + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "MaxExp = '" + world.player.MaxExp + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Level = '" + world.player.Level + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "LocX = '" + world.player.Location.X + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "LocY = '" + world.player.Location.Y + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "LastLocation = '" + world.player.CurrentLocation.ID + "'");

                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void SetupGame()
        {
            SQL = new Sql(String.Format("Server=tcp:roguedatabase.database.windows.net,1433;Initial Catalog=rogueDB;Persist Security Info=False;User ID={0};Password={1};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", sqlID, sqlPass));
            world = new World(pbMap, pbGameForm); //Create the World Object that holds all needed objects
            Login();

            InitializeScreenControls();

            world.combat = new Combat(lblCombatOutput, panCombat, world.player, pbPHealth, pbPlayer, world.player.CurrentLocation.MonsterLivingHere, pbDHealth, pbDefender, world, wait);

            updateScreen();
        }

        void updateScreen()
        {
            world.HUD.Update();
            ScrollToBottomOfMessages();
        }
        
        void ScrollToBottomOfMessages()
        {
            //lblOutput.SelectionStart = lblOutput.Text.Length;
            //lblOutput.ScrollToCaret();
        }

        void InitializePlayer()
        {
            world.player = new Character(1, creator.txtName.Text, creator.cmbClass.SelectedItem.ToString(), new Point(0, 9), 1, 0, 100, 10, "player", new Bitmap("icons/player.png"), world, pbMap); //You, the player, Character creation will be implemented later
            world.player.MoveTo(world.LocationByID(world.LOCATION_ID_HOUSE));
            InitializeHUD();
        }
        void InitializeHUD()
        {
            world.HUD = new Hud(new Bitmap("icons/HUDBars/CharStatBar.png"), new Bitmap("icons/HUDBars/CharImgBox.png"), new Bitmap("icons/HUDBars/strength.png"), new Bitmap("icons/HUDBars/defense.png"), world);
        }
        void InitializeScreenControls()
        {
            world.Journal = journal.dgvQuests;
        }
        void OpenBag()
        {
            world.HUD.InventoryBox.Shown = true;
            world.HUD.DrawBars(world.HUD.InventoryBox, world.HudForm);
            world.HUD.UpdateInventory();
        }
        void CloseBag()
        {
            world.HUD.InventoryBox.Shown = false;
            world.HUD.Update();
        }
        
        #region EventHandlers
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyPressed = e.KeyCode.ToString();
            if (keyPressed == "Up" || keyPressed == "Down" || keyPressed == "Left" || keyPressed == "Right")
            {
                if (!world.combat.Initiated)
                {
                    if(world.HUD.InventoryBox.Shown)
                    {
                        CloseBag();
                    }
                    world.player.MovePlayer(keyPressed);
                    updateScreen();
                }
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
                                    SaveCharacter(world.player.Name);
                                    Close();                                    
                                }
                                break;
                            case "Bag":
                                OpenBag();
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
