using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Item
    { 
        string namePlural;

        public Item(int _id, string _name, string _namePlural)
        {
            ID = _id;
            Name = _name;
            namePlural = _namePlural;

            Recipe = new List<CraftingItem>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get { return namePlural; } set { namePlural = value; } }

        public string EquipTag { get; set; }
        public bool Equipable { get; set; }
        public bool Consumable { get; set; }

        public List<CraftingItem> Recipe;
        
        
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

