using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.Dungeon
{
    public class DungeonPlayer : Player
    {
        public short EQUIPMENT_SIZE { get; set; }

        public short Strength { get; set; }
        public short Defense { get; set; }

        public DungeonEquipment[] Equipment { get; set; }

        public DungeonPlayer(int id, string name, int health, int maxHealth) : base(id, name, health, maxHealth)
        {
            Inventory = new List<InventoryItem>();
            Equipment = new DungeonEquipment[EQUIPMENT_SIZE];
        }

        public void UseItem (DungeonItem itemToUse)
        {
            if (!itemToUse.HasBeenDiscovered)
            {
                // discover the item
                DiscoverItem(itemToUse);
            }

            // use the item
            // check if the item is equipment
            if (itemToUse.Equipable)
            {
                DungeonEquipment equ = (DungeonEquipment)itemToUse;
                // if the item is already eqipped it should be unequipped
                if (equ.Equipped)
                {
                    Unequip(equ);
                    ((DungeonEquipment)itemToUse).Equipped = false;
                }
                else
                {
                    // check if another item is equipped in that slot
                    if (Equipment[equ.Slot] == null)
                    {
                        Equip(equ);
                        ((DungeonEquipment)itemToUse).Equipped = true;
                    }
                }
            }

            if (itemToUse.Consumable)
            {
                // remove 1 from the inventory
                RemoveItemFromInventory(itemToUse);
            }
        }

        public void DiscoverItem (DungeonItem item)
        {
            if (!item.HasBeenDiscovered)
            {
                item.ItemName = item.Name;
                item.ItemNamePlural = item.NamePlural;

                item.HasBeenDiscovered = true;
            }
        }

        public void Equip(DungeonEquipment equ)
        {
            Equipment[equ.Slot] = equ;

            Strength += equ.Strength;
            Defense += equ.Defense;
        }
        public void Unequip (DungeonEquipment equ)
        {
            Equipment[equ.Slot] = null;

            Strength -= equ.Strength;
            Defense -= equ.Defense;
        }
    }
}
