using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [CreateAssetMenu(menuName = "Kandooz/KVR/InputManager")]
    public class VRInputManager : ScriptableObject
    {

#if UNITY_2018
        #region 2018
        [Header("right hand axis")]
        public string leftIndex;
        public string leftMiddle;
        public string leftRing;
        public string leftPinky;
        public string leftThumb;
        public string leftGrip;

        [Header("left hand axis")]
        public string rightIndex ;
        public string rightMiddle;
        public string rightRing ;
        public string rightPinky ;
        public string rightThumb;
        public string rightGrip;

        public float GetFingerValue(HandType hand, FingerName finger)
        {
            var value = 0f;
            switch (finger)
            {
                case FingerName.Thumb:
                    if (hand == HandType.left)
                    {
                        value = (Input.GetKey(KeyCode.JoystickButton8)|| Input.GetKey(KeyCode.JoystickButton16)) ? 1 : 0;
                    }
                    else
                    {
                        value = (Input.GetKey(KeyCode.JoystickButton9)|| Input.GetKey(KeyCode.JoystickButton17)) ? 1 : 0;
                    }
                    break;
                case FingerName.Index:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftIndex);
                    }
                    else
                    {
                        value = Input.GetAxis(rightIndex);
                    }
                    break;

                case FingerName.Middle:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftMiddle);
                    }
                    else
                    {
                        value = Input.GetAxis(rightMiddle);
                    }
                    break;
                case FingerName.Ring:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftRing);
                    }
                    else
                    {
                        value = Input.GetAxis(rightRing);
                    }
                    break;
                case FingerName.Pinky:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftIndex);
                    }
                    else
                    {
                        value = Input.GetAxis(rightIndex);
                    }
                    break;
                default:
                    break;
            }
            return value;
        }
        public float GetGrip(HandType hand)
        {
            var value = 0f;
            switch (hand)
            {
                case HandType.right:
                    value = Input.GetAxis(rightGrip);
                    break;
                case HandType.left:
                    value = Input.GetAxis(leftGrip);
                    break;
            }
            return value;

        }
#endregion
#else

#endif
    }
}