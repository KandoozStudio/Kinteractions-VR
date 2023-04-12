using UnityEngine;

namespace Kandooz.Interactions.Runtime
{

    public class KeyboardBasedInput : InputManagerBase
    {
        private void Update()
        {
            leftHand[0] = Input.GetKey(KeyCode.LeftControl) ? 1 : 0;
            leftHand[1] = leftHand[2] = leftHand[3] = Input.GetKey(KeyCode.LeftAlt) ? 1 : 0;

            leftHand.triggerObserver.ButtonState = Input.GetKey(KeyCode.LeftControl);
            leftHand.gripObserver.ButtonState = Input.GetKey(KeyCode.LeftAlt);

            rightHand[0] = Input.GetKey(KeyCode.RightControl) ? 1 : 0;
            rightHand[1] = rightHand[2] = rightHand[3] = Input.GetKey(KeyCode.RightAlt) ? 1 : 0;

            rightHand.triggerObserver.ButtonState = Input.GetKey(KeyCode.RightControl);
            rightHand.gripObserver.ButtonState = Input.GetKey(KeyCode.RightAlt);
        }
    }}