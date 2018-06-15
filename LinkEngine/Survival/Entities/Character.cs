using LinkEngine.Entities;

namespace LinkEngine.Survival
{
    public class Character : Player
    {
        // Character class is the the playable class of the game
        // Inherits from Entities.Player

        public short Hunger { get; set; }
        public bool Full { get; set; }
        

        public Character (int id, string name, int health, int maxHealth) :
            base (id, name, health, maxHealth)
        {

        }

        public void Craft(Item itemToCraft)
        {
            if (HasAllCraftingRecipeItems(itemToCraft))
            {
                RemoveCraftingRecipeItems(itemToCraft);
                AddItemToInventory(itemToCraft);
            }
        }

        public void LoseHunger(short amount)
        {
            Hunger -= amount;
        }
        
        public void Eat (short amountToFeed)
        {
            Hunger += amountToFeed;
        }
        
    }
}
