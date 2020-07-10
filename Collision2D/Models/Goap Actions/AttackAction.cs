using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils.Models.Goap_Actions
{
    /// <summary>
    /// Entity Attacking Entity
    /// </summary>
    /// <remarks>
    /// This action will target an entity and move to hit it once.
    /// </remarks>
    public class AttackAction : GoapAction
    {
        bool attacked = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">
        /// This is the Entity to target.
        /// </param>
        public AttackAction(Entities.Entity entity)
        {
            // use the sprite of the entity in order to keep modularity of the AI piece within this library
            this.target = entity.Sprite;
        }
        public override bool checkProceduralPrecondition(object agent)
        {
            return true;
        }

        public override bool isDone()
        {
            return attacked;
        }

        public override bool perform(object agent)
        {
            return true;
        }

        public override bool requiresInRange()
        {
            attacked = true;
            return true;
        }

        public override void reset()
        {
            attacked = false;
        }
    }
}
