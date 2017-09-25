using System.Collections.Generic;
using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Item : ScreenObject
    {
        World world;
        string namePlural;
        int cost;

        public Item(int _id, string _name, string _namePlural, int _cost, Bitmap _img, World world) :
            base(_id, _name, _img)
        {
            namePlural = _namePlural;
            cost = _cost;

            Recipe = new List<CraftingItem>();

            this.world = world;
        }

        public string NamePlural { get { return namePlural; } set { namePlural = value; } }
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
        public void Consume (InventoryItem ii)
        {
            Potion con = (Potion)ii.Details;

            world.player.RemoveItemFromInventory(ii.Details);
            world.player.Health += con.AmountToBuff;
            if (world.player.Health > world.player.MaxHealth)
            {
                world.player.Health = world.player.MaxHealth;
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

