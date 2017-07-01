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
        string sqlConnString = 
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
            SetupGame();
        }
        void SetupGame()
        {
            SQL = new Sql(String.Format(sqlConnString, sqlID, sqlPass));
            Login();
            InitializeScreenControls();
            world.combat = new Combat(lblCombatOutput, panCombat, world.player, pbPHealth, pbPlayer, world.player.CurrentLocation.MonsterLivingHere, pbDHealth, pbDefender, world, wait);
            updateScreen();
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
                object[,] obj = SQL.ExecuteSELECTWHERE("Screenname", arg, "UserData");
                string screen = obj[0,0].ToString();

                arg = String.Format("Screenname = '{0}'", screen);
                obj = SQL.ExecuteSELECTWHERE("Screenname", arg, "CharacterData");
                string str = obj[0, 0].ToString();
                if (str == string.Empty)
                {
                    CreateCharacter(screen);
                    SQL.ExecuteINSERT10("CharacterData", world.player.Name, world.player.Class, world.player.Health, world.player.Exp, world.player.MaxExp, world.player.Level, world.player.Gold, world.player.Location.X, world.player.Location.Y, world.player.CurrentLocation.ID);
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
                string clss = SQL.ExecuteSELECTWHERE("Class", arg, "CharacterData").GetValue(0,0).ToString();
                int health = int.Parse(SQL.ExecuteSELECTWHERE("Health", arg, "CharacterData").GetValue(0, 0).ToString());
                int maxhealth = int.Parse(SQL.ExecuteSELECTWHERE("MaxHealth", arg, "CharacterData").GetValue(0, 0).ToString());
                int mana = int.Parse(SQL.ExecuteSELECTWHERE("Mana", arg, "CharacterData").GetValue(0, 0).ToString());
                int maxmana = int.Parse(SQL.ExecuteSELECTWHERE("MaxMana", arg, "CharacterData").GetValue(0, 0).ToString());
                int damage = int.Parse(SQL.ExecuteSELECTWHERE("Damage", arg, "CharacterData").GetValue(0, 0).ToString());
                int defense = int.Parse(SQL.ExecuteSELECTWHERE("Defense", arg, "CharacterData").GetValue(0, 0).ToString());
                int level = int.Parse(SQL.ExecuteSELECTWHERE("Level", arg, "CharacterData").GetValue(0, 0).ToString());
                int exp = int.Parse(SQL.ExecuteSELECTWHERE("Exp", arg, "CharacterData").GetValue(0, 0).ToString());
                int gold = int.Parse(SQL.ExecuteSELECTWHERE("Gold", arg, "CharacterData").GetValue(0, 0).ToString());
                int maxExp = int.Parse(SQL.ExecuteSELECTWHERE("MaxExp", arg, "CharacterData").GetValue(0, 0).ToString());
                int locX = int.Parse(SQL.ExecuteSELECTWHERE("LocX", arg, "CharacterData").GetValue(0, 0).ToString());
                int locY = int.Parse(SQL.ExecuteSELECTWHERE("LocY", arg, "CharacterData").GetValue(0, 0).ToString());
                string slug = SQL.ExecuteSELECTWHERE("Slug", arg, "CharacterData").GetValue(0, 0).ToString();
                world = new World(pbMap, pbGameForm, new Bitmap("icons/HUDBars/CharStatBar.png"), new Bitmap("icons/HUDBars/CharImgBox.png"), new Bitmap("icons/HUDBars/strength.png"), new Bitmap("icons/HUDBars/defense.png"), new Character(1, screename, clss, new Point(locX, locY), health, maxhealth, mana, maxmana, damage, defense, level, exp, maxExp, gold, slug, new Bitmap("icons/" + slug + ".png"), pbMap)); 
                world.player.MoveTo(world.LocationByID(int.Parse(SQL.ExecuteSELECTWHERE("LastLocation", arg, "CharacterData").GetValue(0, 0).ToString())));
                LoadCharacterSkills(screename);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void LoadCharacterSkills(string screenname)
        {
            try
            {
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, "CharacterSkills");
                for (int i = 0; i < query.Length / 4; i++)
                {
                    if (query[i,1] != null)
                    {
                        world.player.Skills.Add(world.SkillByID(int.Parse(query[i,1].ToString())));
                        world.player.Skills[i].SkillExp = int.Parse(query[i, 2].ToString());
                        world.player.Skills[i].SkillLevel = int.Parse(query[i, 3].ToString());
                    }                    
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void LoadCharacterInventory(string screenname)
        {
            try
            {
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, "CharacterInventory");
                for (int i = 0; i < query.Length / 4; i++)
                {
                    if (query[i, 1] != null)
                    {
                        world.player.Inventory.Add(new InventoryItem(world.ItemByID(int.Parse(query[i, 1].ToString())), int.Parse(query[i, 2].ToString())));
                    }
                }
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
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Health = '" + world.player.Health + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "MaxHealth = '" + world.player.MaxHealth + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Mana = '" + world.player.Mana + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "MaxMana = '" + world.player.MaxMana + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Damage = '" + world.player.MaximumDamage + "'");
                SQL.ExecuteUPDATE("CharacterData", "Screenname = '" + screenname + "'", "Defense = '" + world.player.MaximumDefense + "'");
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
        void SaveCharacterSkills(string screenname)
        {
            try
            {
                SQL.Open();
                foreach (Skill skill in world.player.Skills)
                {
                    object[,] obj = SQL.ExecuteSELECTWHEREAND("*", "Screenname = '" + screenname + "'", "ID = " + skill.ID, "CharacterSkills");
                    if (obj[0, 0] == null)
                    {
                        SQL.ExecuteINSERT4("CharacterSkills", screenname, skill.ID, skill.SkillExp, skill.SkillLevel);
                    }
                    else
                    {
                        SQL.ExecuteUPDATEAND("CharacterSkills", "Screenname = '" + screenname + "'", "ID = " + skill.ID, "Exp = '" + skill.SkillExp + "'");
                        SQL.ExecuteUPDATEAND("CharacterSkills", "Screenname = '" + screenname + "'", "ID = " + skill.ID, "Level = '" + skill.SkillLevel + "'");
                    }              
                }
                SQL.Close();
            }
            catch {  }
        }
        void SaveCharacterInventory(string screenname)
        {
            try
            {
                SQL.Open();                
                foreach (InventoryItem item in world.player.Inventory)
                {
                    object[,] obj = SQL.ExecuteSELECTWHEREAND("Quantity", "Screenname = '" + screenname + "'", "Id = " + item.Details.ID, "CharacterInventory");
                    if(obj[0,0] == null)
                    {
                        SQL.ExecuteINSERT3("CharacterInventory", screenname, item.Details.ID, item.Quantity);
                    }
                    else
                    {
                        SQL.ExecuteUPDATEAND("CharacterInventory", "Screenname = '" + screenname + "'", "Id = " + item.Details.ID, "Quantity = '" + item.Quantity + "'");
                    }                  
                }
                SQL.Close();
            }
            catch {  }
        }

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
        }

        void InitializePlayer()
        {
            world = new World(pbMap, pbGameForm, new Bitmap("icons/HUDBars/CharStatBar.png"), new Bitmap("icons/HUDBars/CharImgBox.png"), new Bitmap("icons/HUDBars/strength.png"), new Bitmap("icons/HUDBars/defense.png"), new Character(1, creator.txtName.Text, creator.cmbClass.SelectedItem.ToString(), new Point(0, 9), CalculateMaxHealth(creator.cmbClass.Text), CalculateMaxHealth(creator.cmbClass.Text), CalculateMaxMana(creator.cmbClass.Text), CalculateMaxMana(creator.cmbClass.Text), CalculateMaxDamage(creator.cmbClass.Text), CalculateMaxDefense(creator.cmbClass.Text), 1, 0, 100, 10, "player", new Bitmap("icons/player.png"), pbMap)); //You, the player, Character creation will be implemented later
            world.player.MoveTo(world.LocationByID(world.LOCATION_ID_HOUSE));
            world.player.Skills.Add(world.SkillByID(world.SKILL_ID_ATTACK));
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
            if (keyPressed == "W" || keyPressed == "S" || keyPressed == "A" || keyPressed == "D")
            {
                if (!world.combat.Initiated)
                {
                    if(world.HUD.InventoryBox.Shown)
                    {
                        CloseBag();
                    }                    

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
            SaveCharacter(world.player.Name);
            SaveCharacterSkills(world.player.Name);
            SaveCharacterInventory(world.player.Name);
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
            foreach(Skill skill in world.player.Skills)
            {
                lstSkills.Items.Add(skill.Name);
            }
            lstSkills.Visible = true;
        }
        private void wait_Tick(object sender, EventArgs e)
        {
            wait.Enabled = false;
            world.combat.DefenderAttack(world.player.CurrentLocation.MonsterLivingHere.Skills[rand.Next(world.player.CurrentLocation.MonsterLivingHere.Skills.Count)]);
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
        private void lstSkills_DoubleClick(object sender, EventArgs e)
        {
            world.combat.PlayerAttack(world.SkillByName(lstSkills.SelectedItem.ToString()));
            wait.Enabled = true;
            lstSkills.Visible = false;
        }
        #endregion
    }
}
