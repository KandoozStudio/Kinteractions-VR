using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    public class RigidBodyGrabStrategy : IGrabStrategy
    {
        private readonly Rigidbody body;
        private readonly  Collider[] colliders;
        public RigidBodyGrabStrategy(Rigidbody body)
        {
            this.body = body;
            colliders = body.GetComponentsInChildren<Collider>();
        }

        public void Initialize() => ToggleRigidbodyAndColliders(true);

        private void ToggleRigidbodyAndColliders(bool enable)
        {
            body.isKinematic = !enable;
            foreach (var collider in colliders)
            {
                collider.enabled = enable;
            }
        }

        public void Grab(Grabable interactable, InteractorBase interactor)
        {
            ToggleRigidbodyAndColliders(true);
            interactor.InteractorAttachmentJoint.connectedBody = body;
            interactor.ToggleJointObject(true);
            
        }

        public void UnGrab(Grabable interactable, InteractorBase interactor)
        {
            interactor.InteractorAttachmentJoint.connectedBody = null;
            interactor.ToggleJointObject(false);
        }
    }
}