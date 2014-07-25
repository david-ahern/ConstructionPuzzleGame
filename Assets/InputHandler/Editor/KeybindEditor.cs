using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class KeybindEditor : EditorWindow {

    static KeybindHolder _KeyHolder;

    [MenuItem("Custom/Keybind Editor")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(KeybindEditor));
    }
}
