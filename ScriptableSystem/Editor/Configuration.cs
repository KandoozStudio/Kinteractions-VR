using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace Kandooz.ScriptableSystem
{
    [CreateAssetMenu()]
    public class Configuration : ScriptableObject
    {
        [HideInInspector] public string variables = "Assets/";
        [HideInInspector] public string events = "Assets/";
    }
    [CustomEditor(typeof(Configuration))]
    public class ConfigurationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PathChooser("variables");
            PathChooser("events");
            serializedObject.ApplyModifiedProperties();
        }

        private void PathChooser(string name)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
            if (GUILayout.Button("Choose"))
            {
                var path = serializedObject.FindProperty(name).stringValue;
                if (path.Length == 0)
                {
                    path = "Assets";
                }
                path = EditorUtility.OpenFolderPanel(name, serializedObject.FindProperty(name).stringValue, name);
                path = "Assets/"+path.Substring(Application.dataPath.Length);
                serializedObject.FindProperty(name).stringValue = path;
            }
            EditorGUILayout.EndHorizontal();

        }
    }
}