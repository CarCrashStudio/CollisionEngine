using LinkEngine.RPG2D.Models;
using MonoLink2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.RPG2D
{
    public static class Helpers
    {
        public static void Attack(this Entity a, Entity b)
        {
            if (a != null)
            {
                int min = ((int)((Attributes)a.TotalAttributes).Strength) - (int)((5 * Math.Round(((Attributes)a.TotalAttributes).Strength / 100, 2, MidpointRounding.AwayFromZero)) * 100);
                int max = ((int)((Attributes)a.TotalAttributes).Strength) + (int)((2 * Math.Round(((Attributes)a.TotalAttributes).Strength / 100, 2, MidpointRounding.AwayFromZero)) * 100);
                if (min < 0)
                    min = 0;
                int damage = MonoLink2D.Helpers.Random.Next(min, max);
                if (damage > 0)
                {
                    // add an attribute modifier for the damage taken
                    // first we want to see if there is already a modifier named 'dmg'
                    if (b.AttributeModifiers != null && b.AttributeModifiers.Count > 0)
                    {
                        var dmg = (from d in a.AttributeModifiers where d.Name == "dmg" select d).First();
                        if (dmg != null)
                        {
                            // we need to remove this damage modifier from the list
                            b.AttributeModifiers.Remove(dmg);
                            // increase the damage of the copy that we created by the value we just calculated
                            dmg.CurrentHP -= damage;
                            // add the copy with the new damage back into the attributes
                            b.AttributeModifiers.Add(dmg);
                        }
                        else b.AttributeModifiers.Add(new Attributes() { CurrentHP = -damage, Name = "dmg" });
                    }
                        
                }
            }
            
        }

        public static Attributes Sum(this IEnumerable<Attributes> attributes)
        {
            var finalAttributes = new Attributes();

            foreach (var attribute in attributes)
                finalAttributes += attribute;

            return finalAttributes;
        }
    }
}
