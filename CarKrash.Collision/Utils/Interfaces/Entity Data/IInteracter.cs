using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarKrash.Collision.Utils
{
    public interface IInteracter<T>
    {
        IInteractable<T> currentInteractable { get; set; }
    }
}
