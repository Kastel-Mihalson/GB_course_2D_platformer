using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateLevelView))]
public class GeneratorLevelEditor : Editor
{
    private GenerateLevelController _controller;


    private void OnEnable()
    {
        var view = (GenerateLevelView)target;
        _controller = new GenerateLevelController(view);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var tileMap = serializedObject.FindProperty("_tileMap");
        EditorGUILayout.PropertyField(tileMap);

        if (GUI.Button(new Rect(10, 0, 100, 30), "Generate"))
            _controller.GenerateLevel();

        if (GUI.Button(new Rect(10, 40, 100, 30), "Clear"))
            _controller.ClearMap();

        GUILayout.Space(100);

        serializedObject.ApplyModifiedProperties();
    }
}
