using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Hud
    {
        public List<HUDObject> Clickables { get; set; }
        public List<HUDObject> InventoryItems { get; set; }

        // HUD STATS
        public HUDObject CharImgBox { get; set; }
        public HUDObject CharImg { get; set; }
        public HUDObject CharStatBar { get; set; }
        public HUDObject MainHealthBar { get; set; }
        public HUDObject MainExpBar { get; set; }
        public HUDObject NameLevelString { get; set; }
        public HUDObject Class { get; set; }
        public HUDObject Strength { get; set; }
        public HUDObject Defense { get; set; }
        public HUDObject InventoryItem { get; set; }
        public HUDObject Gold { get; set; }

        // HUD INVENTORIES
        public HUDObject InventoryBox { get; set; }
        public HUDObject QuestBox { get; set; }

        // HUD BUTTONS
        public HUDObject InventoryButton { get; set; }
        public HUDObject CloseButton { get; set; }

        // COMBAT SCREEN
        public HUDObject PHealthCombat { get; set; }
        public HUDObject DHealthCombat { get; set; }

        public Bitmap strImg;
        public Bitmap defImg;

        World world { get; set; }
        public Hud(World _world)
        {
            world = _world;
        }

        public void AddCombatHealth()
        {
            PHealthCombat.Picture = world.combat.PHealth;
            DHealthCombat.Picture = world.combat.DHealth;
        }
        public void Update()
        {
            UpdateWorld();
            UpdatePlayer();
            if (world.combat.Initiated)
            {
                UpdateCombatScreen();
            }
        }

        void UpdateStats()
        {
            NameLevelString.Text = world.player.Name + " (" + world.player.Level + ")";
            Class.Text = world.player.Class;
            Strength.Text = ": " + world.player.MaximumDamage.ToString();
            Defense.Text = ": " + world.player.MaximumDefense.ToString();

            world.HudForm.Image = DrawBars(CharImgBox, world.HudForm);
            world.HudForm.Image = DrawBars(CharImg, world.HudForm);
            world.HudForm.Image = DrawBars(CharStatBar, world.HudForm);
            world.HudForm.Image = DrawBars(NameLevelString, world.HudForm);
            world.HudForm.Image = DrawBars(Class, world.HudForm);
            world.HudForm.Image = DrawBars(Strength, world.HudForm);
            world.HudForm.Image = DrawBars(Defense, world.HudForm);

            world.HudForm.Image = DrawBars(InventoryButton, world.HudForm);
            world.HudForm.Image = DrawBars(CloseButton, world.HudForm);

            DrawHealth(MainHealthBar, world.HudForm, world.player);
            DrawExp(MainExpBar, world.HudForm, world.player);
        }
        public void UpdateInventory()
        {
            List<Point> temp = new List<Point>();
            temp.Add(new Point(InventoryBox.Boundries[0].X + 70, InventoryBox.Boundries[0].Y + 15));
            Gold = new HUDObject(temp, null, "Gold: " + world.player.Gold.ToString());

            // Refresh player's inventory list
            foreach (InventoryItem inventoryItem in world.player.Inventory)
            {
                int increment = 1;
                temp = new List<Point>();
                temp.Add(new Point(InventoryBox.Boundries[0].X + 50, InventoryBox.Boundries[0].Y + (35 * increment)));
                InventoryItem = new HUDObject(temp, null, inventoryItem.Details.Name);
                if (inventoryItem.Quantity > 0)
                {
                    InventoryItem.Text = inventoryItem.Details.Name + "(" + inventoryItem.Quantity.ToString() + ") " + inventoryItem.Details.EquipTag;
                }
                if (InventoryBox.Shown)
                {
                    world.HudForm.Image = DrawBars(Gold, world.HudForm);
                    world.HudForm.Image = DrawBars(InventoryItem, world.HudForm);
                }
                InventoryItems.Add(InventoryItem);
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
        void UpdatePlayer()
        {
            world.HudForm.Image = world.player.Draw(world.HudForm.Width, world.HudForm.Height, new Point(world.WIDTH / 2, world.HEIGHT / 2), (Bitmap)world.HudForm.Image);
            //UpdateInventory();
            UpdateStats();
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
            DrawHealth(PHealthCombat, world.combat.PHealth, world.player);
            DrawHealth(DHealthCombat, world.combat.DHealth, world.player.CurrentLocation.MonsterLivingHere);
        }
        public Bitmap DrawBars(HUDObject bar, PictureBox form)
        {
            Font font = new Font("Arial", 16);
            Brush brush = new SolidBrush(Color.Black);

            var bitmap = new Bitmap(form.Image, form.Width, form.Height);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (bar.Text != null)
            {
                if (bar.Image != null)
                {
                    graphics.DrawString(bar.Text, font, brush, bar.Boundries[0].X + bar.Image.Width, bar.Boundries[0].Y);
                }
                else
                {
                    graphics.DrawString(bar.Text, font, brush, bar.Boundries[0].X, bar.Boundries[0].Y);
                    bar.Boundries.Add(new Point(bar.Boundries[0].X + bitmap.Width, bar.Boundries[0].Y + bitmap.Height));
                }
            }
            if (bar.Image != null)
            {
                graphics.DrawImage(bar.Image, bar.Boundries[0]);
            }
            return bitmap;
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
            form.Image = health.Draw(world.HudForm.Width, world.HudForm.Height, health.Boundries[0], (Bitmap)form.Image);
        }
        public void DrawExp(HUDObject exp, PictureBox form, Character entity)
        {

            Bitmap img = new Bitmap("icons/HUDBars/ExpBar/ExpBar (10).png");
            if (entity != null)
            {
                double temp = entity.Exp / entity.MaxExp;
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
            form.Image = exp.Draw(world.HudForm.Width, world.HudForm.Height, exp.Boundries[0], (Bitmap)form.Image);
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
    }
}
