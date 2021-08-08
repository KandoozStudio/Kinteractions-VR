using Kandooz.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandInputMapper))]
    public class Interactor : MonoBehaviour
    {
        HandInputMapper mapper;
        [ReadOnly] Interactable currentInteractale;
        [ReadOnly] Collider currentCollider;
        [ReadOnly] bool interacting;

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
                currentCollider = other;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (interacting)
            {
                return;
            }
            if (currentCollider==other)
            {
                currentCollider = null;
            }
        }

    }
}
