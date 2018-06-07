using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RPG
{
    public class Shop
    {
        public List<InventoryItem> Inventory;

        public Shop ()
        {
            Inventory = new List<InventoryItem>();
        }
        public void Open()
        {
            Form shop = new Form
            {
                SizeGripStyle = SizeGripStyle.Hide,
                FormBorderStyle = FormBorderStyle.None
            };

            ListBox inventory = new ListBox();
            foreach(InventoryItem ii in Inventory)
            {
                if(ii.Quantity != 0)
                    inventory.Items.Add(ii.Details.Name + "(" + ii.Quantity + ")" + " ---- " + ii.Details.Cost + "G");
            }

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
                int cost = World.ItemByName(temp).Cost;

                // Make sure the player doesnt have negative gold
                if (World.Player.Gold >= cost && Inventory[inventory.SelectedIndex].Quantity > 0)
                {
                    World.Player.Gold -= cost;
                    Inventory[inventory.SelectedIndex].Quantity -= 1;
                    World.Player.AddItemToInventory(Inventory[inventory.SelectedIndex].Details);
                    Open();
                }
                    
                shop.Close();
            };

            Button close = new Button
            {
                AutoSize = true,
                Text = "Close"
            };
            close.Click += delegate { shop.Close(); };

            shop.Controls.Add(inventory);
            shop.Controls.Add(close);

            shop.Show();
        }
    }
}
