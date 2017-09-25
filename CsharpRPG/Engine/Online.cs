using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Online
    {
        Timer AutoSave = new Timer();

        Sql SQL;
        World world;
        MainForm form;

        string Table_CharacterData;
        string Table_UserInventory;
        string Table_UserEquipment;
        string Table_UserSkills;
        string Table_UserQuests;

        /// <summary>
        /// Creates a new Online Session
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="_world"></param>
        /// <param name="_form"></param>
        /// <param name="arg"></param>
        /// <param name="_userData">The name of the table used to hold player data</param>
        /// <param name="_userInvent">The name of the table used to hold player inventory</param>
        /// <param name="_userEquip">The name of the table used to hold player equipment</param>
        /// <param name="_userSkill">The name of the table used to hold player skill</param>
        /// <param name="_userQuest">The name of the table used to hold player quests</param>
        public Online (Sql sql, World _world, MainForm _form, string arg, string _userData, string _userInvent, string _userEquip, string _userSkill, string _userQuest)
        {
            SQL = sql;
            world = _world;
            form = _form;

            AutoSave.Enabled = true;
            AutoSave.Interval = 1000000;
            AutoSave.Tick += delegate { SaveGame(); };

            Table_CharacterData = _userData;
            Table_UserInventory = _userInvent;
            Table_UserEquipment = _userEquip;
            Table_UserSkills = _userSkill;
            Table_UserQuests = _userQuest;

            CheckForProfile(arg, "UserData");
        }
        
        /// <summary>
        /// Checks the requested table for the requested screen name
        /// </summary>
        /// <param name="arg">The argument to be sent to the search query, i.e. Screenname = 'name' </param>
        /// <param name="table">The table to search</param>
        void CheckForProfile(string arg, string table)
        {
            SQL.Open();
            object[,] obj = SQL.ExecuteSELECTWHERE("Screenname", arg, table);
            string screen = obj[0, 0].ToString();

            arg = String.Format("Screenname = '{0}'", screen);
            obj = SQL.ExecuteSELECTWHERE("Screenname", arg, Table_CharacterData);
            string str = obj[0, 0].ToString();
            if (str == string.Empty)
            {
                CreateCharacter(screen);
                SQL.ExecuteINSERT(Table_CharacterData, world.player.Name + ", " + world.player.Class + ", " + world.player.Health + ", " + world.player.Exp + ", " + world.player.MaxExp + ", " + world.player.Level + ", " + world.player.Gold + ", " + world.player.Location.X + ", " + world.player.Location.Y + ", " + world.player.CurrentLocation.ID);
            }
            else { SQL.Close(); LoadCharacter(screen); }
        }
        void CreateCharacter(string screenname)
        {
            CreatorForm creator = new CreatorForm();
            creator.txtName.Text = screenname;
            creator.txtName.Enabled = false;
            creator.ShowDialog();

            form.InitializePlayer(creator.txtName.Text, creator.cmbClass.SelectedItem.ToString(), new System.Drawing.Point(0, 9), form.CalculateMaxHealth(creator.cmbClass.Text), form.CalculateMaxHealth(creator.cmbClass.Text), form.CalculateMaxMana(creator.cmbClass.Text), form.CalculateMaxMana(creator.cmbClass.Text), form.CalculateMaxDamage(creator.cmbClass.Text), form.CalculateMaxDefense(creator.cmbClass.Text), 1, 0, 100, 10, "player", world.LOCATION_ID_HOUSE);

            creator.Close();
        }

        void LoadCharacter(string screename)
        {
            SQL.Open();
            string arg = String.Format("Screenname = '{0}'", screename);
            string clss = SQL.ExecuteSELECTWHERE("Class", arg, Table_CharacterData).GetValue(0, 0).ToString();
            int health = int.Parse(SQL.ExecuteSELECTWHERE("Health", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int maxhealth = int.Parse(SQL.ExecuteSELECTWHERE("MaxHealth", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int mana = int.Parse(SQL.ExecuteSELECTWHERE("Mana", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int maxmana = int.Parse(SQL.ExecuteSELECTWHERE("MaxMana", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int damage = int.Parse(SQL.ExecuteSELECTWHERE("Damage", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int defense = int.Parse(SQL.ExecuteSELECTWHERE("Defense", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int level = int.Parse(SQL.ExecuteSELECTWHERE("Level", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int exp = int.Parse(SQL.ExecuteSELECTWHERE("Exp", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int gold = int.Parse(SQL.ExecuteSELECTWHERE("Gold", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int maxExp = int.Parse(SQL.ExecuteSELECTWHERE("MaxExp", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int locX = int.Parse(SQL.ExecuteSELECTWHERE("LocX", arg, Table_CharacterData).GetValue(0, 0).ToString());
            int locY = int.Parse(SQL.ExecuteSELECTWHERE("LocY", arg, Table_CharacterData).GetValue(0, 0).ToString());
            string slug = SQL.ExecuteSELECTWHERE("Slug", arg, Table_CharacterData).GetValue(0, 0).ToString();
            int lastLoc = (int)SQL.ExecuteSELECTWHERE("LastLocation", arg, Table_CharacterData).GetValue(0, 0);

            SQL.Close();

            form.InitializePlayer(screename, clss, new System.Drawing.Point(locX, locY), health, maxhealth, mana, maxmana, damage, defense, level, exp, maxExp, gold, slug, lastLoc);
            world = form.world;

            LoadCharacterSkills(screename);
            LoadCharacterInventory(screename);
            LoadCharacterQuests(screename);
            LoadCharacterEquipment(screename);

            world.player.MoveTo(world.LocationByID(lastLoc));
        }
        void LoadCharacterSkills(string screenname)
        {
            try
            {
                SQL.Open();
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, Table_UserSkills);
                for (int i = 0; i < query.Length / 5; i++)
                {
                    if (query[i, 1] != null)
                    {
                        world.player.Skills.Add(new Skill(world.SkillByID(int.Parse(query[i, 1].ToString()))));
                        world.player.Skills[i].SkillExp = int.Parse(query[i, 2].ToString());
                        world.player.Skills[i].SkillLevel = int.Parse(query[i, 3].ToString());
                    }
                }
                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); SQL.Close(); }
        }
        void LoadCharacterInventory(string screenname)
        {
            try
            {
                SQL.Open();
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, Table_UserInventory);
                for (int i = 0; i < query.Length / 4; i++)
                {
                    if (query[i, 1] != null)
                    {
                        world.player.Inventory.Add(new InventoryItem(world.ItemByID(int.Parse(query[i, 1].ToString())), int.Parse(query[i, 2].ToString())));
                    }
                }
                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); SQL.Close(); }
        }
        void LoadCharacterQuests(string screenname)
        {
            try
            {
                SQL.Open();
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, Table_UserQuests);
                for (int i = 0; i < query.Length / 4; i++)
                {
                    if (query[i, 1] != null)
                    {
                        world.player.Quests.Add(new PlayerQuest(world.QuestByID(int.Parse(query[i, 1].ToString())), query[i, 2].ToString()));
                    }
                }
                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); SQL.Close(); }
        }
        void LoadCharacterEquipment(string screenname)
        {
            try
            {
                SQL.Open();
                string arg = String.Format("Screenname = '{0}'", screenname);
                object[,] query = SQL.ExecuteSELECTWHERE("*", arg, Table_UserEquipment);
                for (int i = 0; i < query.Length / 3; i++)
                {
                    if (query[i, 1] != null)
                    {
                        world.HUD.UpdateEquipment(new Equipment((Equipment)world.ItemByID(int.Parse(query[i, 1].ToString()))), form.charSheet);
                    }
                }
                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); SQL.Close(); }
        }

        void SaveCharacter(string screenname)
        {
            try
            {
                SQL.Open();

                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Class = '" + world.player.Class + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Gold = '" + world.player.Gold + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Health = '" + world.player.Health + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "MaxHealth = '" + world.player.MaxHealth + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Mana = '" + world.player.Mana + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "MaxMana = '" + world.player.MaxMana + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Damage = '" + world.player.Strength + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Defense = '" + world.player.Defense + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Exp = '" + world.player.Exp + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "MaxExp = '" + world.player.MaxExp + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "Level = '" + world.player.Level + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "LocX = '" + world.player.Location.X + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "LocY = '" + world.player.Location.Y + "'");
                SQL.ExecuteUPDATE(Table_CharacterData, "Screenname = '" + screenname + "'", "LastLocation = '" + world.player.CurrentLocation.ID + "'");

                SQL.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); SQL.Close(); }
        }
        void SaveCharacterSkills(string screenname)
        {
            try
            {
                SQL.Open();
                foreach (Skill skill in world.player.Skills)
                {
                    object[,] obj = SQL.ExecuteSELECTWHEREAND("*", "Screenname = '" + screenname + "'", "ID = " + skill.ID, Table_UserSkills);
                    if (obj[0, 0] == null)
                    {
                        obj = SQL.ExecuteSELECTWHEREAND("RowNumber", "Screenname = '" + screenname + "'", "ID = " + skill.ID, Table_UserSkills);
                        SQL.ExecuteINSERT(Table_UserSkills, screenname + ", " + skill.ID + ", " + skill.SkillExp + ", " + skill.SkillLevel + ", " + ((int)obj[obj.Length, 0]) + 1);
                    }
                    else
                    {
                        SQL.ExecuteUPDATEAND(Table_UserSkills, "Screenname = '" + screenname + "'", "ID = " + skill.ID, "Exp = '" + skill.SkillExp + "'");
                        SQL.ExecuteUPDATEAND(Table_UserSkills, "Screenname = '" + screenname + "'", "ID = " + skill.ID, "Level = '" + skill.SkillLevel + "'");
                    }
                }
                SQL.Close();
            }
            catch { SQL.Close(); }
        }
        void SaveCharacterInventory(string screenname)
        {
            try
            {
                SQL.Open();
                SQL.ExecuteDELETEWHERE(Table_UserInventory, "Screenname = '" + screenname + "'");

                //object[,] obj = SQL.ExecuteSELECTWHERE("ID", "Screenname = '" + screenname + "'", Table_UserInventory);

                if (world.player.Inventory.Count != 0)
                    foreach (InventoryItem item in world.player.Inventory)
                    {
                        object[,] obj = SQL.ExecuteSELECT("RowNumber", Table_UserInventory);
                        int length = obj.GetLength(0);
                        int rowNumber = (int)obj[length - 1, 0];
                        SQL.ExecuteINSERT(Table_UserInventory, "'" + screenname + "', '" + item.Details.ID + "', '" + item.Quantity + "', '" + (rowNumber + 1).ToString() + "'");
                    }
                SQL.Close();
            }
            catch (Exception ex)
            {
                SQL.Close();
                MessageBox.Show(ex.Message);
            }
        }
        void SaveCharacterQuests(string screenname)
        {
            try
            {
                SQL.Open();
                foreach (PlayerQuest quest in world.player.Quests)
                {
                    object[,] obj = SQL.ExecuteSELECTWHEREAND("Completed", "Screenname = '" + screenname + "'", "Id = " + quest.Details.ID, Table_UserQuests);
                    if (obj[0, 0] == null)
                    {
                        obj = SQL.ExecuteSELECTWHEREAND("RowNumber", "Screenname = '" + screenname + "'", "Id = " + quest.Details.ID, Table_UserQuests);
                        SQL.ExecuteINSERT(Table_UserQuests, screenname + ", " + quest.Details.ID + ", " + quest.IsCompleted + ", " + ((int)obj[obj.Length, 0]) + 1);
                    }
                    else
                    {
                        SQL.ExecuteUPDATEAND(Table_UserQuests, "Screenname = '" + screenname + "'", "Id = " + quest.Details.ID, "Completed = '" + quest.IsCompleted + "'");
                    }
                }
                SQL.Close();
            }
            catch { SQL.Close(); }
        }
        void SaveCharacterEquipment(string screenname)
        {
            try
            {
                SQL.Open();

                SQL.ExecuteDELETEWHERE(Table_UserEquipment, "Screenname = '" + screenname + "'");

                if (world.player.Equipped.Count != 0)
                    foreach (Equipment item in world.player.Equipped)
                    {
                        object[,] obj = SQL.ExecuteSELECT("RowNumber", Table_UserEquipment);
                        int length = obj.GetLength(0);
                        int rowNumber = (int)obj[length - 1, 0];
                        SQL.ExecuteINSERT(Table_UserEquipment, "'" + screenname + "', '" + item.ID + "', '" + (rowNumber + 1).ToString() + "'");
                    }

                SQL.Close();
            }
            catch (Exception ex) { SQL.Close(); MessageBox.Show(ex.Message);}
        }

        public void SaveGame()
        {
            SaveCharacter(world.player.Name);
            SaveCharacterSkills(world.player.Name);
            SaveCharacterInventory(world.player.Name);
            SaveCharacterEquipment(world.player.Name);
        }
    }
}
