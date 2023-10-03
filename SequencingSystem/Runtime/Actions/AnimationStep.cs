using UniRx;
using UnityEngine;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEvenListener))]
    public class AnimationStep : MonoBehaviour
    {
        [SerializeField] private string animationTriggerName;
        [SerializeField] private Animator _animator;
        private StepEvenListener listener;
        void Awake()
        {
             listener = GetComponent<StepEvenListener>();
            _animator = GetComponent<Animator>();
            listener.OnStarted.Do(_ => _animator.SetTrigger(animationTriggerName)).Subscribe().AddTo(this);
        }
        /// <summary>
        /// this function must be called from the animation 
        /// </summary>
        public void AnimationEnded()
        {
            listener.OnActionCompleted();;
        }
    }
}