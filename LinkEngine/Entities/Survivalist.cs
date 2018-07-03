using LinkEngine.Gameplay.Items;

namespace LinkEngine.Entities
{
    public class Survivalist
    {
        // Character class is the the playable class of the game
        // Inherits from Entities.Player

        public short Hunger { get; set; }
        public bool Full { get; set; }

        public Player Details { get; set; }

        public void Craft(Item itemToCraft)
        {
            if (Details.HasAllCraftingRecipeItems(itemToCraft))
            {
                Details.RemoveCraftingRecipeItems(itemToCraft);
                Details.AddItemToInventory(itemToCraft);
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
