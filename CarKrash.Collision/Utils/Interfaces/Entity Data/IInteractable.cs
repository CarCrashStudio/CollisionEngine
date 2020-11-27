using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarKrash.Collision.Utils
{
    public interface IInteractable<T>
    {
        bool Entered { get; }
        bool Active { get; }
        void OnInteractableEnter(IInteracter interacter);
        void OnInteractableExit(IInteracter interacter);

        void Interact(T interacter);
    }
}
