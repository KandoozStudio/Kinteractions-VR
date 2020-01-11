using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandTracker))]
    public class HandController : MonoBehaviour
    {
        /// <summary>
        /// the input manager is to get input from the diffrent buttons on the controllers
        /// </summary>
        public VRInputManager inputManager;
        private HandType type;
        private HandAnimationController animationController;
        private Vector3 shift;
        private float radius=1;
        private IHandControlerStrategy handControllerStrategy;
        /// <summary>
        /// Mainly for reducing garbage allocation
        /// </summary>
        private IHandControlerStrategy defaultControlStrategy;

        private new Collider []collider;
        private void Start()
        {
            type = GetComponent<HandTracker>().hand;
            collider = new Collider[1];
            animationController=GetComponentInChildren<HandAnimationController>();
            defaultControlStrategy=handControllerStrategy = new InputDeviceHandController(animationController, inputManager, type);
        }
        private void Update()
        {
            handControllerStrategy.UpdateHand();
        }

        private void OnObjectGrabbed(Interactable interactable)
        {

        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position + shift, radius);
        }
    }
}
