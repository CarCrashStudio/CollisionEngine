using System.Drawing;

namespace RPG.Engine
{
    public class Potion : Item
    {
        int amountToHeal;

        public Potion(int _id, string _name, string _namePlural, int _amountToHeal, int _cost, Bitmap _img, World world) :
            base(_id, _name, _namePlural, _cost, _img, world)
        {
            Consumable = true;
            amountToHeal = _amountToHeal;
        }
     
        public int AmountToBuff { get { return amountToHeal; } set { amountToHeal = value; } }
    }
}
