using System;
using Kandooz.Interactions;
using Kandooz.InteractionSystem.Core;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz.InteractionSystem.Interactions
{
    [Flags]
    public enum InteractionHand
    {
        Left = 1,
        Right = 2,
    }

    [RequireComponent(typeof(InteractionPoseConstrainer))]
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private InteractionHand interactionHand = (InteractionHand.Left | InteractionHand.Right);
        [SerializeField] private XRButton selectionButton;
        [SerializeField] private InteractorUnityEvent onSelected;
        [SerializeField] private InteractorUnityEvent onDeselected;
        [SerializeField] private InteractorUnityEvent onHoverStart;
        [SerializeField] private InteractorUnityEvent onHoverEnd;
        [SerializeField] private InteractorUnityEvent onActivated;
        [ReadOnly] private bool isSelected;

        private InteractionPoseConstrainer poseConstrainter;
        private InteractionState currentState;
        private InteractorBase currentInteractor;
        public bool IsSelected => isSelected;
        public IObservable<InteractorBase> OnSelected => onSelected.AsObservable();
        public IObservable<InteractorBase> OnDeselected => onDeselected.AsObservable();
        public IObservable<InteractorBase> OnHoverStarted => onHoverStart.AsObservable();
        public IObservable<InteractorBase> OnHoverEnded => onHoverEnd.AsObservable();
        public IObservable<InteractorBase> OnActivated => onActivated.AsObservable();
        public Transform RightHandRelativePosition => InteractionConstrainter.RightHandTransform;
        public Transform LeftHandRelativePosition => InteractionConstrainter.LeftHandTransform;
        public XRButton SelectionButton => selectionButton;
        public InteractionState CurrentState => currentState;
        protected InteractorBase CurrentInteractor => currentInteractor;


        protected InteractionPoseConstrainer InteractionConstrainter
        {
            get
            {
                poseConstrainter ??= GetComponent<InteractionPoseConstrainer>();
                return poseConstrainter;
            }
        }

        public void OnStateChanged(InteractionState state, InteractorBase interactor)
        {
            if (this.currentState == state) return;
            currentInteractor = interactor;
            if (state == InteractionState.None)
                HandleNoneState();
            else if (state == InteractionState.Hovering)
                HandleHoverState();
            else if (state == InteractionState.Selected)
                HandleSelectionState();
            else if (state == InteractionState.Activated)
                HandleActiveState();
        }

        private void HandleNoneState()
        {
            if (currentState == InteractionState.Selected)
            {
                DeSelected();
                isSelected = false;
                onDeselected.Invoke(currentInteractor);
            }

            else if (currentState == InteractionState.Hovering)
            {
                EndHover();
                onHoverEnd.Invoke(currentInteractor);
            }

            currentState = InteractionState.None;
            currentInteractor = null;
        }

        private void HandleHoverState()
        {
            if (currentState == InteractionState.Selected)
            {
                isSelected = false;
                onDeselected.Invoke(currentInteractor);
                DeSelected();
            }

            currentState = InteractionState.Hovering;
            StartHover();
            onHoverStart.Invoke(currentInteractor);
        }

        private void HandleSelectionState()
        {
            if (currentState == InteractionState.Hovering)
            {
                onHoverEnd.Invoke(currentInteractor);
                EndHover();
            }

            isSelected = true;
            onSelected.Invoke(currentInteractor);
            Select();
            currentState = InteractionState.Selected;
        }

        private void HandleActiveState()
        {
            if (this.currentState == InteractionState.Selected)
            {
                onActivated.Invoke(currentInteractor);
                Activate();
            }
        }

        protected abstract void Activate();
        protected abstract void StartHover();
        protected abstract void EndHover();
        protected abstract void Select();
        protected abstract void DeSelected();

        public bool IsValidHand(HandIdentifier hand)
        {
            var handID = (int)hand;
            var valid = (int)interactionHand;
            return (valid & handID) != 0;
        }
    }
}