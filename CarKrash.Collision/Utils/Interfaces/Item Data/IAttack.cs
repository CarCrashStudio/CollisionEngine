using UnityEngine;
using System.Collections;

namespace CarKrash.Collision.Utils
{
    public interface IAttack
    {
        void Attack(Entity attacker, Weapon weapon, Animator animator, GameObject meleePoint, float meleeRadius, LayerMask enemyLayers);
        void Attack(Entity attacker, Weapon weapon);
    }
}
