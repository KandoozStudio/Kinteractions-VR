using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.Animations;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : MonoBehaviour
    {
        private Interactable interactable;
        private Rigidbody rigidBody;
        private bool grabbed = false;
        InputDevice device;
        Vector3 targetPosition;
        Vector3 targetROtation;
        void Start()
        {
            interactable = GetComponent<Interactable>();
            rigidBody = GetComponent<Rigidbody>();

            interactable.onInteractionStart.AddListener(OnInteractionStart);
            interactable.onInteractionEnd.AddListener(OnInterActionEnd);
            rigidBody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            Vector3 velocity;
            Vector3 angularVelocity;
            device.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
            device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);
            Debug.Log(velocity);

        }
        private void FixedUpdate()
        {
            if (grabbed)
                this.rigidBody.position = this.transform.parent.TransformPoint(targetPosition);
        }
        public void OnInteractionStart(Hand hand)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(LerpToHand(hand, .05f));
            switch (hand.hand)
            {
                case HandType.right:
                    device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                    break;
                case HandType.left:
                    device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                    break;
                default:
                    break;
            }
        }
        public void OnInterActionEnd(Hand hand)
        {
            this.transform.parent = null;
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            grabbed = false;

        }

        IEnumerator LerpToHand(Hand hand, float time)
        {
            var obj = new GameObject().transform;
            obj.parent = this.transform;
            switch (hand.hand)
            {
                case HandType.right:
                    obj.transform.localPosition = interactable.rightHandPivot.position;
                    obj.transform.localRotation = Quaternion.Euler(interactable.rightHandPivot.rotation);
                    break;
                case HandType.left:
                    obj.transform.localPosition = interactable.leftHandPivot.position;
                    obj.transform.localRotation = Quaternion.Euler(interactable.leftHandPivot.rotation);
                    break;
                default:
                    break;
            }
            obj.parent = null;
            this.transform.parent = obj;
            targetPosition = this.transform.localPosition;
            var targetRotation = this.transform.localRotation;
            Destroy(obj.gameObject);
            this.transform.parent = hand.transform;

            float t = 0;
            time = 1 / time;
            while (t < 1)
            {
                t += time * Time.deltaTime;
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetPosition, t);
                this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, targetRotation, t);
                yield return null;
            }
            this.transform.localPosition = targetPosition;
            this.transform.localRotation = targetRotation;
            Debug.Log("done");
            Debug.Log(targetPosition);
            grabbed = true;
        }
    }
}