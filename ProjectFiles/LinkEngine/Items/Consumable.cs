namespace LinkEngine.Components
{
    public class Consumable : Item
    {
        int amountToHeal;

        public Consumable(int _id, string _name, string _namePlural, int _amountToHeal, int _cost) :
            base(_id, _name, _namePlural, _cost)
        {
            Consumable = true;
            amountToHeal = _amountToHeal;
        }

        public int AmountToBuff { get { return amountToHeal; } set { amountToHeal = value; } }
        public string VariableToBuff { get; set; }
    }
}
