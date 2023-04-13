using Kandooz.Interactions.Runtime.Core;
using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    public class Gradable : InteractableBase
    {
        [SerializeField] private VariableTweener tweener;
        [SerializeField] private Transform rightHandRelativePosition;
        [SerializeField] private Transform leftHandRelativePosition;

        private IGrabStrategy grabStrategy;
        private void Awake()
        {
            tweener ??= GetComponent<VariableTweener>();
            tweener ??= gameObject.AddComponent<VariableTweener>();
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
            var relativeTransform = CurrentInteractor.Hand == HandIdentifier.Left ? leftHandRelativePosition : rightHandRelativePosition;
            relativeTransform.parent = null;
            transform.parent = relativeTransform;
            CurrentInteractor.AttachmentPoint.localPosition = transform.localPosition;
            CurrentInteractor.AttachmentPoint.localRotation = transform.localRotation;

            transform.parent = null;
            relativeTransform.parent = transform;
            LerpObjectToPosition();
            //grabStrategy.Grab(this, CurrentInteractor);
            transform.parent = CurrentInteractor.AttachmentPoint;
            transform.localPosition = Vector3.zero;
            transform.localRotation=Quaternion.identity;
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