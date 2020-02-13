using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandTracker))]
    public class HandController : MonoBehaviour
    {
        public VRInputManager inputManager;
        private HandType type;
        private HandAnimationController animationController;
        private Vector3 shift;
        private float radius=1;

        private Collider []collider;
        private void Start()
        {
            type = GetComponent<HandTracker>().type;
            collider = new Collider[1];
            animationController=GetComponentInChildren<HandAnimationController>();
        }
        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                animationController[i]=inputManager.GetFingerValue(type, (FingerName)i);
            }
            // Physics.OverlapSphereNonAlloc(this.transform.position + shift, radius, collider);
            // if (collider[0])
            // {
            //     var interactable = collider[0].GetComponentInParent<Interactable>();
            //     interactable.OnHandHover(this);
            // }
        }
        void OnDrawGizmosSelected()
        {

        }
    }
}
