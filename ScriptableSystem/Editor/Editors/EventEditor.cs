using UnityEngine;
using UnityEditor;
namespace Kandooz.ScriptableSystem.Editors
{
    [CustomEditor(typeof(ScriptableEvent))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (Application.isPlaying)
            {
                CreateInvokeButton();
            }
        }
        private void CreateInvokeButton()
        {
            if (GUILayout.Button("Raise"))
            {
                ((ScriptableEvent)this.target).Invoke();
            }
        }
    }
    [CustomPropertyDrawer(typeof(ScriptableEvent))]
    public class EventDrawer : PropertyDrawer
    {
        string path="";
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (path == "")
            {
                var assets = AssetDatabase.FindAssets("t:Configuration");
                var config = AssetDatabase.LoadAssetAtPath<Configuration>(AssetDatabase.GUIDToAssetPath(assets[0]));
                path = config.events;
            }
            if (property.objectReferenceValue == null)
            {
                ScriptableVariableDrawer.DrawNullDrawer(position, property, path);
            }
            else
            {
                EditorGUI.PropertyField(position, property);
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 1;
        }
    }
}