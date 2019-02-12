using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class CraftingItem
    {
        public Item Details { get; set; }
        public int Quantity { get; set; }
        public CraftingItem(Item details, int quantity)
        {
            Details = details;
            Quantity = quantity;
        }
    }
}
