using Collision2D.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Collision2D.RPG.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public Currency Cost { get; set; }
        public Sprite Sprite { get; set; }

        public List<CraftingItem> Recipe { get; set; }

        public Item(int id, string name, string namePlur, Currency cost, Texture2D texture)
        {
            Sprite = new Sprite(texture);
            ID = id;
            Name = name;
            NamePlural = namePlur;
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

