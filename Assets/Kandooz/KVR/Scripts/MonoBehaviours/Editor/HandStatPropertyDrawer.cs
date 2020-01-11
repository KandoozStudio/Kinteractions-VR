using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Kandooz.KVR
{
    [CustomPropertyDrawer(typeof(HandState))]
    public class HandStatPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var amountField = new PropertyField(property.FindPropertyRelative("staticPose"));
            container.Add(amountField);

            return container;
        }
    }

}
