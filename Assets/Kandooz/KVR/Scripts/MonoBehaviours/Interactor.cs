using Kandooz.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandInputMapper))]
    [RequireComponent(typeof(Collider))]
    public class Interactor : MonoBehaviour
    {
        HandInputMapper mapper;
        [ReadOnly] [SerializeField] Interactable currentInteractale;
        [ReadOnly] [SerializeField] Collider currentCollider;
        [ReadOnly] [SerializeField] bool interacting;
        [ReadOnly] [SerializeField] List<Collider> availableColliders;
        public HandInputMapper Mapper { get => mapper; }
        public void StartInteraction()
        {
            interacting = true;
        }
        public void EndInteraction()
        {
            interacting = false;
            DeselectCurrentInteractable();
        }

        private void Awake()
        {
            mapper = GetComponent<HandInputMapper>();
            availableColliders = new List<Collider>(); ;
        }
        private void Update()
        {
            if (interacting)
            {
                return;
            }
            if (currentInteractale)
            {
                currentInteractale.OnHover(this);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (interacting)
            {
                return;
            }
            var interactable = other.GetComponent<Interactable>();
            if (interactable)
            {
                ChangeCurrentCollider(other, interactable);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (interacting)
            {
                return;
            }
            if (currentCollider == other)
            {
                DeselectCurrentInteractable();
                SelectNextColliderInList();
            }
            RemoveColliderFromList(other);
        }
        private void ChangeCurrentCollider(Collider other, Interactable interactable)
        {
            if (ShouldChangeCurrentCollider(other))
            {
                DeselectCurrentInteractable();
                SelectInteractable(other, interactable);

            }
            else
            {
                availableColliders.Add(other);

            }
        }
        private bool ShouldChangeCurrentCollider(Collider collider)
        {
            if (!currentCollider)
            {
                return true;
            }
            var currentDistance = (currentCollider.transform.position - this.transform.position).magnitude;
            var newDistance = (collider.transform.position - this.transform.position).magnitude;
            return currentDistance > newDistance;
            
        }
        private void SelectInteractable(Collider collider, Interactable interactable)
        {
            currentCollider = collider;
            currentInteractale = interactable;
            currentInteractale.OnHoverStart(this);
        }
        private void DeselectCurrentInteractable()
        {
            if (currentInteractale)
            {
                currentInteractale.OnHoverEnd(this);
            }
            currentCollider = null;
            currentInteractale = null;
        }
        private void SelectNextColliderInList()
        {
            if (availableColliders.Count > 0)
            {
                var collider = availableColliders[availableColliders.Count - 1];
                var interactable = collider.GetComponent<Interactable>();
                SelectInteractable(collider, interactable);
                availableColliders.RemoveAt(availableColliders.Count - 1);
            }
        }
        private void RemoveColliderFromList(Collider collider)
        {
            availableColliders.Remove(collider);
        }
    }
}
