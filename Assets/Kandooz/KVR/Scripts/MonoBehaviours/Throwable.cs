using UnityEngine;
using System.Collections;
using UnityEngine.XR;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : MonoBehaviour
    {
        private Interactable interactable;
        private  Rigidbody rigidBody;
        private bool grabbed=false;
        InputDevice device;

        void Start()
        {
            interactable = GetComponent<Interactable>();
            rigidBody = GetComponent<Rigidbody>();
            
            interactable.onHandGrab .AddListener( OnInteractionStart);
            interactable.onHandRelease .AddListener( OnInterActionEnd);
            
        }
        private void Update()
        {
            Vector3 velocity;
            Vector3 angularVelocity;
            device.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
            device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);
            Debug.Log(velocity);
        }

        public void OnInteractionStart(Hand hand)
        {
            grabbed = true;
            rigidBody.isKinematic = true;
            StartCoroutine(LerpToHand(hand.transform,.2f));
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
            grabbed = false;
            Vector3 velocity;
            Vector3 angularVelocity;
            device.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
            device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);
            this.rigidBody.velocity = velocity;
            this.rigidBody.angularVelocity = angularVelocity;
        }

        IEnumerator LerpToHand(Transform hand,float time)
        {
            var targetPosition =-interactable.rightHandPivot.position;
            var targetRotation = Quaternion.Euler(interactable.rightHandPivot.rotation);
            this.transform.parent = hand;

            float t = 0;
            time = 1 / time;
            while (t < 1)
            {
                t += time * Time.deltaTime;
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetPosition, t);
                this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(interactable.rightHandPivot.rotation), t);
                yield return null;
            }
            Debug.Log("done");
        }
    }
}