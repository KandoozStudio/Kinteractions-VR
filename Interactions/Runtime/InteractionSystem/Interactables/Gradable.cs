using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    public class Gradable : InteractableBase
    {
        
        private IGrabStrategy grabStrategy;

        private void Awake()
        {
            Rigidbody body = GetComponent<Rigidbody>();
            if (body)
            {
                grabStrategy = new RigidBodyGrabStrategy(body);
            }
            else
            {
                grabStrategy = new TransformGrabStrategy(transform);
            }
        }

        protected override void OnActivate()
        {
            
        }

        protected override void OnAHoverStart()
        {
            
        }

        protected override void OnAHoverEnd()
        {
        }

        protected override void OnSelected()
        {
            grabStrategy.Initialize();
            LerpObjectToPosition();
            grabStrategy.Grab(this, CurrentInteractor);
        }

        private void LerpObjectToPosition()
        {
        }

        protected override void OnDeSelected()
        {
            grabStrategy.UnGrab(this, CurrentInteractor);
        }
    }

}