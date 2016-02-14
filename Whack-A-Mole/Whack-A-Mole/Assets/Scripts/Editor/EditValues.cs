using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditValues : EditorWindow {


    float _drainAmount,
          _minTimeBetweenMoles,
          _minMoleDuration,
          _maxTimeBetweenMoles,
          _maxMoleDuration;

    [MenuItem("Custom Tools/Value Editor")]
	static void Init()
    {
        EditValues window = (EditValues)EditorWindow.GetWindow(typeof(EditValues));
        window.Show();
    }


    void OnGUI()
    {
        GUILayout.Label("Edit Gameplay Values  NOT WORKING CURRENTLY", EditorStyles.boldLabel);

        _drainAmount = EditorGUILayout.FloatField("Bar Drain Amount", _drainAmount);

        EditorGUILayout.Space();
        GUILayout.Label("Time Between Moles", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        _minTimeBetweenMoles = EditorGUILayout.FloatField("Min", _minTimeBetweenMoles);
        _maxTimeBetweenMoles = EditorGUILayout.FloatField("Max", _maxTimeBetweenMoles);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        GUILayout.Label("Mole Life Duration", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        _minMoleDuration = EditorGUILayout.FloatField("Min", _minMoleDuration);
        _maxMoleDuration = EditorGUILayout.FloatField("Max", _maxMoleDuration);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        GUI.color = Color.green;
        GUILayout.Button("SET VALUES");


    }

    private void GetValues()
    {
        LevelManager _levelManager = GameObject.Find("_LevelManager").GetComponent<LevelManager>();
    }
}
