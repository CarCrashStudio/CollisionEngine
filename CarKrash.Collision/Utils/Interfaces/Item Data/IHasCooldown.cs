using UnityEngine;
using System.Collections;

namespace CarKrash.Collision.Utils
{
    public interface IHasCooldown
    {
        int ID { get; }
        float CooldownTime { get; }
    }
}
