using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Item : ScreenObject
    {
        string namePlural;
        int cost;

        public Item(int _id, string _name, string _namePlural, int _cost, Bitmap _img) :
            base(_id, _name, _img)
        {
            namePlural = _namePlural;
            cost = _cost;
        }

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

