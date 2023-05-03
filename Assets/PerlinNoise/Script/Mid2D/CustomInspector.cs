using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateMap))]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateMap genMap=(GenerateMap)target;

        if (DrawDefaultInspector())
        {
            if (genMap.autoUpdate)
            {
                genMap.Generate();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            genMap.Generate();
        }
    }
}
