using System.Collections.Generic;
using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Item
    {
        public World world;
        string namePlural;
        int cost;

        public Item(int _id, string _name, string _namePlural, int _cost, Bitmap _img, World world)
        {
            ID = _id;
            Name = _name;
            namePlural = _namePlural;
            Image = _img;
            cost = _cost;

            Recipe = new List<CraftingItem>();

            this.world = world;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get { return namePlural; } set { namePlural = value; } }
        public Bitmap Image { get; set; }
        public int Cost { get { return cost; } set { cost = value; } }

        public string EquipTag { get; set; }
        public bool Equipable { get; set; }
        public bool Consumable { get; set; }

        public List<CraftingItem> Recipe;

        public bool Equip (InventoryItem ii)
        {
            Equipment equip = (Equipment)ii.Details;
            if (world.player.Equipped.Count != 0)
            {
                foreach (Equipment equ in world.player.Equipped)
                {
                    if (equ.Slot == equip.Slot)
                    {
                        return false; ; // Do not equip, and do not check other places
                    }
                }
                return false;
            }
            else
            {
                world.player.RemoveItemFromInventory(ii.Details);
                world.HUD.UpdateEquipment(equip, world.charSheet);
                world.HUD.UpdateCharSheet(world.charSheet);
                return true;
            }
            
        }
        public void Consume (InventoryItem ii, Entity target)
        {
            Potion con = (Potion)ii.Details;

            world.player.RemoveItemFromInventory(ii.Details);
            target.Health += con.AmountToBuff;
            if (target.Health > target.MaxHealth)
            {
                target.Health = target.MaxHealth;
            }
        }
        public void Craft(InventoryItem ii)
        {
            foreach(InventoryItem p_ii in world.player.Inventory)
            {
                foreach(CraftingItem ci in ii.Details.Recipe)
                {
                    if(p_ii.Details.ID == ci.Details.ID)
                    {
                        if(p_ii.Quantity >= ci.Quantity)
                        {
                            for (int x = 0; x > ci.Quantity; x++)
                            {
                                world.player.RemoveItemFromInventory(p_ii.Details);
                            }
                        }
                    }
                }
            }
        }
    }
    public class InventoryItem
    {
        public Item Details { get; set; }
        public int Quantity { get; set; }
        public InventoryItem(Item details, int quantity)
        {
            Details = details;
            Quantity = quantity;
        }
    }
    public class LootItem
    {
        public Item Details { get; set; }
        public int DropPercentage { get; set; }
        public bool IsDefaultItem { get; set; }

        public LootItem(Item details, int dropPercentage, bool isDefaultItem)
        {
            Details = details;
            DropPercentage = dropPercentage;
            IsDefaultItem = isDefaultItem;
        }
    }
    public class CraftingItem
    {
        public Item Details { get; set; }
        public int Quantity { get; set; }
        public CraftingItem(Item details, int quantity)
        {
            Details = details;
            Quantity = quantity;
        }
    }
}

