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
        [ReadOnly][SerializeField] Interactable currentInteractale;
        [ReadOnly][SerializeField] Collider currentCollider;
        [ReadOnly][SerializeField] bool interacting;
        private void Awake()
        {
            mapper = GetComponent<HandInputMapper>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (interacting)
            {
                return;
            }
            if (currentCollider != other)
            {
                CheckInteractablevalididty(other);
            }
        }
        private void CheckInteractablevalididty(Collider other)
        {
            var interactable = other.GetComponentInParent<Interactable>();
            if (interactable)
            {

                if (IsColliderCloserThanCurrent(other))
                {
                    currentCollider = other;
                    currentInteractale = interactable;
                }
            }
        }
        private bool IsColliderCloserThanCurrent(Collider collider)
        {
            if (!currentCollider)
            {
                return true;
            }
            var distanceTocurrentInteractor = (currentCollider.transform.position - this.transform.position).magnitude;
            var distanceToNewInteractor = (collider.transform.position - this.transform.position).magnitude;
            if (distanceTocurrentInteractor > distanceToNewInteractor)
            {
                
                return true;
            }
            return false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (interacting)
            {
                return;
            }
            if (currentCollider == other)
            {
                currentCollider = null;
                currentInteractale = null;
            }
        }

    }
}
