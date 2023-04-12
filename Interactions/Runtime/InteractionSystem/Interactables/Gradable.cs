using System;

namespace Kandooz.Interactions.Runtime
{
    public class Gradable :InteractableBase
    {
        private IGrabStrategy grabStrategy;
        
        private void Awake()
        {
            
        }

        protected override void OnActivate()
        {
            
        }

        protected override void OnAHoverStart()
        {
        }

        protected override void OnAHoverEnd()
        {
        }

        protected override void OnSelected()
        {
            grabStrategy.Grab(this, CurrentInteractor);
        }

        protected override void OnDeSelected()
        {
            grabStrategy.UnGrab(this, CurrentInteractor);
        }
    }
}