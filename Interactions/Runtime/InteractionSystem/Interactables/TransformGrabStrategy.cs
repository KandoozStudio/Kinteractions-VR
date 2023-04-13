using UnityEngine;

namespace Kandooz.Interactions.Runtime
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

        public void Grab(Gradable interactable, InteractorBase interactor)
        {
            transform.parent = this.transform.parent;
        }

        public void UnGrab(Gradable interactable, InteractorBase interactor)
        {
            transform.parent = null;
        }
    }
}