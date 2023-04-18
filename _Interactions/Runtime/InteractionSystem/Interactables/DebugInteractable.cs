using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class DebugInteractable : InteractableBase
    {
        protected override void OnActivate()
        {
            Debug.Log("Activated");
        }

        protected override void OnAHoverStart()
        {
            Debug.Log("HoverStart");
        }

        protected override void OnAHoverEnd()
        {
            Debug.Log("HoverEnd");
        }

        protected override void OnSelected()
        {
            Debug.Log("Selected");
        }

        protected override void OnDeSelected()
        {
            Debug.Log("Deselected");
        }
    }
}