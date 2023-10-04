using System;
using Kandooz.InteractionSystem.Interactions;
using UnityEngine;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEvenListener))]
    public class InsertionAction : MonoBehaviour
    {
        [SerializeField] private InteractableBase interactable;
        private GameObject interactableObject;
        private bool insideTrigger=false;
        private StepEvenListener listner;
        private void Awake()
        {
            listner = GetComponent<StepEvenListener>();
            interactableObject = interactable.gameObject;
            interactable.OnDeselected += OnSelectionEnded;

        }

        private void OnSelectionEnded(InteractorBase interactor)
        {
            if(!listner.Current || !insideTrigger) return;
            interactableObject.transform.position = this.transform.position;
            interactableObject.transform.rotation = this.transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == interactableObject)
            {
                insideTrigger = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == interactableObject)
            {
                insideTrigger = false;
            }
        }
    }
}