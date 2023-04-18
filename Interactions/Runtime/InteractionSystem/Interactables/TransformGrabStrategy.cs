using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{

    internal class TransformGrabStrategy : IGrabStrategy
    {
        private readonly Transform transform;

        public TransformGrabStrategy(Transform transform)
        {
            this.transform = transform;
        }

        public void Initialize()
        {
            //
        }

        public void Grab(Grabable interactable, InteractorBase interactor)
        {
            transform.parent = interactor.AttachmentPoint;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void UnGrab(Grabable interactable, InteractorBase interactor)
        {
            transform.parent = null;
        }
    }
}