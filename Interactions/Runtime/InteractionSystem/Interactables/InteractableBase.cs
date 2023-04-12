using UnityEngine;

namespace Kandooz.Interactions.Runtime
{
    public abstract class InteractableBase : MonoBehaviour
    {
        // TODO use a state machine
        private InteractionState currentState;
        private InteractorBase currentInteractor;
        [SerializeField] private XRButton selectionButton;
        [SerializeField] private InteractorUnityEvent onSelected;
        [SerializeField] private InteractorUnityEvent onDeselected;
        [SerializeField] private InteractorUnityEvent onHoverStart;
        [SerializeField] private InteractorUnityEvent onHoverEnd;
        [SerializeField] private InteractorUnityEvent onActivated;

        public XRButton SelectionButton => selectionButton;
        public InteractionState CurrentState => currentState;
        protected InteractorBase CurrentInteractor => currentInteractor;

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
                OnDeSelected();
                onDeselected.Invoke(currentInteractor);
            }

            else if (currentState == InteractionState.Hovering)
            {
                OnAHoverEnd();
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
                OnDeSelected();
            }
            currentState = InteractionState.Hovering;
            OnAHoverStart();
            onHoverStart.Invoke(currentInteractor);
        }

        private void HandleSelectionState()
        {
            if (currentState == InteractionState.Hovering)
            {
                onHoverEnd.Invoke(currentInteractor);
                OnAHoverEnd();
            }
            onSelected.Invoke(currentInteractor);
            OnSelected();
            currentState = InteractionState.Selected;
        }

        private void HandleActiveState()
        {
            if (this.currentState == InteractionState.Selected)
            {
                onActivated.Invoke(currentInteractor);
                OnActivate();
            }
        }

        protected abstract void OnActivate();
        protected abstract void OnAHoverStart();
        protected abstract void OnAHoverEnd();
        protected abstract void OnSelected();
        protected abstract void OnDeSelected();

    }
}