using System.Collections.Generic;

namespace LinkEngine.Survival
{
    public class Item
    {
        string namePlural;
        public int Cost { get; set; }

        public Item(int id, string name, string namePlur, int cost)
        {
            ID = id;
            Name = name;
            namePlural = namePlur;
            Cost = cost;

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
}

