using System;
using Kandooz.InteractionSystem.Interactions;
using Kinteractions_VR.Runtime.Animations.Constraints;
using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    public class Hand : MonoBehaviour
    {
        
        [SerializeField] private HandIdentifier hand;
        [SerializeField] private Config config;
        public HandIdentifier HandIdentifier => hand;
        public IObservable<ButtonState> OnTriggerTriggerButtonStateChange => config.InputManager[hand].TriggerObservable ;
        public IObservable<ButtonState> OnGripButtonStateChange =>  config.InputManager[hand].GripObservable;
        public float this[FingerName index] => config.InputManager[hand][(int)index];
        public float this[int index] => config.InputManager[hand][index];

        public void Constrain(IPoseConstrainer constrain)
        {
        }
        public void Unconstrain(IPoseConstrainer constrain)
        {
            
        }
    }
}