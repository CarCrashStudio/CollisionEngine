using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.Dungeon
{
    public class DungeonPlayer : Player
    {
        System.Random rand;
        short equip_size = 4;

        public short EQUIPMENT_SIZE { get { return equip_size; } set { equip_size = value; } }

        public short Strength { get; set; }
        public short Defense { get; set; }

        public DungeonEquipment[] Equipment { get; set; }

        public DungeonPlayer(int id, string name, int health, int maxHealth, short str, short def) : 
            base(id, name, health, maxHealth)
        {
            Inventory = new List<InventoryItem>();
            Equipment = new DungeonEquipment[EQUIPMENT_SIZE];

            Strength = str;
            Defense = def;
        }

        /// <summary>
        /// Decides how it is going to use the given item. If the item is equippable it will try to equip it
        /// But if the item is consumable, the function will try to consume it.
        /// </summary>
        /// <param name="itemToUse">The item selected from inventory</param>
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
                // find the target variable of the player
                // buff that variable with the modifier amount
                switch (((DungeonPotion)itemToUse).TargetVariable)
                {
                    case "Health":
                        Health += ((DungeonPotion)itemToUse).ModifierAmount;
                        break;
                    case "Strength":
                        Strength += ((DungeonPotion)itemToUse).ModifierAmount;
                        break;
                    case "Defense":
                        Defense += ((DungeonPotion)itemToUse).ModifierAmount;
                        break;
                }

                // remove 1 from the inventory
                RemoveItemFromInventory(itemToUse);
            }
        }

        /// <summary>
        /// Changes the name of the item from "???" to it's original name
        /// </summary>
        /// <param name="item">The item being discovered</param>
        public void DiscoverItem (DungeonItem item)
        {
            if (!item.HasBeenDiscovered)
            {
                item.ItemName = item.Name;
                item.ItemNamePlural = item.NamePlural;

                item.HasBeenDiscovered = true;
            }
        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and place the item there.
        /// This will add the Strength and Defense boosts of the item
        /// </summary>
        /// <param name="equ">The item to equip</param>
        public void Equip(DungeonEquipment equ)
        {
            Equipment[equ.Slot] = equ;

            Strength += equ.Strength;
            Defense += equ.Defense;
        }

        /// <summary>
        /// Will find the slot in the array, as defined by the item's 'Slot' variable, and set the slot to null.
        /// This will subtract the Strength and Defense boosts of the item
        /// </summary>
        /// <param name="equ">The item to unequip</param>
        public void Unequip (DungeonEquipment equ)
        {
            Equipment[equ.Slot] = null;

            Strength -= equ.Strength;
            Defense -= equ.Defense;
        }

        /// <summary>
        /// Will attack the chosen target enemy
        /// </summary>
        /// <param name="target">The enemy to attack</param>
        public void Attack (DungeonEnemy target)
        {
            // Randomize how much damage the player will do
            int damage = rand.Next(Strength);

            // randomize how much defense the target will do
            int defense = rand.Next(target.Defense);

            // if defense is greater than damage, attack misses
            if (damage > defense)
            {
                // subtract the defense from the damage
                damage -= defense;
            }

            // reduce target's health by damage value
            target.Health -= damage;
        }
    }
}
