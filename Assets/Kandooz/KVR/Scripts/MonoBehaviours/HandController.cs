using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandTracker))]
    public class HandController : MonoBehaviour
    {
        /// <summary>
        /// the input manager is to get input from the diffrent buttons on the controllers
        /// </summary>
        public VRInputManager inputManager;
        private HandType type;
        private HandAnimationController animationController;
        private HandConstrains constraints;
        private new Collider []collider;
        private void Start()
        {
            type = GetComponent<HandTracker>().hand;
            collider = new Collider[1];
        }
        public void ResetHandControllerStrategy()
        {
            constraints = HandConstrains.Free;
        }
        public void SetHandConstraints(HandConstrains constraints)
        {
            this.constraints = constraints;
        }
        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                var finger = (FingerName)i;
                var value = inputManager.GetFingerValue(type, finger);
                switch (finger)
                {
                    case FingerName.Thumb:
                        value = Mathf.Clamp(constraints.thumbFingerLimits.x, constraints.thumbFingerLimits.y, value);
                        break;
                    case FingerName.Index:
                        value = Mathf.Clamp(constraints.indexFingerLimits.x, constraints.indexFingerLimits.y, value);
                        break;
                    case FingerName.Middle:
                        value = Mathf.Clamp(constraints.middleFingerLimits.x, constraints.middleFingerLimits.y, value);
                        break;
                    case FingerName.Ring:
                        value = Mathf.Clamp(constraints.ringFingerLimits.x, constraints.ringFingerLimits.y, value);
                        break;
                    case FingerName.Pinky:
                        value = Mathf.Clamp(constraints.pinkyFingerLimits.x, constraints.pinkyFingerLimits.y, value);
                        break;
                }
                animationController[i] = value;
            }

        }
    }
}
