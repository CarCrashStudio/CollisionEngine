using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CarKrash.Collision.Unity2D.ScriptableObjects
{
    public class SignalListener : MonoBehaviour
    {
        public Signal signal;
        public UnityEvent signalEvent;
        public void OnSignalRaised()
        {
            signalEvent.Invoke();
        }

        private void OnEnable()
        {
            signal.RegisterListener(this);
        }

        private void OnDisable()
        {
            signal.DeRegisterListener(this);
        }
    }
}
