using System;
using Kandooz.InteractionSystem.Animations.Constraints;
using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private HandIdentifier hand;
        [SerializeField] private Config config;
        public HandIdentifier HandIdentifier
        {
            get => hand;
            internal set => hand = value;
        }
        internal Config Config
        {
            set => config = value;
        }
        public IObservable<ButtonState> OnTriggerTriggerButtonStateChange => config.InputManager[hand].TriggerObservable ;
        public IObservable<ButtonState> OnGripButtonStateChange =>  config.InputManager[hand].GripObservable;
        public float this[FingerName index] => config.InputManager[hand][(int)index];
        public float this[int index] => config.InputManager[hand][index];
        private IPoseable poseDriver;

        private void Awake()
        {
            poseDriver = GetComponent<IPoseable>();
        }

        public void Constrain(IPoseConstrainer constrainer)
        {
            switch (hand)
            {
                case HandIdentifier.Left:
                    poseDriver.Constrains = constrainer.LeftPoseConstrains;
                    break;
                case HandIdentifier.Right:
                    poseDriver.Constrains = constrainer.RightPoseConstrains;
                    break;
            }
        }
        public void Unconstrain(IPoseConstrainer constrain)
        {
            poseDriver.Constrains= PoseConstrains.Free;
        }

        public static implicit operator HandIdentifier(Hand hand) => hand.HandIdentifier;
    }
}