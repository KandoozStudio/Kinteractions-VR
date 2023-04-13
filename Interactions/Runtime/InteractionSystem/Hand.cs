using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Interactions.Runtime
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private HandIdentifier hand;
        [FormerlySerializedAs("inputMapper")] [SerializeField] private Config config;
        public HandIdentifier HandIdentifier => hand;
        public float[] fingers;
        public IObservable<ButtonState> OnTriggerTriggerButtonStateChange => config.InputManagerBase[hand].TriggerObservable ;
        public IObservable<ButtonState> OnGripButtonStateChange =>  config.InputManagerBase[hand].GripObservable;

    }
}