using MonoLink2D.Entities;
using System.Reflection;
namespace LinkEngine.RPG2D.Models
{
    public class Consumable
    {
        int amountToHeal;

        public Item Details { get; set; }
        public int AmountToBuff { get { return amountToHeal; } set { amountToHeal = value; } }
        public string VariableToBuff { get; set; }

        public Consumable (Item details, int _amountToHeal, int _cost)
        {
            Details = details;
            amountToHeal = _amountToHeal;
        }

        public void Use (Entity Target)
        {
            int temp = (int)(Target.GetType().GetProperty(VariableToBuff).GetValue(Target));
            Target.GetType().GetProperty(VariableToBuff).SetValue(Target, temp + amountToHeal, null);
        }
    }
}
