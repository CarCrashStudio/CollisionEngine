using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.Models
{
    
    public class Spell
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public SpellEffect Effect { get; set; }

        public float ManaCost { get; set; }
        public int RequiredLevel { get; set; }
    }

    public class SpellEffect
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public SpellSaves SaveType { get; set; }
        public StatusEffects TargetStatusEffect { get; set; }
        public Attributes Mod { get; set; }

    }
}
