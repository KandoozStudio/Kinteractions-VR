using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Kandooz.Common;
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
        public Vector3 colliderPosition=Vector3.zero;
        public float collisionRadius=.02f;
        [HideInInspector]public HandConstrains defaultHandConstraints= HandConstrains.Free;
        
        [ReadOnly] [SerializeField] private bool interacting;

        private HandConstrains constraints = HandConstrains.Free;
        private Interactable interactable;
        private Collider currentCollider;
        private Collider[] hoveredObject;

        #region properties
        public HandConstrains Constraints { get => constraints;  }
        #endregion

        #region MonoBehaviour messages
        private void Start()
        {
            hoveredObject = new Collider[1];
            SetDefaultConstraints();
        }
        private void Update()
        {
            if (interacting)
            {
                if (interactable)
                {

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
                            if (interactable.interactionButton == GrabbingButton.Trigger)
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
                            if (interactable.interactionButton == GrabbingButton.Trigger)
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
            if (!interacting)
            {
                Physics.OverlapSphereNonAlloc(this.transform.position + colliderPosition, collisionRadius * this.transform.lossyScale.magnitude, hoveredObject);
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
                else
                {
                    if(interactable)
                        interactable.OnHandHoverEnd(this);
                    interactable = null;
                    currentCollider = null;
                }

            }
        }
        #endregion

        public void StartInteracting()
        {
            interacting = true;
            interactable.OnInteractionStart(this);
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
        public void StopInteracting()
        {
            interactable.OnInterActionEnd(this);
            SetDefaultConstraints();
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
            Gizmos.DrawWireSphere(this.transform.position + colliderPosition, collisionRadius * transform.lossyScale.magnitude);
        }


    }
}
