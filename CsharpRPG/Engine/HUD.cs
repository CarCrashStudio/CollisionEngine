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
            DrawExp(world.HUDObjects[5], world.HudForm, world.player); // MainExpBar
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
