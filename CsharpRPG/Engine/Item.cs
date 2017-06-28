namespace CsharpRPG.Engine
{
    public class Item
    {
        int id;
        string name;
        string namePlural;
        int cost;

        public Item(int _id, string _name, string _namePlural, int _cost)
        {
            id = _id;
            name = _name;
            namePlural = _namePlural;
            cost = _cost;
        }

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string NamePlural { get { return namePlural; } set { namePlural = value; } }
        public int Cost { get { return cost; } set { cost = value; } }

        public string EquipTag { get; set; }
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
}

