using UnityEngine;
using UnityEditor;
namespace Kandooz.KVR
{

    enum HandtoEdit
    {
        none, right, left
    }
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        HandAnimationController visibleHand;

        HandtoEdit currentHand = HandtoEdit.none;
        private Interactable interactable;
        

        private void OnEnable()
        {
            interactable = (Interactable)target;
            EditorApplication.update += Update;

        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;

            if (visibleHand)
                GameObject.DestroyImmediate(visibleHand.gameObject);
            currentHand = HandtoEdit.none;
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
            if (currentHand != HandtoEdit.none)
            {

                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical("GroupBox");
                switch (currentHand)
                {
                    case HandtoEdit.none:
                        break;
                    case HandtoEdit.right:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandLimits"));
                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandPivot").FindPropertyRelative("position"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandPivot").FindPropertyRelative("rotation"));
                        break;
                    case HandtoEdit.left:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandLimits"));
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandPivot").FindPropertyRelative("position"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandPivot").FindPropertyRelative("rotation"));
                        break;
                    default:
                        break;
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
                HandPositionSceneEditor(currentHand);
            }
            else
            {
                if (visibleHand) GameObject.DestroyImmediate(visibleHand.gameObject);
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void HandPositionSceneEditor(HandtoEdit hand)
        {
            if (visibleHand == null)
            {
                switch (hand)
                {
                    case HandtoEdit.none:
                        break;
                    case HandtoEdit.right:
                        visibleHand = GameObject.Instantiate<HandAnimationController>(interactable.hand.rightHandPrefab);
                        break;
                    case HandtoEdit.left:
                        visibleHand = GameObject.Instantiate<HandAnimationController>(interactable.hand.leftHandPrefab);
                      break;
                    default:
                        break;
                }
            }
            visibleHand.transform.parent = interactable.transform;

            switch (hand)
            {
                case HandtoEdit.none:
                    break;
                case HandtoEdit.right:
                    visibleHand.transform.localPosition = interactable.rightHandPivot.position;
                    visibleHand.transform.localRotation = Quaternion.Euler(interactable.rightHandPivot.rotation);

                    break;
                case HandtoEdit.left:
                    visibleHand.transform.localPosition = interactable.leftHandPivot.position;
                    visibleHand.transform.localRotation = Quaternion.Euler(interactable.leftHandPivot.rotation);
                    break;
                default:
                    break;
            }

        }


        float t = 0;
        void Update()
        {
            if (t > 1)
            {
                t = 0;
            }
            t += .01f;
            if (visibleHand)
            {
                if (!EditorApplication.isPlaying)
                {
                    if (visibleHand.graph.IsValid())
                    {

                        visibleHand.Update();
                    }
                    else
                    {
                        visibleHand.Init();
                    }

                    var constraints = (currentHand == HandtoEdit.left) ? interactable.leftHandLimits : interactable.rightHandLimits;

                    visibleHand[FingerName.Index] = Mathf.Lerp(constraints.indexFingerLimits.x, constraints.indexFingerLimits.y,t);
                    visibleHand[FingerName.Middle] = Mathf.Lerp(constraints.middleFingerLimits.x, constraints.middleFingerLimits.y, t);
                    visibleHand[FingerName.Ring] = Mathf.Lerp(constraints.ringFingerLimits.x, constraints.ringFingerLimits.y, t);
                    visibleHand[FingerName.Pinky] = Mathf.Lerp(constraints.pinkyFingerLimits.x, constraints.pinkyFingerLimits.y, t);
                    visibleHand[FingerName.Thumb] = Mathf.Lerp(constraints.thumbFingerLimits.x, constraints.thumbFingerLimits.y, t);
                }
            }
        }
    }
}