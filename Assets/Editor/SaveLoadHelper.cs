using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveLoadScript))]
public class SaveLoadHelper : Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoadScript script = (SaveLoadScript)target;
        string fileName = script._fileName;

        DrawDefaultInspector();

        if (GUILayout.Button("Save"))
        {
            script.Save(fileName);
        }

        if (GUILayout.Button("Load"))
        {
            script.Load(fileName);
        }

    }


}
