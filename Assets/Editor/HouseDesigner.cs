using UnityEngine;
using UnityEditor;

public class HouseDesigner : EditorWindow
{

    [MenuItem("Window/Test")]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(HouseDesigner));
    }

    void OnGUI()
    {
        GUILayout.Label("Place House", EditorStyles.boldLabel);
    }

}
