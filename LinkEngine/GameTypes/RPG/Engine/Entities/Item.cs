using LinkEngine.Entities;

namespace LinkEngine.RPG
{
    /// <summary>
    /// RPGItem is the the RPG derivitive of the Entities.Item Class.
    /// It holds the cost of the item in ingame currency
    /// </summary>
    public class RPGItem : Item
    {
        public int Cost { get; set; }

        public RPGItem (int id, string name, string namePlur, int cost) : base (id, name, namePlur)
        {
            Cost = cost;
        }
    }
}
