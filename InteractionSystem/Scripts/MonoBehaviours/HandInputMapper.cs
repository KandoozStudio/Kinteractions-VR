

using UnityEngine;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandPoseController))]
    public class HandInputMapper : MonoBehaviour
    {
        [SerializeField] private AbstractVRInputManager inputManager;
        [SerializeField] private HandSource hand;
        [UnityEngine.Space]
        [HideInInspector] [SerializeField] HandConstraints constraints = HandConstraints.Free;
        HandPoseController poseController;
        public HandConstraints Constraints { get => constraints; set => constraints = value; }
        public int Pose { get { return poseController.Pose; } set { poseController.Pose = value; } }

        public AbstractVRInputManager InputManager { get => inputManager; set => inputManager = value; }
        public HandSource Hand { get => hand; set => this.hand = value; }

        private void Start()
        {
            poseController = GetComponent<HandPoseController>();
        }
        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                var finger = (FingerName)i;
                var value = inputManager.GetFinger(hand, finger);
                MapFingertoPoseController((FingerName)i, value);
                
            }
        }

        public void MapFingertoPoseController(FingerName finger, float value)
        {
            var constrainedInput = constraints[finger];
            poseController[finger] = constrainedInput.GetConstrainedValue(value);
        }
    }
}
