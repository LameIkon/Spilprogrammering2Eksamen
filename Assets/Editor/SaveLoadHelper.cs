using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveLoader))]
public class SaveLoadHelper : Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoader script = (SaveLoader)target;
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
