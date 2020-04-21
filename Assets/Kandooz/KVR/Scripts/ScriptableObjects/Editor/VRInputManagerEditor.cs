using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR.Editors
{
    [CustomEditor(typeof(Kandooz.KVR.VRInputManager))]
    public class VRInputManagerEditor : Editor
    {
        internal class InputAxis
        {
            public string name = "";
            public string descriptiveName = "";
            public string descriptiveNegativeName = "";
            public string negativeButton = "";
            public string positiveButton = "";
            public string altNegativeButton = "";
            public string altPositiveButton = "";
            public float gravity = 0.0f;
            public float dead = 0.001f;
            public float sensitivity = 1.0f;
            public bool snap = false;
            public bool invert = false;
            public int type = 0;
            public int axis = 0;
            public int joyNum = 0;
        }

        //
        // NB: ALL AXIS VALUES WILL BE -1'd DURING PROCESSING, SO USE THE "REAL" AXIS VALUE
        //

        VRInputManager manager;
        private void OnEnable()
        {
            manager = (VRInputManager)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            switch (manager.type)
            {
                case ControllerType.Normal:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftTrigger"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftGrip"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftThumb"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightTrigger"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightGrip"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightThumb"));
                    break;
                case ControllerType.Knuckles:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftIndex"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftMiddle"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftRing"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftPinky"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("leftThumb"));

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightIndex"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightMiddle"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightRing"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightPinky"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("rightThumb"));

                    break;
                default:
                    break;
            }

            if(GUILayout.Button("Seed Axis"))
            {
                SeedInputs(manager);
            }
        }
        public static void SeedInputs(VRInputManager manager)
        {

            var axisList = new List<InputAxis>();
            var inputManagerAsset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            var serializedObject = new UnityEditor.SerializedObject(inputManagerAsset);
            var inputManagerCurrentData = serializedObject.FindProperty("m_Axes");

            /// this part is done in the most stuoid way possible but I couldn;t figure another way of doing it
            #region populating input 
            List<InputAxis> inputAxes = new List<InputAxis>();
            switch (manager.type)
            {
                case ControllerType.Normal:
                    inputAxes = new List<InputAxis>
                    {
                        new InputAxis()
                        {
                            name = manager.LeftTrigger,
                            descriptiveName = "Device trigger axis",
                            axis = 9,
                            type = 2
                        },
                        new InputAxis()
                        {
                            name = manager.LeftGrip,
                            descriptiveName = "Device grip axis",
                            axis = 11,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.LeftThumb,
                            descriptiveName = "Device thumb proximity touch axis",
                            dead = 0.19f,
                            axis = 15,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightTrigger,
                            descriptiveName = "Device trigger axis",
                            axis = 10,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightGrip,
                            descriptiveName = "Device grip axis",
                            axis = 12,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightThumb,
                            descriptiveName = "Device thumb proximity touch axis",
                            dead = 0.19f,
                            axis = 16,
                            type = 2,
                        }
                    };
                    break;
                case ControllerType.Knuckles:
                    inputAxes = new List<InputAxis>
                    {

                        new InputAxis()
                        {
                            name = manager.LeftIndex,
                            descriptiveName = "Left Index Finger for a Knuckles Style controller",
                            axis = 21,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.LeftMiddle,
                            descriptiveName = "Left Middle Finger for a Knuckles Style controller",
                            axis = 23,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.LeftRing,
                            descriptiveName = "Left Ring Finger for a Knuckles Style controller",
                            axis = 25,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.LeftPinky,
                            descriptiveName = "Left Pinky Finger for a Knuckles Style controller",
                            axis = 27,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.LeftThumb,
                            descriptiveName = "Device thumb proximity touch axis",
                            dead = 0.19f,
                            axis = 15,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightIndex,
                            descriptiveName = "Right Index Finger for a Knuckles Style controller",
                            axis = 22,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightMiddle,
                            descriptiveName = "Right Middle Finger for a Knuckles Style controller",
                            axis = 24,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightRing,
                            descriptiveName = "Right Ring Finger for a Knuckles Style controller",
                            axis = 26,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightPinky,
                            descriptiveName = "Right Pinky Finger for a Knuckles Style controller",
                            axis = 28,
                            type = 2,
                        },
                        new InputAxis()
                        {
                            name = manager.RightThumb,
                            descriptiveName = "Device thumb proximity touch axis",
                            dead = 0.19f,
                            axis = 16,
                            type = 2,
                        }

                    };
                    break;
                default:
                    break;
            }

            for (int i = 0; i < inputAxes.Count; i++)
            {
                if (!ElementExsists(inputAxes[i].name, inputManagerCurrentData))
                {
                    AddAxe(inputAxes[i], ref inputManagerCurrentData);

                }

            }

            #endregion

            serializedObject.ApplyModifiedProperties();

        }
        private static void AddAxe(InputAxis axe, ref UnityEditor.SerializedProperty currentList)
        {
            var index = currentList.arraySize++;
            var newItem = currentList.GetArrayElementAtIndex(index);
            var iteratorProperty = newItem.Copy();
            iteratorProperty.Next(true);
            do
            {
                switch (iteratorProperty.name)
                {
                    case "m_Name":
                        iteratorProperty.stringValue = axe.name;
                        break;
                    case "descriptiveName":
                        iteratorProperty.stringValue = axe.descriptiveName;
                        break;
                    case "descriptiveNegativeName":
                        iteratorProperty.stringValue = axe.descriptiveNegativeName;
                        break;
                    case "negativeButton":
                        iteratorProperty.stringValue = axe.negativeButton;
                        break;
                    case "positiveButton":
                        iteratorProperty.stringValue = axe.positiveButton;
                        break;
                    case "altNegativeButton":
                        iteratorProperty.stringValue = axe.altNegativeButton;
                        break;
                    case "altPositiveButton":
                        iteratorProperty.stringValue = axe.altPositiveButton;
                        break;
                    case "gravity":
                        iteratorProperty.floatValue = axe.gravity;
                        break;
                    case "dead":
                        iteratorProperty.floatValue = axe.dead;
                        break;
                    case "sensitivity":
                        iteratorProperty.floatValue = axe.sensitivity;
                        break;
                    case "snap":
                        iteratorProperty.boolValue = axe.snap;
                        break;
                    case "invert":
                        iteratorProperty.boolValue = axe.invert;
                        break;
                    case "type":
                        iteratorProperty.intValue = axe.type;
                        break;
                    case "axis":
                        iteratorProperty.intValue = axe.axis-1;
                        break;
                    case "joyNum":
                        iteratorProperty.intValue = axe.joyNum;
                        break;

                }
            } while (iteratorProperty.Next(false));

        }
        private static  bool ElementExsists(string name, UnityEditor.SerializedProperty currentList)
        {
            bool elementExsist = false;

            for (int i = 0; i < currentList.arraySize; i++)
            {
                var element = currentList.GetArrayElementAtIndex(i).Copy();

                element.Next(true);
                do
                {

                    if (element.name == "m_Name")
                    {
                        if (element.stringValue == name)
                        {
                            elementExsist = true;
                        }
                        break;
                    }
                } while (element.Next(false));
                if (elementExsist)
                {
                    break;
                }
            }
            return elementExsist;
        }

    }
}