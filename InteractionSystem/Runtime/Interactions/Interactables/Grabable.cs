using System;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class Grabable : InteractableBase
    {
        [SerializeField] private VariableTweener tweener;

        private TransformTweenable transformTweenable= new();
        private GrabStrategy grabStrategy;
        private void Awake()
        {
            tweener ??= GetComponent<VariableTweener>();
            if (!tweener)
            {
                tweener = gameObject.AddComponent<VariableTweener>();
                tweener.tweenScale = 10;
            }
            
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
            grabStrategy.Initialize(CurrentInteractor);
            InitializeAttachmentPointTransform();
            LerpObjectToPosition(() => grabStrategy.Grab(this, CurrentInteractor));
        }

        private void InitializeAttachmentPointTransform()
        {
            var relativeTransform = CurrentInteractor.HandIdentifier == HandIdentifier.Left ? LeftHandRelativePosition : RightHandRelativePosition;
            relativeTransform.parent = null;
            transform.parent = relativeTransform;
            CurrentInteractor.AttachmentPoint.localPosition = transform.localPosition;
            CurrentInteractor.AttachmentPoint.localRotation = transform.localRotation;
            transform.parent = null;
            relativeTransform.parent = InteractionConstrainter.PivotParent;
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