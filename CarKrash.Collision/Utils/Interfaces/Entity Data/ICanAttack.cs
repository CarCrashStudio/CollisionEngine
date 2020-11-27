using System;
using System.Collections.Generic;
using System.Text;

namespace CarKrash.Collision.Utils
{
    public interface ICanAttack
    {
        /// <summary>
        /// The point of which an attack starts
        /// </summary>
        object AttackPoint { get; }
        /// <summary>
        /// The float value distance between the AttackPoint and the Entity
        /// </summary>
        float DistanceFromEntity { get; }
    }
}
