using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace Kandooz.ScriptableSystem.Editors
{
    [CustomPropertyDrawer(typeof(IScriptableVariable), true)]

    public class ScriptableVariableDrawer : PropertyDrawer
    {
        string path = "";
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (path == "")
            {
                var assets = AssetDatabase.FindAssets("t:Configuration");
                var config = AssetDatabase.LoadAssetAtPath<Configuration>(AssetDatabase.GUIDToAssetPath(assets[0]));
                path = config.variables;
            }
            var content = EditorGUI.BeginProperty(position, label, property);
            var variableReffrence = (IScriptableVariable)property.objectReferenceValue;

            if (variableReffrence == null)
            {
                DrawNullDrawer(position, property, path);
            }
            else
            {
                DrawEditableDrawer(position, property);
            }
            EditorGUI.EndProperty();
        }
        private static void DrawEditableDrawer(Rect position, SerializedProperty property)
        {
            var width = position.width;

            position.width = width * .6f;
            var serializedObject = new SerializedObject(property.objectReferenceValue);
            EditorGUI.PropertyField(position, serializedObject.FindProperty("value"), new GUIContent(property.name));
            serializedObject.ApplyModifiedProperties();
            position.x += width * .62f;
            position.width = width * .38f;
            EditorGUI.PropertyField(position, property,new GUIContent(""));
        }

        public static void DrawNullDrawer(Rect position, SerializedProperty property, string path)
        {
            var width = position.width;
            position.width = width * 4 / 5;
            EditorGUI.PropertyField(position, property);
            position.x += width * 4 / 5;
            position.width = width / 5;
            if (GUI.Button(position, "Assign"))
            {
                var type = property.type.Substring(6);
                type = type.Substring(0, type.Length - 1);
                var assetsGUID = AssetDatabase.FindAssets("t:" + type + " " + property.name);
                if (assetsGUID.Length == 0)
                {
                    DisplayCreationDialoge(property, type, path);
                }
                else
                {
                    AssignVariable(property, assetsGUID);
                }
            }
        }
        private static void AssignVariable(SerializedProperty property, string[] assetsGUID)
        {
            var path = AssetDatabase.GUIDToAssetPath(assetsGUID[0]);
            var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            property.objectReferenceValue = obj;
        }
        private static void DisplayCreationDialoge(SerializedProperty property, string type, string path)
        {
            var directory = EditorPrefs.GetString("variablePath");
            // Todo can't find asset Do something here 
            var answer = EditorUtility.DisplayDialog(
                "Can't find variable",
                $"can't find a variable with the name: {property.name} do you want to create it in the default directory: {path}",
                "Yes",
                "No");

            if (answer)
            {
                var instance = ScriptableObject.CreateInstance(type);
                AssetDatabase.CreateAsset(instance, path + "/" + property.name + ".asset");
                property.objectReferenceValue = instance;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 1;
        }
    }
}