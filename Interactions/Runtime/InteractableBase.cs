using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz.Interactions.Runtime
{
    public abstract class InteractableBase
    {
        private InteractionState currentState;
        private InteractorBase currentInteractor;
        public InteractorBase InteractorBase
        {
            set => currentInteractor = value;
        }

        public InteractionState CurrentState => currentState;

        public void OnStateChanged(InteractionState state)
        {
            if (this.currentState == state) return;
            if (state == InteractionState.None)
                HandleNoneState();
            else if (state == InteractionState.Hover)
                HandleHoverState();
            else if (state == InteractionState.Select)
                HandleSelectionState();
            else if (state == InteractionState.Activate)
                HandleActiveState();
        }

        private void HandleNoneState()
        {
            if (currentState == InteractionState.Select)
            {
                OnDeSelected();
            }

            else if (currentState == InteractionState.Hover)
            {
                OnAHoverEnd();
            }

            this.currentState = InteractionState.None;
        }

        private void HandleHoverState()
        {
            if (currentState == InteractionState.Select)
            {
                OnDeSelected();
            }
            OnAHoverStart();
            currentState = InteractionState.Hover;
        }

        private void HandleSelectionState()
        {
            if (currentState == InteractionState.Hover)
            {
                OnAHoverEnd();
            }
            OnSelected();
            currentState = InteractionState.Select;

        }

        private void HandleActiveState()
        {
            if (this.currentState == InteractionState.Select)
            {
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