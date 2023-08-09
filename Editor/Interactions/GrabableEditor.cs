using Kandooz.InteractionSystem.Core;
using Kandooz.InteractionSystem.Interactions;
using UnityEditor;

namespace Kinteractions_VR.Interactions
{
    [CustomEditor(typeof(Config))]
    public class GrabableEditor: Editor
    {
        private Grabable grabable;

        private void OnEnable()
        {
            grabable = (Grabable)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}