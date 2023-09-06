using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    internal class AxisBasedInputManager : InputManagerBase
    {
        private const string LeftTriggerAxis = "XRI_Left_Trigger";
        private const string LeftTriggerButton = "XRI_Left_TriggerButton";
        private const string LeftGripAxis = "XRI_Left_Grip";
        private const string LeftGripButton = "XRI_Left_GripButton";

        private const string RightTriggerAxis = "XRI_Right_Trigger";
        private const string RightTriggerButton = "XRI_Right_TriggerButton";
        private const string RightGripAxis = "XRI_Right_Grip";
        private const string RightGripButton = "XRI_Right_GripButton";


        private void Update()
        {
            leftHand[0] = Input.GetAxis(LeftTriggerAxis);
            leftHand[1] = leftHand[2] = leftHand[3] = Input.GetAxis(LeftGripAxis);
            leftHand.triggerObserver.ButtonState = Input.GetButton(LeftTriggerButton);
            leftHand.gripObserver.ButtonState = Input.GetButton(LeftGripButton);

            rightHand[0] = Input.GetAxis(RightTriggerAxis);
            rightHand[1] = rightHand[2] = rightHand[3] = Input.GetAxis(RightGripAxis);
            rightHand.triggerObserver.ButtonState = Input.GetButton(RightTriggerButton);
            rightHand.gripObserver.ButtonState = Input.GetButton(RightGripButton);
        }
    }
}