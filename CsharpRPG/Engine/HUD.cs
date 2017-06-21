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
        public HUDObject CharImgBox { get; set; }
        public HUDObject CharStatBar { get; set; }
        public HUDObject HealthBar { get; set; }

        World world { get; set; }
        public Hud(Bitmap _CharStatBar, Bitmap _CharImgBox, World _world)
        {
            world = _world;

            CharImgBox = new RPG_Engine.HUDObject();
            CharImgBox.Boundries = new List<Point>();
            CharImgBox.Boundries.Add(new Point(0, 0));
            CharImgBox.Boundries.Add(new Point(128, 128));
            CharImgBox.Image = _CharImgBox;

            CharStatBar = new RPG_Engine.HUDObject();
            CharStatBar.Boundries = new List<Point>();
            CharStatBar.Boundries.Add(new Point(128, 0));
            CharStatBar.Boundries.Add(new Point(384, 128));
            CharStatBar.Image = _CharStatBar;
        }

        public void Update()
        {
            UpdateStats();
            UpdateWorld();
            UpdatePlayer();
            UpdateMonsters();
        }

        void UpdateStats()
        {
            world.Stats.Text = "Name: " + world.player.Name + " (" + world.player.Level + ")" + Environment.NewLine;
            world.Stats.Text += "Class: " + world.player.Class + Environment.NewLine;
            world.Stats.Text += "Strength: " + world.player.MaximumDamage + Environment.NewLine;
            world.Stats.Text += "Defense: " + world.player.MaximumDefense + Environment.NewLine;
            world.Stats.Text += "Health: " + world.player.Health + "/" + world.player.MaxHealth + Environment.NewLine;
            world.Stats.Text += "Exp: " + world.player.Exp + "/" + world.player.MaxExp + Environment.NewLine;
            world.Stats.Text += "Gold: " + world.player.Gold + Environment.NewLine;
            world.Stats.Text += world.player.Location.ToString();
            world.Stats.Text += Environment.NewLine;
            world.Stats.Text += "==================\n";
            world.Stats.Text += "[    " + world.player.CurrentLocation.Name + "    ]" + Environment.NewLine;
            world.Stats.Text += world.player.CurrentLocation.Description + Environment.NewLine;
            world.Stats.Text += "==================\n";
            world.Stats.Text += world.player.CountDown;
            world.Stats.Text += Environment.NewLine;

            //lblArmor.Text = "Head: " + world.player.HeadEquipped + Environment.NewLine;
            //lblArmor.Text += "Torso: " + world.player.TorsoEquipped + Environment.NewLine;
            //lblArmor.Text += "Legs: " + world.player.LegsEquipped + Environment.NewLine;
            //lblArmor.Text += "Feet: " + world.player.FeetEquipped + Environment.NewLine;
            //lblArmor.Text += "Main Hand: " + world.player.MainHandEquipped + Environment.NewLine;
            //lblArmor.Text += "Off Hand: " + world.player.OffHandEquipped + Environment.NewLine;

            // Refresh player's inventory list
            world.Inventory.Items.Clear();
            foreach (InventoryItem inventoryItem in world.player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    world.Inventory.Items.Add(inventoryItem.Details.Name + "(" + inventoryItem.Quantity.ToString() + ") " + inventoryItem.Details.EquipTag);
                }
            }

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
            world.charForm.Image = world.player.Draw();

            world.map.gameForm.Image = DrawBars(CharImgBox);
            world.map.gameForm.Image = DrawBars(CharStatBar);
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
                world.charForm.Image = MonsterLivingHere.Draw();
            }
        }

        public Bitmap DrawBars(HUDObject bar)
        {
            var bitmap = new Bitmap(world.map.gameForm.Image, world.map.gameForm.Width, world.map.gameForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(bar.Image, bar.Boundries[0]);

            return bitmap;
        }
        public Bitmap DrawHealth()
        {
            var bitmap = new Bitmap(world.map.gameForm.Image, world.map.gameForm.Width, world.map.gameForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            Bitmap img = new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png");

            float temp = world.player.Health / world.player.MaxHealth;
            if (temp == 10) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png"); }
            else if (temp >= .9 && temp < 1) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar9.png"); }
            else if (temp >= .8 && temp < .9) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar8.png"); }
            else if (temp >= .7 && temp < .8) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar7.png"); }
            else if (temp >= .6 && temp < .7) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar6.png"); }
            else if (temp >= .5 && temp < .6) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar5.png"); }
            else if (temp >= .4 && temp < .5) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar4.png"); }
            else if (temp >= .3 && temp < .4) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar3.png"); }
            else if (temp >= .2 && temp < .3) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar2.png"); }
            else if (temp >= .1 && temp < .2) { img = new Bitmap("icons/HUDBars/HealthBar/HealthBar1.png"); }

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(img, new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.FindCenterofBounds().Y + 25));

            return bitmap;
        }

        public Point GetCursorPos(MouseEventArgs e)
        {
            return new Point(e.Location.X, e.Location.Y);
        }
        
        public void ShiftMap(string Direction) 
        {
            TempImg = (Bitmap)world.map.gameForm.Image;
            switch (Direction)
            {
                case "Up":
                    TempImg = DrawMap(TempImg, world.player.Location.X, world.player.Location.Y + 1);
                    break;
                case "Down":
                    TempImg = DrawMap(TempImg, world.player.Location.X, world.player.Location.Y - 1);
                    break;
                case "Left":
                    TempImg = DrawMap(TempImg, world.player.Location.X + 1, world.player.Location.Y);
                    break;
                case "Right":
                    TempImg = DrawMap(TempImg, world.player.Location.X - 1, world.player.Location.Y);
                    break;
            }
            //foreach(Tile tile in world.map.TilesOnMap)
            //{
            //    switch(Direction)
            //    {
            //        case "Up":
            //            tile.Location = new Point(tile.Location.X, tile.Location.Y - 1);
            //            break;
            //        case "Down":
            //            tile.Location = new Point(tile.Location.X, tile.Location.Y + 1);
            //            break;
            //        case "Left":
            //            tile.Location = new Point(tile.Location.X - 1, tile.Location.Y);
            //            break;
            //        case "Right":
            //            tile.Location = new Point(tile.Location.X + 1, tile.Location.Y);
            //            break;
            //    }
            //    TempImg = world.map.DrawTile(tile);
            //}
            //world.map.gameForm.Image = TempImg;
        }
        Bitmap DrawMap(Bitmap TempImg, int x, int y)
        {
            var bitmap = new Bitmap(world.map.gameForm.Image, world.map.gameForm.Width, world.map.gameForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(TempImg, new Point(x,y));

            return bitmap;
        }
    }
    public class HUDObject
    {
        public List<Point> Boundries { get; set; }
        public Bitmap Image { get; set; }

        public Point FindCenterofBounds()
        {
            Point tempPoint = new Point((Boundries[1].X) / 2, (Boundries[1].Y) / 2);
            return tempPoint;
        }
    }
}
