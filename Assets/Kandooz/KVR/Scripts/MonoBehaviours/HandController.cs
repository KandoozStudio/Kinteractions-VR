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
            defaultControlStrategy=handControllerStrategy = new NormalHandControllerStrategy();
        }
        public void ResetHandControllerStrategy()
        {
            handControllerStrategy = defaultControlStrategy;
        }
        public void SetHandControllerStrategy(IHandControlerStrategy strategy)
        {
            if (strategy != null)
            {
                handControllerStrategy = strategy;
            }
        }
        private void Update()
        {
            handControllerStrategy.UpdateHand(animationController,inputManager,type);
        }
    }
}
