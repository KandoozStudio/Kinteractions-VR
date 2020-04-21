using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kandooz.KVR;
using UnityEditor.Experimental.AssetImporters;
using Kandooz.KVR.Editors;

[ScriptedImporter(1,"KinterActionHandData")]
public class InputManagerImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var managerIDs=AssetDatabase.FindAssets("t:VRInputManager");

        for (int i = 0; i < managerIDs.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(managerIDs[i]);
            var manager=AssetDatabase.LoadAssetAtPath<VRInputManager>(path);
            VRInputManagerEditor.SeedInputs(manager);
        }
    }
}
