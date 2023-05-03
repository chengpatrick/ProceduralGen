using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PerlinGenerator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PerlinGenerator perlin = (PerlinGenerator)target;

        if(GUILayout.Button("Generate Map"))
        {
            Debug.Log(Mathf.PI);
            perlin.ClearScene();
            perlin.StartGenerate();
        }
        if (GUILayout.Button("Clear Map"))
        {
            perlin.ClearScene();
        }
    }
}
