using System.Threading.Tasks;
using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    public class RigidBodyGrabStrategy : IGrabStrategy
    {
        private readonly Rigidbody body;
        private readonly  Collider[] colliders;
        private readonly int[] collisionLayers;
        private bool grabbed = false;
        public RigidBodyGrabStrategy(Rigidbody body)
        {
            this.body = body;
            colliders = body.GetComponentsInChildren<Collider>();
            collisionLayers = new int [colliders.Length];
            for (int i = 0; i < collisionLayers.Length; i++)
            {
                collisionLayers[i] = colliders[i].gameObject.layer;
            }
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
            foreach (var collider in colliders)
            {
                collider.gameObject.layer = interactor.gameObject.layer;
            }

            grabbed = true;
        }

        public async void UnGrab(Grabable interactable, InteractorBase interactor)
        {
            interactor.InteractorAttachmentJoint.connectedBody = null;
            interactor.ToggleJointObject(false);
            grabbed = false;
            await Task.Delay(100);
            if (grabbed != false) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.layer = collisionLayers[i];
            }
        }
    }
}