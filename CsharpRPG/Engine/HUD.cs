using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Hud
    {
        World world { get; set; }
        int offset = 0;
        Bitmap HudImg;
        public 
            List<NPC> NpcsHere;
        public Hud(World _world, int offset)
        {
            world = _world;
            this.offset = offset;

            XOverHang = world.HudForm.Width - Screen.PrimaryScreen.Bounds.Width;
            YOverHang = world.HudForm.Height - Screen.PrimaryScreen.Bounds.Height;
        }

        int XOverHang = 0;
        int YOverHang = 0;

        public void AddCombatHealth()
        {
            world.PHealthCombat.Picture = world.combat.combat.pbCurrentHealth;
            world.DHealthCombat.Picture = world.combat.combat.pbCurrentMonsterHealth;
        }

        public void Update()
        {
            UpdateWorld();
            UpdatePlayer();
            UpdateNPCs();
            UpdateQuestLog();
            if (world.player.StatsChanged)
            {
                UpdateStats();
                world.player.StatsChanged = false;
            }
            UpdateButtons();
            world.HudForm.Image = DrawHud();
            if (world.combat.Initiated)
            {
                UpdateCombatScreen();
            }
        }

        public void UpdateScreenSize()
        {
            world.HudForm.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
        public void UpdateButtons()
        {
            DrawBars(world.Clickables[0], world.HudForm);
            DrawBars(world.Clickables[1], world.HudForm);
            DrawBars(world.Clickables[2], world.HudForm);
        }
        public void UpdateQuestLog()
        {
            // Refresh player's quest list
            world.Journal.RowHeadersVisible = false;

            world.Journal.ColumnCount = 2;
            world.Journal.Columns[0].Name = "Name";
            world.Journal.Columns[0].Width = 100;
            world.Journal.Columns[1].Name = "Done?";

            world.Journal.Rows.Clear();

            foreach (PlayerQuest playerQuest in world.player.Quests)
            {
                world.Journal.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }
        public void UpdateNPCs()
        {
            // world.map.Image = new Bitmap("maps/worldmap.png");
            NpcsHere = new List<NPC>();
            try
            {
                foreach (NPC npc in world.player.CurrentLocation.NPCsLivingHere)
                {
                    NpcsHere.Add(npc);
                }
            }
            catch { } // Npcs in CurrentLoc
            try
            {
                foreach (NPC npc in world.player.CurrentLocation.LocationToNorth.NPCsLivingHere)
                {
                    NpcsHere.Add(npc);
                }
            }
            catch { } // Npcs in LocToNorth
            try
            {
                foreach (NPC npc in world.player.CurrentLocation.LocationToSouth.NPCsLivingHere)
                {
                    NpcsHere.Add(npc);
                }
            }
            catch { } // Npcs in LocToSouth
            try
            {
                foreach (NPC npc in world.player.CurrentLocation.LocationToWest.NPCsLivingHere)
                {
                    NpcsHere.Add(npc);
                }
            }
            catch { } // Npcs in LocToWest
            try
            {
                foreach (NPC npc in world.player.CurrentLocation.LocationToEast.NPCsLivingHere)
                {
                    NpcsHere.Add(npc);
                }
            }
            catch { } // Npcs in LocToEasst

            foreach (NPC npc in NpcsHere)
            {
               world.HudForm.Image = npc.Draw(world.HudForm.Image.Width, world.HudForm.Image.Height, new Point((npc.Location.X + world.map.MapLoc.X) * 32, (npc.Location.Y + (world.map.MapLoc.Y - 10)) * 32), (Bitmap)world.HudForm.Image);
            }
        }
        public void UpdateBag(InventoryForm inventory)
        {
            int padding = 10;
            int i = 0;
            inventory.pnlSlotPanel.Controls.Clear();
            inventory.lblGold.Text = world.player.Gold.ToString();
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
                            Form frm = new Form();
                            frm.Size = new Size(100, 100);
                            frm.FormBorderStyle = FormBorderStyle.None;
                            frm.Location = Cursor.Position;

                            ListBox lstOptions = new ListBox();
                            lstOptions.Size = new Size(100, 100);

                            frm.Controls.Add(lstOptions);

                            InventoryItem ii = world.player.ItemByName(temp.Name);
                            if (ii.Details.Equipable)
                            {
                                lstOptions.Items.Add("Equip");
                            }
                            if (ii.Details.Consumable)
                            {
                                lstOptions.Items.Add("Consume");
                            }
                            if(ii.Details.Recipe != null) //.count == 0
                            {
                                lstOptions.Items.Add("Craft");
                            }
                            frm.Show();
                            lstOptions.DoubleClick += delegate
                            {
                                if (lstOptions.SelectedItem.ToString() == "Equip")
                                {
                                    if (ii.Details.Equip(ii))
                                    {
                                        temp.Name = "";
                                        temp.Image = null;
                                        temp.Controls[0].Text = "0";
                                        temp.Controls[0].Visible = false;
                                    }
                                }
                                if (lstOptions.SelectedItem.ToString() == "Consume")
                                {
                                    ii.Details.Consume(ii);
                                }
                                if (lstOptions.SelectedItem.ToString() == "Craft")
                                {
                                    ii.Details.Craft(ii);
                                }
                                frm.Close();
                                UpdateBag(world.inventory);
                            };

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
        public void UpdateCharSheet(CharacterForm charSheet)
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
        public void UpdateEquipment(Equipment equ, CharacterForm charSheet)
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
        void UpdateStats()
        {
            HudImg = new Bitmap(world.HudForm.Width, world.HudForm.Height);

            world.HUDObjects[5].Text = world.player.Name + " (" + world.player.Level + ")"; // NameLevelString
            world.HUDObjects[6].Text = world.player.Class; // Class
            world.HUDObjects[7].Text = ": " + world.player.Strength.ToString(); // Strength
            world.HUDObjects[8].Text = ": " + world.player.Defense.ToString(); // Defense

            DrawBars(world.HUDObjects[0], world.HudForm); // CharImgBox
            DrawBars(world.HUDObjects[1], world.HudForm); // CharImg
            DrawBars(world.HUDObjects[2], world.HudForm); // CharStatsBar
            DrawBars(world.HUDObjects[5], world.HudForm); // NameLevelString
            DrawBars(world.HUDObjects[6], world.HudForm); // Class
            DrawBars(world.HUDObjects[7], world.HudForm); // Strength
            DrawBars(world.HUDObjects[8], world.HudForm); // Defense

            DrawHealth(world.HUDObjects[3], world.HudForm, world.player); // MainHealthBar
            DrawExp(world.HUDObjects[4], world.HudForm, world.player); // MainExpBar
        }
        void UpdatePlayer()
        {
            world.HudForm.Image = world.player.Draw(world.HudForm.Width, world.HudForm.Height, new Point(world.WIDTH / 2, (world.HEIGHT / 2) - 320), (Bitmap)world.HudForm.Image);
            //UpdateInventory();
            
        }
        void UpdateWorld()
        {
            world.map.SetMap();
        }
        void UpdateMonsters()
        {
            Monster MonsterLivingHere = world.player.CurrentLocation.MonsterLivingHere;
            if (MonsterLivingHere != null)
            {
                world.HudForm.Image = MonsterLivingHere.Draw(world.WIDTH, world.HEIGHT, new Point(MonsterLivingHere.Location.X * 32, MonsterLivingHere.Location.Y * 32), (Bitmap)world.HudForm.Image);
            }
        }
        void UpdateCombatScreen()
        {
            DrawHealth(world.PHealthCombat, world.combat.combat.pbCurrentHealth, world.player);
            DrawHealth(world.DHealthCombat, world.combat.combat.pbCurrentMonsterHealth, world.player.CurrentLocation.MonsterLivingHere);
        }

        public void DrawBars(HUDObject bar, PictureBox form)
        {
            if(bar.Text != null)
            {
                if (bar.Image != null)
                {
                    HudImg = bar.DrawText(bar.Text, form.Width, form.Height, new Point(bar.Boundries[0].X + bar.Image.Width + 5, bar.Boundries[0].Y), HudImg);
                }
                else
                {
                    HudImg = bar.DrawText(bar.Text, form.Width, form.Height, bar.Boundries[0], HudImg);

                }
            }
            if(bar.Image != null)
            {
                HudImg = bar.Draw(form.Width, form.Height, bar.Boundries[0], HudImg);
            }
        }
        public void DrawHealth(HUDObject health, PictureBox form, Entity entity)
        {
            Bitmap img = new Bitmap(Properties.Resources.HealthBar10);
            if (entity != null)
            {
                double temp = (double)entity.Health / entity.MaxHealth;
                if (temp == 1) { img = new Bitmap(Properties.Resources.HealthBar10); }
                else if (temp >= .9 && temp < 1) { img = new Bitmap(Properties.Resources.HealthBar9); }
                else if (temp >= .8 && temp < .9) { img = new Bitmap(Properties.Resources.HealthBar8); }
                else if (temp >= .7 && temp < .8) { img = new Bitmap(Properties.Resources.HealthBar7); }
                else if (temp >= .6 && temp < .7) { img = new Bitmap(Properties.Resources.HealthBar6); }
                else if (temp >= .5 && temp < .6) { img = new Bitmap(Properties.Resources.HealthBar5); }
                else if (temp >= .4 && temp < .5) { img = new Bitmap(Properties.Resources.HealthBar4); }
                else if (temp >= .3 && temp < .4) { img = new Bitmap(Properties.Resources.HealthBar3); }
                else if (temp >= .2 && temp < .3) { img = new Bitmap(Properties.Resources.HealthBar2); }
                else if (temp >= .1 && temp < .2) { img = new Bitmap(Properties.Resources.HealthBar1); }
            }
            health.Image = img;
            HudImg = health.Draw(world.HudForm.Width, world.HudForm.Height, health.Boundries[0], HudImg);
        }
        public void DrawExp(HUDObject exp, PictureBox form, Character entity)
        {

            Bitmap img = new Bitmap(Properties.Resources.ExpBar__10_);
            if (entity != null)
            {
                double temp = (double)entity.Exp / entity.MaxExp;
                if (temp == 1) { img = new Bitmap(Properties.Resources.ExpBar__10_); }
                else if (temp >= .9 && temp < 1) { img = new Bitmap(Properties.Resources.ExpBar__9_); }
                else if (temp >= .8 && temp < .9) { img = new Bitmap(Properties.Resources.ExpBar__8_); }
                else if (temp >= .7 && temp < .8) { img = new Bitmap(Properties.Resources.ExpBar__7_); }
                else if (temp >= .6 && temp < .7) { img = new Bitmap(Properties.Resources.ExpBar__6_); }
                else if (temp >= .5 && temp < .6) { img = new Bitmap(Properties.Resources.ExpBar__5_); }
                else if (temp >= .4 && temp < .5) { img = new Bitmap(Properties.Resources.ExpBar__4_); }
                else if (temp >= .3 && temp < .4) { img = new Bitmap(Properties.Resources.ExpBar__3_); }
                else if (temp >= .2 && temp < .3) { img = new Bitmap(Properties.Resources.ExpBar__2_); }
                else if (temp >= .1 && temp < .2) { img = new Bitmap(Properties.Resources.ExpBar__1_); }
                else { img = new Bitmap(Properties.Resources.ExpBarEmpty); }
            }
            exp.Image = img;
            HudImg = exp.Draw(world.HudForm.Width, world.HudForm.Height, exp.Boundries[0], HudImg);
        }
        public Bitmap DrawHud()
        {
            var bitmap = new Bitmap(world.HudForm.Image, world.HudForm.Width, world.HudForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.DrawImage(HudImg, new PointF(0, 0));
            return bitmap;
        }

        public Point GetCursorPos(MouseEventArgs e)
        {
            return new Point(e.Location.X, e.Location.Y);
        }
    }
    public class HUDObject : ScreenObject
    {
        public bool Shown = false;
        public List<Point> Boundries { get; set; }
        public PictureBox Picture { get; set; }
        public string Text { get; set; }

        public HUDObject(List<Point> _Boundries, Bitmap _Image = null, string _Text = null) : 
            base(1000,"HUDObject",_Image)
        {
            Boundries = _Boundries;
            Image = _Image;
            Text = _Text;
        }
        public Point FindCenterofBounds()
        {
            Point tempPoint = new Point((Boundries[1].X) / 2, (Boundries[1].Y) / 2);
            return tempPoint;
        }
        public bool IsInBounds(MouseEventArgs e)
        {
            if (e.Location.X > Boundries[0].X && e.Location.X < Boundries[1].X)
            {
                if (e.Location.Y > Boundries[0].Y && e.Location.Y < Boundries[1].Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
