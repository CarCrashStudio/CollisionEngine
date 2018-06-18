using LinkEngine.Entities;

namespace LinkEngine.RPG
{
    public class RPGItem : Item
    {
        public int Cost { get; set; }

        public RPGItem (int id, string name, string namePlur, int cost) : base (id, name, namePlur)
        {
            Cost = cost;
        }

        public void Use(Character player)
        {
            Potion pot = (Potion)this;

            switch (pot.VariableToBuff)
            {
                case "Health":
                    player.Health += pot.AmountToBuff;
                    break;
                case "Strength":
                    // player.Strength += pot.AmountToBuff;
                    break;
                case "Defense":
                    // player.Defense += pot.AmountToBuff;
                    break;

            }
        }
        public void Equip()
        {

        }
    }
}
