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
            if (world.player.StatsChanged)
            {
                UpdateStats();
                UpdateInventory();
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
            DrawBars(world.InventoryButton, world.HudForm);
            DrawBars(world.CloseButton, world.HudForm);
        }
        public void UpdateInventory()
        {
            world.InventoryItems = new List<HUDObject>();

            int increment = 1;

            List<Point> temp = new List<Point>();
            temp.Add(new Point(world.InventoryBox.Boundries[0].X + 80, world.InventoryBox.Boundries[0].Y + 15));
            world.Gold = new HUDObject(temp, null, "Gold: " + world.player.Gold.ToString());

            // Refresh player's inventory list
            if (world.InventoryBox.Shown)
            {
                DrawBars(world.InventoryBox, world.HudForm);
            }
            foreach (InventoryItem inventoryItem in world.player.Inventory)
            {
                temp = new List<Point>();
                temp.Add(new Point(world.InventoryBox.Boundries[0].X + 50, world.Gold.Boundries[0].Y + (30 * increment)));
                
                world.InventoryItem = new HUDObject(temp, null, inventoryItem.Details.Name);
                world.InventoryItem.Boundries.Add(new Point(world.InventoryBox.Boundries[1].X, world.InventoryItem.Boundries[0].Y + World.fontSize));
                if (inventoryItem.Quantity > 0)
                {
                    world.InventoryItem.Text = inventoryItem.Details.Name + "(" + inventoryItem.Quantity.ToString() + ") " + inventoryItem.Details.EquipTag;
                }
                if (world.InventoryBox.Shown)
                {
                    DrawBars(world.Gold, world.HudForm);
                    DrawBars(world.InventoryItem, world.HudForm);
                }
                world.InventoryItems.Add(world.InventoryItem);
                increment++;
            }
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
            world.map.Image = new Bitmap("maps/worldmap.png");
            List<NPC> NpcsHere = new List<NPC>();
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
                if (npc != null)
                {
                    world.map.Image = npc.Draw(world.WIDTH, world.HEIGHT, new Point(npc.Location.X * 32, npc.Location.Y * 32), (Bitmap)world.HudForm.Image);
                }
            }
        }
        void UpdateStats()
        {
            HudImg = new Bitmap(world.HudForm.Width, world.HudForm.Height);
            world.NameLevelString.Text = world.player.Name + " (" + world.player.Level + ")";
            world.Class.Text = world.player.Class;
            world.Strength.Text = ": " + world.player.Strength.ToString();
            world.Defense.Text = ": " + world.player.Defense.ToString();

            DrawBars(world.CharImgBox, world.HudForm);
            DrawBars(world.CharImg, world.HudForm);
            DrawBars(world.CharStatBar, world.HudForm);
            DrawBars(world.NameLevelString, world.HudForm);
            DrawBars(world.Class, world.HudForm);
            DrawBars(world.Strength, world.HudForm);
            DrawBars(world.Defense, world.HudForm);

            DrawHealth(world.MainHealthBar, world.HudForm, world.player);
            DrawExp(world.MainExpBar, world.HudForm, world.player);
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
            Bitmap img = new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png");
            if (entity != null)
            {
                double temp = (double)entity.Health / entity.MaxHealth;
                if (temp == 1) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png"); }
                else if (temp >= .9 && temp < 1) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar9.png"); }
                else if (temp >= .8 && temp < .9) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar8.png"); }
                else if (temp >= .7 && temp < .8) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar7.png"); }
                else if (temp >= .6 && temp < .7) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar6.png"); }
                else if (temp >= .5 && temp < .6) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar5.png"); }
                else if (temp >= .4 && temp < .5) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar4.png"); }
                else if (temp >= .3 && temp < .4) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar3.png"); }
                else if (temp >= .2 && temp < .3) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar2.png"); }
                else if (temp >= .1 && temp < .2) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar1.png"); }
            }
            health.Image = img;
            HudImg = health.Draw(world.HudForm.Width, world.HudForm.Height, health.Boundries[0], HudImg);
        }
        public void DrawExp(HUDObject exp, PictureBox form, Character entity)
        {

            Bitmap img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (10).png");
            if (entity != null)
            {
                double temp = (double)entity.Exp / entity.MaxExp;
                if (temp == 1) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (10).png"); }
                else if (temp >= .9 && temp < 1) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (9).png"); }
                else if (temp >= .8 && temp < .9) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (8).png"); }
                else if (temp >= .7 && temp < .8) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (7).png"); }
                else if (temp >= .6 && temp < .7) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (6).png"); }
                else if (temp >= .5 && temp < .6) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (5).png"); }
                else if (temp >= .4 && temp < .5) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (4).png"); }
                else if (temp >= .3 && temp < .4) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (3).png"); }
                else if (temp >= .2 && temp < .3) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (2).png"); }
                else if (temp >= .1 && temp < .2) { img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (1).png"); }
                else { img = new Bitmap("icons/HUDBars/ExpBar/ExpBarEmpty.bmp"); }
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
