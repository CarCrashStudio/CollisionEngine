using System.Collections.Generic;

namespace RPG.Engine
{
    public class Item
    { 
        string namePlural;
        int cost;

        public Item(int _id, string _name, string _namePlural, int _cost)
        {
            ID = _id;
            Name = _name;
            namePlural = _namePlural;
            cost = _cost;

            Recipe = new List<CraftingItem>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get { return namePlural; } set { namePlural = value; } }
        public int Cost { get { return cost; } set { cost = value; } }

        public string EquipTag { get; set; }
        public bool Equipable { get; set; }
        public bool Consumable { get; set; }

        public List<CraftingItem> Recipe;
        
        public void Use(Character player)
        {
            Potion pot = (Potion)this;

            switch (pot.VariableToBuff)
            {
                case "Health":
                    player.Health += pot.AmountToBuff;
                    break;
                case "Strength":
                    player.Strength += pot.AmountToBuff;
                    break;
                case "Defense":
                    player.Defense += pot.AmountToBuff;
                    break;

            }
        }
        public void Equip()
        {

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

