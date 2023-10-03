using UniRx;
using UnityEngine;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEventListener))]
    public class AnimationStep : MonoBehaviour
    {
        [SerializeField] private string animationTriggerName;
        [SerializeField] private Animator _animator;
        private StepEventListener listener;
        void Awake()
        {
             listener = GetComponent<StepEventListener>();
            _animator = GetComponent<Animator>();
            listener.OnStarted.Do(_ => _animator.SetTrigger(animationTriggerName)).Subscribe().AddTo(this);
        }
        /// <summary>
        /// this function must be called from the animation 
        /// </summary>
        public void AnimationEnded()
        {
            listener.step.OnActionCompleted();;
        }
    }
}