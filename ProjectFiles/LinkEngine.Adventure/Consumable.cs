namespace LinkEngine.Adventure
{
    public class Potion
    {
        int amountToHeal;

        public Item Details { get; set; }
        public int AmountToBuff { get { return amountToHeal; } set { amountToHeal = value; } }
        public string VariableToBuff { get; set; }

        public Potion(Item details, int _amountToHeal, int _cost)
        {
            Details = details;
            amountToHeal = _amountToHeal;
        }

    }
}
