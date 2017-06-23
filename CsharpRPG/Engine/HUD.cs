using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RPG_Engine
{
    public class Hud
    {
        Bitmap TempImg;

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

        Bitmap strImg;
        Bitmap defImg;

        World world { get; set; }
        public Hud(Bitmap _CharStatBar, Bitmap _CharImgBox, Bitmap _strImg, Bitmap _defImg, World _world)
        {
            Clickables = new List<HUDObject>();
            InventoryItems = new List<HUDObject>();

            strImg = _strImg;
            defImg = _defImg;

            world = _world;
            List<Point> temp = new List<Point>();
            temp.Add(new Point(0, 0));
            temp.Add(new Point(temp[0].X + _CharImgBox.Width, temp[0].Y + _CharImgBox.Height));

            CharImgBox = new HUDObject(temp, _CharImgBox);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[0].X + 10, CharImgBox.Boundries[0].Y + 10));
            CharImg = new HUDObject(temp, world.player.Image);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[1].X, 0));
            temp.Add(new Point(temp[0].X + _CharStatBar.Width, temp[0].Y + _CharStatBar.Height));
            CharStatBar = new HUDObject(temp, _CharStatBar);

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.FindCenterofBounds().Y + 25));
            MainHealthBar = new HUDObject(temp, new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png"));

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.FindCenterofBounds().Y + 40));
            MainExpBar = new HUDObject(temp, new Bitmap("icons/HUDBars/ExpBar/ExpBar (10).png"));

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, MainHealthBar.Boundries[0].Y - 80));
            NameLevelString = new HUDObject(temp, null, world.player.Name + " (" + world.player.Level + ")");

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 25));
            Class = new HUDObject(temp, null, world.player.Class);

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 45));
            Strength = new HUDObject(temp, strImg, ": " + world.player.MaximumDamage.ToString());

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X + 70, NameLevelString.Boundries[0].Y + 45));
            Defense = new HUDObject(temp, defImg, ": " + world.player.MaximumDefense.ToString());

            temp = new List<Point>();
            temp.Add(new Point(0, world.HudForm.Height - 32));
            temp.Add(new Point(32, world.HudForm.Height));
            InventoryButton = new HUDObject(temp, new Bitmap("icons/HUDBars/bagbutton.png"));
            InventoryButton.Name = "Bag";
            Clickables.Add(InventoryButton);

            temp = new List<Point>();
            temp.Add(new Point(world.HudForm.Width - 32, world.HudForm.Height - 32));
            temp.Add(new Point(world.HudForm.Width, world.HudForm.Height));
            CloseButton = new HUDObject(temp, new Bitmap("icons/HUDBars/exitbutton.png"));
            CloseButton.Name = "Close";
            Clickables.Add(CloseButton);

            temp = new List<Point>();
            temp.Add(new Point(InventoryButton.Boundries[0].X, InventoryButton.Boundries[0].Y - 400));
            temp.Add(new Point(InventoryButton.Boundries[0].X + 300, InventoryButton.Boundries[0].Y));
            InventoryBox = new HUDObject(temp, new Bitmap("icons/HUDBars/bagbox.png"));

            temp = new List<Point>();
            temp.Add(new Point(0, 0));
            PHealthCombat = new HUDObject(temp);
            DHealthCombat = new HUDObject(temp);
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
            //UpdateMonsters();
            UpdateCombatScreen();
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

            world.HudForm.Image = DrawHealth(MainHealthBar, world.HudForm, world.player, world.HudForm.Width, world.HudForm.Height, world.HudForm.Image);
            world.HudForm.Image = DrawExp(MainExpBar, world.HudForm, world.player, world.HudForm.Width, world.HudForm.Height, world.HudForm.Image);
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
            world.HudForm.Image = world.player.Draw();
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
                world.HudForm.Image = MonsterLivingHere.Draw();
            }
        }
        void UpdateCombatScreen()
        {
            world.combat.PHealth.Image = DrawHealth(PHealthCombat, world.combat.PHealth, world.player, 160, 16);
            world.combat.DHealth.Image = DrawHealth(DHealthCombat, world.combat.DHealth, world.player.CurrentLocation.MonsterLivingHere, 160, 16);
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
        public Bitmap DrawHealth(HUDObject health, PictureBox form, Entity entity, int Width, int Height, Image Image = null)
        {
            var bitmap = health.Image;
            if (Image == null)
            {
                bitmap = new Bitmap(Width, Height);
            }
            else
            {
                bitmap = new Bitmap(Image, Width, Height);
            }
            var graphics = Graphics.FromImage(bitmap);

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
            graphics.DrawImage(health.Image, health.Boundries[0]);

            return bitmap;
        }
        public Bitmap DrawExp(HUDObject exp, PictureBox form, Character entity, int Width, int Height, Image Image = null)
        {
            var bitmap = exp.Image;
            if (Image == null)
            {
                bitmap = new Bitmap(Width, Height);
            }
            else
            {
                bitmap = new Bitmap(Image, Width, Height);
            }
            var graphics = Graphics.FromImage(bitmap);

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
            }
            exp.Image = img;
            graphics.DrawImage(exp.Image, exp.Boundries[0]);

            return bitmap;
        }

        public Point GetCursorPos(MouseEventArgs e)
        {
            return new Point(e.Location.X, e.Location.Y);
        }
    }
    public class HUDObject
    {
        public string Name { get; set; }
        public bool Shown = false;
        public List<Point> Boundries { get; set; }
        public PictureBox Picture { get; set; }
        public Bitmap Image { get; set; }
        public string Text { get; set; }

        public HUDObject(List<Point> _Boundries, Bitmap _Image = null, string _Text = null)
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
