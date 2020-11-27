using UnityEngine;

namespace CarKrash.Collision.Unity2D.ScriptableObjects
{
    [CreateAssetMenu]
    public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public float initialValue;
        /*[HideInInspector]*/
        public float runtimeValue;


        public void OnAfterDeserialize() { }
        public void OnBeforeSerialize() { }
    }
}
