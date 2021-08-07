

using UnityEngine;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandPoseController))]
    public class HandInputMapper : MonoBehaviour
    {
        [SerializeField] private AbstractVRInputManager inputManager;
        [SerializeField] private HandSource hand;
        [UnityEngine.Space]
        [HideInInspector] [SerializeField] HandConstrains constraints = HandConstrains.Free;
        HandPoseController poseController;
        public HandConstrains Constraints { get => constraints; set => constraints = value; }
        public int Pose { get { return poseController.Pose; } set { poseController.Pose = value; } }

        public AbstractVRInputManager InputManager { get => inputManager; set => inputManager = value; }

        private void Awake()
        {
            poseController = GetComponent<HandPoseController>();
        }
        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                var finger = (FingerName)i;
                var value = inputManager.GetFinger(hand, finger);
                MapFingertoPoseController(FingerName.Index, value);
            }
        }

        public void MapFingertoPoseController(FingerName finger, float value)
        {
            var constrainedInput = constraints[finger];
            poseController[finger] = constrainedInput.GetConstrainedValue(value);
        }
    }
}
