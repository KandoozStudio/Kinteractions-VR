using System.Threading.Tasks;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class RigidBodyGrabStrategy : IGrabStrategy
    {
        private readonly Rigidbody body;
        private readonly  Collider[] colliders;
        private readonly int[] collisionLayers;
        private bool grabbed = false;
        private int layer;
        public RigidBodyGrabStrategy(Rigidbody body)
        {
            this.body = body;
            layer = body.gameObject.layer;
            colliders = body.GetComponentsInChildren<Collider>();
            collisionLayers = new int [colliders.Length];
            for (int i = 0; i < collisionLayers.Length; i++)
            {
                collisionLayers[i] = colliders[i].gameObject.layer;
            }
        }

        public void Initialize() => ToggleRigidbodyAndColliders(false);

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
            body.gameObject.layer = interactor.gameObject.layer;
            foreach (var collider in colliders)
            {
                collider.gameObject.layer = interactor.gameObject.layer;
            }
            grabbed = true;
            //Debug.Break();
        }

        public async void UnGrab(Grabable interactable, InteractorBase interactor)
        {
            interactor.InteractorAttachmentJoint.connectedBody = null;
            ToggleRigidbodyAndColliders(true);
            interactor.ToggleJointObject(false);
            grabbed = false;
            if (grabbed != false) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.layer = collisionLayers[i];
            }
            body.gameObject.layer = layer;

        }
    }
}