using System;
using Kandooz.Interactions;
using Kandooz.InteractionSystem.Core;
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

        private InteractionPoseConstrainer poseConstrainter;
        private InteractionState currentState;
        private InteractorBase currentInteractor;

        public event UnityAction<InteractorBase> OnSelected
        {
            add => onSelected.AddListener(value);
            remove => onSelected.RemoveListener(value);
        }

        public event UnityAction<InteractorBase> OnDeselected
        {
            add => onDeselected.AddListener(value);
            remove => onDeselected.RemoveListener(value);
        }

        public event UnityAction<InteractorBase> OnHoverStarted
        {
            add => onHoverStart.AddListener(value);
            remove => onHoverStart.RemoveListener(value);
        }

        public event UnityAction<InteractorBase> OnHoverEnded
        {
            add => onHoverEnd.AddListener(value);
            remove => onHoverEnd.RemoveListener(value);
        }

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
    }
}