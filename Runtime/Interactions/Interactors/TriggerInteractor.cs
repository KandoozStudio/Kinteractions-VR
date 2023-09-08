using System;
using System.Collections.Generic;
using Kandooz.Interactions;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class TriggerInteractor : InteractorBase
    {
        [ReadOnly][SerializeField] private Collider currentCollider;
        //private LinkedList<InteractableBase> availableInteractables;
        private HashSet<InteractableBase> interactables = new(10);
        private int frameCounter;
        private void OnTriggerEnter(Collider other)
        {
            if (other == currentCollider || isInteracting) return;
            var interactable = other.GetComponentInParent<InteractableBase>();
            if (!interactable || interactables.Contains(interactable)) return;
            interactables.Add(interactable);
            if (!ShouldChangeInteractable( interactable)) return;
            ChangeInteractable(other, interactable);
        }

        private void ChangeInteractable(Collider other, InteractableBase interactable)
        {
            currentInteractable = interactable;
            currentCollider = other;
            OnHoverStart();
        }

        private bool ShouldChangeInteractable(InteractableBase interactable )
        {
            if (currentInteractable == null) return true;
            var interactableDistance = (transform.position - interactable.transform.position).sqrMagnitude;
            var currentInteractableDistance = (transform.position - currentInteractable.transform.position).sqrMagnitude;
            return currentInteractableDistance > interactableDistance;
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponent<InteractableBase>();
            if (interactables.Contains(interactable)) interactables.Remove(interactable);
            
            if (currentInteractable != null && currentInteractable.CurrentState == InteractionState.Selected) return;
            if (currentCollider == other)
            {
                currentCollider = null;
                OnHoverEnd();
            }
        }

        private void Update()
        {
            frameCounter++;
            if (isInteracting) return;
            if (frameCounter < 5) return;
            frameCounter = 0;
            var position = transform.position;
            var distanceToCurrent = (currentInteractable.transform.position - position).sqrMagnitude;
            foreach (var interactable in interactables)
            {
                var interactableDistance = (interactable.transform.position - position).sqrMagnitude;
                if (interactableDistance < distanceToCurrent) ChangeInteractable(interactable.GetComponentInChildren<Collider>(), interactable);
            }
            
        }
    }
}