using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class GestureSetter : MonoBehaviour
    {
        [SerializeField] private HandSource hand;
        [SerializeField] private AbstractVRInputManager inputManager;
        [SerializeField] private GestureVariable gestureVariable;
        private bool thumb;
        private bool index;
        private bool grip;
        private void Update()
        {
            ReadFingerStatus();
            SetGesture();
        }

        private void SetGesture()
        {
            if (thumb)
            {
                if (grip)
                {
                    gestureVariable.value = index ? Gesture.Fist : Gesture.Pointing;
                }
                else if (!index)
                {
                    gestureVariable.value = Gesture.Three;
                }
            }
            else if (grip)
            {
                gestureVariable.value = index ? Gesture.ThumbsUp : Gesture.Pointing;
            }
            else
            {
                gestureVariable.value = index ? Gesture.None : Gesture.relaxed;

            }
        }
        private void ReadFingerStatus()
        {
            thumb = inputManager.GetFinger(hand, FingerName.Thumb) > .5f;
            index = inputManager.GetFinger(hand, FingerName.Index) > .5f;
            grip = inputManager.GetFinger(hand, FingerName.Middle) > .5f;
        }
    }
}
