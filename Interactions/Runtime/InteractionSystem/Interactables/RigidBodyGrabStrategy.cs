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

        public void Grab(Gradable interactable, InteractorBase interactor)
        {
            ToggleRigidbodyAndColliders(true);
            interactor.InteractorJoint.connectedBody = body;
            interactor.ToggleJointObject(true);
        }

        public void UnGrab(Gradable interactable, InteractorBase interactor)
        {
            interactor.InteractorJoint.connectedBody = null;
            interactor.ToggleJointObject(false);
        }
    }
}