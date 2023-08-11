using System;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class Grabable : InteractableBase
    {
        [SerializeField] private VariableTweener tweener;

        private TransformTweenable transformTweenable= new();
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

        protected override void Activate()
        {
            
        }

        protected override void StartHover()
        {
            
        }

        protected override void EndHover()
        {
        }

        protected override void Select()
        {
            grabStrategy.Initialize();
            InitializeAttachmentPointTransform();
            LerpObjectToPosition(() => grabStrategy.Grab(this, CurrentInteractor));
        }

        private void InitializeAttachmentPointTransform()
        {
            var relativeTransform = CurrentInteractor.Hand == HandIdentifier.Left ? LeftHandRelativePosition : RightHandRelativePosition;
            relativeTransform.parent = null;
            transform.parent = relativeTransform;
            CurrentInteractor.AttachmentPoint.localPosition = transform.localPosition;
            CurrentInteractor.AttachmentPoint.localRotation = transform.localRotation;
            transform.parent = null;
            relativeTransform.parent = transform;
        }

        private void LerpObjectToPosition(Action callBack)
        {
            transformTweenable.Initialize(transform, CurrentInteractor.AttachmentPoint);
            tweener.AddTweenable(transformTweenable);
            transformTweenable.OnTweenComplete += callBack;
        }

        protected override void DeSelected()
        {
            tweener.RemoveTweenable(transformTweenable);
            grabStrategy.UnGrab(this, CurrentInteractor);
        }
    }

}