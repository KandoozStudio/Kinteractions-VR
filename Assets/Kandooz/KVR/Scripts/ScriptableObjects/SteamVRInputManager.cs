#if STEAM_VR || VRTK_DEFINE_SDK_STEAMVR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Kandooz.KVR
{

    [CreateAssetMenu(menuName = "Kandooz/KVR/SteamVR VR Input Manager")]
    public class SteamVRInputManager : AbstractVRInputManager
    {
        public SteamVR_Action_Single index;
        public SteamVR_Action_Single middle;
        public SteamVR_Action_Single ring;
        public SteamVR_Action_Single pinky;
        public SteamVR_Action_Single thumb;

        public SteamVR_Action_Boolean trigger;
        public SteamVR_Action_Boolean grip;


        private void OnEnable()
        {
        }
        public override float GetFingerValue(HandType hand, FingerName finger)
        {

            var value = 0f;
            //value = (hand == HandType.right) ? right.fingers[(int)finger]:left.fingers[(int)finger];
            SteamVR_Input_Sources source = (hand == HandType.right) ? SteamVR_Input_Sources.RightHand : SteamVR_Input_Sources.LeftHand;
            switch (finger)
            {
                case FingerName.Thumb:
                    value = thumb.GetAxis(source);
                    break;
                case FingerName.Index:
                    value = index.GetAxis(source);
                    break;
                case FingerName.Middle:
                    value = middle.GetAxis(source);
                    break;
                case FingerName.Ring:
                    value = ring.GetAxis(source);
                    break;

                case FingerName.Pinky:
                    value = pinky.GetAxis(source);
                    break;
                default:
                    break;
            }
            return value;
        }
        public override bool GetFingerDown(HandType hand, FingerName finger)
        {
            bool value = false;
            SteamVR_Input_Sources source = (hand == HandType.right) ? SteamVR_Input_Sources.RightHand : SteamVR_Input_Sources.LeftHand;
            switch (finger)
            {
                case FingerName.Trigger:
                    value=trigger.GetLastStateDown(source);
                    break;
                case FingerName.Grip:
                    value = grip.GetLastStateDown(source);
                    break;
                default:
                    break;
            }
            return value;
        }
        public override bool GetFinger(HandType hand, FingerName finger)
        {
            bool value = false;
            SteamVR_Input_Sources source = (hand == HandType.right) ? SteamVR_Input_Sources.RightHand : SteamVR_Input_Sources.LeftHand;
            switch (finger)
            {
                case FingerName.Trigger:
                    value = trigger.GetLastState(source);
                    break;
                case FingerName.Grip:
                    value = grip.GetLastState(source);
                    break;
                default:
                    break;
            }
            return value;

        }
        public override bool GetFIngerUp(HandType hand, FingerName finger)
        {
            bool value = false;
            SteamVR_Input_Sources source = (hand == HandType.right) ? SteamVR_Input_Sources.RightHand : SteamVR_Input_Sources.LeftHand;
            switch (finger)
            {
                case FingerName.Trigger:
                    value = trigger.GetLastStateUp(source);
                    break;
                case FingerName.Grip:
                    value = grip.GetLastStateUp(source);
                    break;
                default:
                    break;
            }
            return value;

        }
    }
}
#endif