using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class KeybindEditor : EditorWindow 
{
    static private string HolderPath = "Assets/InputHandler/Holder/KeybindHolder.asset";
    static KeybindHolder _KeyHolder;

    [MenuItem("Custom/Keybind Editor")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(KeybindEditor));

        if (_KeyHolder == null)
        {
            _KeyHolder = (KeybindHolder)AssetDatabase.LoadAssetAtPath(HolderPath, typeof(KeybindHolder));

            if (_KeyHolder == null)
            {
                _KeyHolder = ScriptableObject.CreateInstance<KeybindHolder>();
                _KeyHolder.Keys = new List<KeybindHolder.Key>();
                AssetDatabase.CreateAsset(_KeyHolder, HolderPath);

                if (_KeyHolder == null)
                    Debug.Log("Failed to load or create Keybind Holder");
            }
        }
    }

    void OnGUI()
    {
        foreach(KeybindHolder.Key key in _KeyHolder.Keys)
        {
            GUILayout.Label(key.Name);
        }
    }
}
