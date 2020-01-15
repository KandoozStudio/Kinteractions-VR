using UnityEngine;
using UnityEditor;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        enum HandtoEdit {
            none,right,left
        }
        HandtoEdit currentHand = HandtoEdit.none;
        private Interactable interactable;
        private void OnEnable()
        {
            interactable = (Interactable)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Edit Right hand"))
            {
                if (currentHand == HandtoEdit.right)
                {
                    currentHand = HandtoEdit.none;
                }
                else
                {
                    currentHand = HandtoEdit.right;
                }
            }

            if (GUILayout.Button("Edit Left hand"))
            {
                if (currentHand == HandtoEdit.left)
                {
                    currentHand = HandtoEdit.none;
                }
                else
                {
                    currentHand = HandtoEdit.left;
                }
            }
            EditorGUILayout.EndHorizontal();


            switch (currentHand)
            {
                case HandtoEdit.none:
                    break;
                case HandtoEdit.right:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandLimits"));
                    break;
                case HandtoEdit.left:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandLimits"));

                    break;
                default:
                    break;
            }

        }
    }
}