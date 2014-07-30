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

    List<bool> KeyFoldouts = new List<bool>();

    void OnGUI()
    {
        if (KeyFoldouts.Count < _KeyHolder.Keys.Count)
            for (int i = KeyFoldouts.Count; i < _KeyHolder.Keys.Count; i++ )
                KeyFoldouts.Add(false);
        else if (KeyFoldouts.Count > _KeyHolder.Keys.Count)
            for (int i = KeyFoldouts.Count; i > _KeyHolder.Keys.Count; i--)
                KeyFoldouts.RemoveAt(KeyFoldouts.Count - 1);

        for (int i = 0; i < _KeyHolder.Keys.Count; i++)
        {
            KeyFoldouts[i] = EditorGUILayout.Foldout(KeyFoldouts[i], _KeyHolder.Keys[i].Name, EditorStyles.foldout);
            if (KeyFoldouts[i])
            {
                GUILayout.Label(_KeyHolder.Keys[i].Name);
                GUILayout.Label(_KeyHolder.Keys[i].IsAxis.ToString());
                GUILayout.Label(_KeyHolder.Keys[i].Code.ToString());
                GUILayout.Label(_KeyHolder.Keys[i].AxisName);
            }
        }
    }
}
