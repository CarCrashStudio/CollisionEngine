using System.Collections.Generic;

namespace LinkEngine.Adventure
{
    public class Item : LinkEngine.Components.Item
    {
        public int Cost { get; set; }
        public List<CraftingItem> Recipe { get; set; }

        public Item(int id, string name, string namePlur, int cost) :
            base (id,name,namePlur,cost)
        {
            Cost = cost;

            Recipe = new List<CraftingItem>();
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
}

