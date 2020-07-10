using Collision2D.RPG.Models;
using Collision2D.RPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG
{
    public static class Helpers
    {
        // TODO: Add Helpers.Random and Helpers.Random.Next()

        public static Attributes Sum(this IEnumerable<Attributes> attributes)
        {
            var finalAttributes = new Attributes();

            foreach (var attribute in attributes)
                finalAttributes += attribute;

            return finalAttributes;
        }
        public static Buff Sum(this IEnumerable<Buff> buffs)
        {
            var finalBuffs = new Buff();
            if(buffs != null)
                foreach (var buff in buffs)
                    finalBuffs.Mod += buff.Mod;

            return finalBuffs;
        }
    }
}
