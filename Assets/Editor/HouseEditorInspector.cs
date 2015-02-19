using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HouseEditor))]
public class HouseEditorInspector : Editor
{
    private const string _BACKGROUNDS = "Backgrounds";
    private const string _GAMEOBJECTS = "GameObjects";
    private const string _NPCS = "Non-Playable Characters";

    private bool _showBackground;
    private bool _showGameObjects;
    private bool _showNPCs;

    public override void OnInspectorGUI()
    {
        HouseEditor editor = (HouseEditor)target;

        _showBackground     = EditorGUILayout.Foldout(_showBackground, _BACKGROUNDS);
        _showGameObjects    = EditorGUILayout.Foldout(_showGameObjects, _GAMEOBJECTS);
        _showNPCs           = EditorGUILayout.Foldout(_showNPCs, _NPCS);

        if (_showBackground)
        {
            EditorGUI.indentLevel++;

            for (int i = 0; i < editor.typeBackgrounds.Length; i++)
            {
                GUILayout.Button(editor.typeBackgrounds[i].GetComponent<SpriteRenderer>().sprite.texture);
            }

            EditorGUI.indentLevel--;
        }

        if (_showGameObjects)
        {
            EditorGUI.indentLevel++;

            for (int i = 0; i < editor.typeGameObjects.Length; i++)
            {
                GUILayout.Button(editor.typeGameObjects[i].GetComponent<SpriteRenderer>().sprite.texture);
            }

            EditorGUI.indentLevel--;
        }

        if (_showNPCs)
        {
            EditorGUI.indentLevel++;

            for (int i = 0; i < editor.typeNPCs.Length; i++)
            {
                GUILayout.Button(editor.typeNPCs[i].GetComponent<SpriteRenderer>().sprite.texture);
            }

            EditorGUI.indentLevel--;
        }
    }
}