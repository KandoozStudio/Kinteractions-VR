using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Kandooz.Common;
using UnityEngine.Assertions;

public enum HandType
{
    right, left
}

namespace Kandooz.KVR
{

    public class Hand : MonoBehaviour
    {
        public HandType hand;
        public VRInputManager inputManager;
        public Vector3 colliderPosition = Vector3.zero;
        public float collisionRadius = .1f;
        public LayerMask interactingLayers=255;
        [HideInInspector]public HandConstrains defaultHandConstraints = HandConstrains.Free;

        [ReadOnly] [SerializeField] private bool interacting;

        private HandConstrains constraints = HandConstrains.Free;
        private Interactable interactable;
        private Collider currentCollider;
        private Collider[] hoveredObject;
        private Vector3 center;
        #region properties
        public HandConstrains Constraints { get => constraints; }
        #endregion

        #region MonoBehaviour messages
        private void Start()
        {
            hoveredObject = new Collider[1];
            SetDefaultConstraints();
        }
        private void Update()
        {
            center = this.transform.TransformPoint(colliderPosition);
            if (interacting)
            {
                if (interactable)
                {
                    switch (hand)
                    {
                        case HandType.right:
                            if (interactable.interactionButton == GrabbingButton.Grip)
                            {
                                if (!inputManager.RightGripDown)
                                {
                                    StopInteracting();
                                }
                            }
                            else if (interactable.interactionButton == GrabbingButton.Trigger)
                            {
                                if (!inputManager.RightTriggerDown)
                                {
                                    StopInteracting();
                                }
                            }
                            break;
                        case HandType.left:
                            if (interactable.interactionButton == GrabbingButton.Grip)
                            {
                                if (!inputManager.LeftGripDown)
                                {
                                    StopInteracting();

                                }
                            }
                            else if (interactable.interactionButton == GrabbingButton.Trigger)
                            {
                                if (!inputManager.LeftTriggerDown)
                                {
                                    StopInteracting();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (interactable)
                {

                    switch (hand)
                    {
                        case HandType.right:
                            if (interactable.interactionButton == GrabbingButton.Grip)
                            {
                                if (inputManager.RightGripDown)
                                {
                                    StartInteracting();
                                }
                            }
                            else if (interactable.interactionButton == GrabbingButton.Trigger)
                            {
                                if (inputManager.RightTriggerDown)
                                {
                                    StartInteracting();
                                }
                            }
                            break;
                        case HandType.left:
                            if (interactable.interactionButton == GrabbingButton.Grip)
                            {
                                if (inputManager.LeftGripDown)
                                {
                                    StartInteracting();

                                }
                            }
                            else if (interactable.interactionButton == GrabbingButton.Trigger)
                            {
                                if (inputManager.LeftTriggerDown)
                                {
                                    StartInteracting();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            //if (!interacting) 
            //{
                hoveredObject[0] = null;
                Physics.OverlapSphereNonAlloc(center, collisionRadius * this.transform.lossyScale.magnitude, hoveredObject, interactingLayers);
                if (hoveredObject[0])
                {
                    if (currentCollider != hoveredObject[0])
                    {
                        if (interactable)
                        {
                            interactable.OnHandHoverEnd(this);
                        }
                        interactable = hoveredObject[0].GetComponent<Interactable>();
                        if (interactable)
                        {
                            interactable.OnHandHoverStart(this);
                        }
                        currentCollider = hoveredObject[0];
                    }
                }
            //}
            
            else
            {
                if (interactable)
                {
                    interactable.OnHandHoverEnd(this);
                    SetDefaultConstraints();
                }
                interactable = null;
                currentCollider = null;
            }


        }
        #endregion
        public void StartInteracting()
        {
            if (interactable.OnInteractionStart(this))
            {
                interacting = true;
                switch (hand)
                {
                    case HandType.right:
                        SetHandConstraints(interactable.rightHandLimits);
                        break;
                    case HandType.left:
                        SetHandConstraints(interactable.leftHandLimits);
                        break;
                    default:
                        break;
                }
            }
        }
        public void StopInteracting()
        {
            Assert.IsNotNull(interactable, "function called with null interactable");
            SetDefaultConstraints();
            interacting = false;
            currentCollider = null;
            interactable.OnInterActionEnd(this);
            interactable = null;
        }
        public void SetHandConstraints(HandConstrains constraints)
        {
            this.constraints = constraints;
        }
        public void SetDefaultConstraints()
        {
            this.constraints = this.defaultHandConstraints;
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            center = this.transform.TransformPoint(colliderPosition);

            Gizmos.DrawWireSphere(center, collisionRadius * transform.lossyScale.magnitude);
        }
    }
}
