﻿using System.Collections.Generic;

namespace LinkEngine
{
    public class Item :GameObject
    {
        string namePlural;

        public Item(int id, string name, string namePlur, int cost)
        {
            ID = id;
            Name = name;
            namePlural = namePlur;
        }

        public int ID { get; set; }
        public string NamePlural { get { return namePlural; } set { namePlural = value; } }

        public string EquipTag { get; set; }
        public bool Equipable { get; set; }
        public bool Consumable { get; set; }
    }
}

