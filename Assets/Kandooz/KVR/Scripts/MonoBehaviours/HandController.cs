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
            type = GetComponent<HandTracker>().hand;
            collider = new Collider[1];
            animationController=GetComponentInChildren<HandAnimationController>();
        }
        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                animationController[i]=inputManager.GetFingerValue(type, (FingerName)i);
            }
            
        }
        void OnDrawGizmosSelected()
        {

        }
    }
}
