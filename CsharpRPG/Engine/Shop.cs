using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Shop
    {
        World world;
        public List<InventoryItem> Inventory;

        public Shop (World world)
        {
            Inventory = new List<InventoryItem>();
            this.world = world;
        }
        public void Open()
        {
            Form shop = new Form();
            shop.Size = new System.Drawing.Size(240, 480);
            shop.SizeGripStyle = SizeGripStyle.Hide;
            shop.FormBorderStyle = FormBorderStyle.None;

            ListBox inventory = new ListBox();
            foreach(InventoryItem ii in Inventory)
            {
                inventory.Items.Add(ii.Details.Name + "(" + ii.Quantity + ")" + " ---- " + ii.Details.Cost + "G");
            }
            inventory.Size = new System.Drawing.Size(shop.Size.Width, shop.Size.Height - 32);
            inventory.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif.Name, 16);
            inventory.DoubleClick += delegate
            {
                string item = inventory.SelectedItem.ToString();
                string temp = "";
                foreach (Char c in item)
                {
                    if (c != '(')
                    {
                        temp += c;
                    }
                    else { break; }
                }
                int cost = world.ItemByName(temp).Cost;
                world.player.Gold -= cost;
                Inventory[inventory.SelectedIndex].Quantity -= 1;
                world.player.AddItemToInventory(Inventory[inventory.SelectedIndex].Details);
                Open();
                shop.Close();
            };

            Button close = new Button();
            close.AutoSize = true;
            close.Text = "Close";
            close.Location = new System.Drawing.Point(shop.Size.Width - close.Width, shop.Size.Height - close.Height);
            close.Click += delegate { shop.Close(); };

            shop.Controls.Add(inventory);
            shop.Controls.Add(close);

            shop.Show();
        }
    }
}
