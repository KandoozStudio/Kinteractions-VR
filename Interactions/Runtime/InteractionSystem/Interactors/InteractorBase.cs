using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kandooz.Interactions.Runtime
{
    [RequireComponent(typeof(Hand))]
    public abstract class InteractorBase : MonoBehaviour
    {
        [SerializeField] private Hand hand;
        [SerializeField][ReadOnly] protected InteractableBase currentInteractable;
        private XRButtonObserver onInteractionStateChanged;
        private XRButtonObserver onActivate;
        private IDisposable interactionSubscriber, activationSubscriber;
        public HandIdentifier Hand => hand.HandIdentifier;

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
            currentInteractable?.OnStateChanged(InteractionState.Activated, this);
        }
    }

    public class XRButtonObserver : IObserver<ButtonState>
    {
        private readonly Action onComplete;
        private readonly Action<Exception> onExceptionRaised;
        private readonly Action<ButtonState> onButtonStateChanged;

        public void OnCompleted() => onComplete();
        public void OnError(Exception error) => onExceptionRaised(error);
        public void OnNext(ButtonState buttonState) => onButtonStateChanged(buttonState);

        public XRButtonObserver(Action<ButtonState> onButtonStateChanged, Action onComplete, Action<Exception> onExceptionRaised)
        {
            this.onComplete = onComplete;
            this.onExceptionRaised = onExceptionRaised;
            this.onButtonStateChanged = onButtonStateChanged;
        }
    }
}