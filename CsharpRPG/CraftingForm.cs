using System;
using System.Windows.Forms;
using CsharpRPG.Engine;

namespace CsharpRPG
{
    public partial class CraftingForm : Form
    {
        World world;
        public CraftingForm(World world)
        {
            InitializeComponent();
            this.world = world;

            foreach(Item item in world.Craftable)
            {
                lstCraftable.Items.Add(item.Name);
            }
        }

        private void lstCraftable_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryItem ii = new InventoryItem(world.ItemByName(lstCraftable.SelectedItem.ToString()), 1);
            pbItem.Image = ii.Details.Image;

            rtbItemDesc.Clear();

            foreach (CraftingItem ci in ii.Details.Recipe)
            {
                rtbItemDesc.Text += ci.Details.Name + ": " + ci.Quantity + "\n";
            }
           
        }

        private void lstCraftable_DoubleClick(object sender, EventArgs e)
        {
            InventoryItem ii = new InventoryItem(world.ItemByName(lstCraftable.SelectedItem.ToString()), 1);

            world.player.Inventory.Add(new InventoryItem(world.ItemByID(202), 3));
            if (world.player.HasAllCraftingRecipeItems(ii.Details))
            {
                world.player.RemoveCraftingRecipeItems(ii.Details);
                world.player.AddItemToInventory(ii.Details);
                MessageBox.Show(ii.Details.Name + " added to inventory.");
            }
            else
            {
                MessageBox.Show("You do not have the required items!");
            }
        }
    }
}
