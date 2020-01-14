using UnityEngine;
using UnityEditor;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        private Interactable interactable;
        private void OnEnable()
        {
            interactable = (Interactable)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}