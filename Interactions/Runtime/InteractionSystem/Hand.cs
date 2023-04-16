using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Interactions.Runtime
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private HandIdentifier hand;
        [SerializeField] private Config config;
        public HandIdentifier HandIdentifier => hand;
        public IObservable<ButtonState> OnTriggerTriggerButtonStateChange => config.InputManager[hand].TriggerObservable ;
        public IObservable<ButtonState> OnGripButtonStateChange =>  config.InputManager[hand].GripObservable;

        public float this[int index] => config.InputManager[hand][index];
    }
}