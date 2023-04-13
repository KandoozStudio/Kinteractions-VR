using System;
using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    [RequireComponent(typeof(Hand))]
    public abstract class InteractorBase : MonoBehaviour
    {
        [SerializeField] private Hand hand;
        [SerializeField] [ReadOnly] protected InteractableBase currentInteractable;
        private XRButtonObserver onInteractionStateChanged;
        private XRButtonObserver onActivate;
        private IDisposable interactionSubscriber, activationSubscriber;
        private Joint joint;

        public HandIdentifier Hand => hand.HandIdentifier;
        public Joint InteractorJoint => joint;

        private void Awake()
        {
            hand = GetComponent<Hand>();
            onInteractionStateChanged = new XRButtonObserver((state) =>
            {
                if (currentInteractable is null) return;
                switch (state)
                {
                    case ButtonState.Up:
                        if (currentInteractable.CurrentState == InteractionState.Selected)
                        {
                            OnDeSelect();
                        }

                        break;
                    case ButtonState.Down:
                        if (currentInteractable.CurrentState == InteractionState.Hovering)
                        {
                            OnSelect();
                        }

                        break;
                }
            }, null, null);
            onActivate = new XRButtonObserver((state) =>
            {
                if (currentInteractable is null) return;
                switch (state)
                {
                    case ButtonState.Down:
                        OnActivate();
                        break;
                }
            }, null, null);
        }

        protected void OnHoverStart()
        {
            if (currentInteractable == null) return;

            currentInteractable.OnStateChanged(InteractionState.Hovering, this);
            interactionSubscriber = currentInteractable.SelectionButton switch
            {
                XRButton.Grip => hand.OnGripButtonStateChange.Subscribe(onInteractionStateChanged),
                XRButton.Trigger => hand.OnTriggerTriggerButtonStateChange.Subscribe(onInteractionStateChanged),
                _ => interactionSubscriber
            };
        }

        protected void OnHoverEnd()
        {
            currentInteractable.OnStateChanged(InteractionState.None, this);
            interactionSubscriber?.Dispose();
            currentInteractable = null;
        }

        protected void OnSelect()
        {
            if (currentInteractable == null) return;
            currentInteractable.OnStateChanged(InteractionState.Selected, this);
            activationSubscriber = currentInteractable.SelectionButton switch
            {
                XRButton.Trigger => hand.OnGripButtonStateChange.Subscribe(onActivate),
                XRButton.Grip => hand.OnTriggerTriggerButtonStateChange.Subscribe(onActivate),
                _ => activationSubscriber
            };
        }

        private void OnDeSelect()
        {
            activationSubscriber?.Dispose();
            interactionSubscriber?.Dispose();
            OnHoverStart(); // this might need to change in the future
        }

        private void OnActivate()
        {
            if (!currentInteractable) return;

            currentInteractable.OnStateChanged(InteractionState.Activated, this);
        }

        public void ToggleJointObject(bool enable)
        {
            joint.gameObject.SetActive(enable);
        }
    }
}