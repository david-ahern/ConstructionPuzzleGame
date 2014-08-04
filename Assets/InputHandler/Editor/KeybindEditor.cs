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
    }

    void OnDestroy()
    {
        _KeyHolder = null;
    }

    List<bool> KeyFoldouts = new List<bool>();

    string searchString = "";

    string newActionName = "";

    InputController.PlayerNumber ShowPlayer = InputController.PlayerNumber.One;

    void OnGUI()
    {
        if (_KeyHolder == null)
            GetKeyholder();
        try
        {
            GUIStyle BoldButton = new GUIStyle(EditorStyles.miniButton);
            BoldButton.fontStyle = FontStyle.Bold;

            Texture AddButton = GUIIconEditor.GetIcon("Add Icon");
            Texture TrashButton = GUIIconEditor.GetIcon("Trash Icon");
            Texture UpArrow = GUIIconEditor.GetIcon("Up Icon");
            Texture DownArrow = GUIIconEditor.GetIcon("Down Icon");
            Texture SaveIcon = GUIIconEditor.GetIcon("Save Icon");

            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            GUILayout.FlexibleSpace();
            searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.Width(this.position.width - 150));
            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                searchString = "";
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();

            ShowPlayer = (InputController.PlayerNumber)EditorGUI.EnumPopup(new Rect(1, 1, 100, 15), "", ShowPlayer);

            EditorGUILayout.BeginHorizontal();
            newActionName = GUILayout.TextField(newActionName);

            if (GUILayout.Button("Add Action", GUILayout.Width(100)) && newActionName != "")
                AddAction(newActionName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.EndHorizontal();

            if (KeyFoldouts.Count < _KeyHolder.Keys.Count)
                for (int i = KeyFoldouts.Count; i < _KeyHolder.Keys.Count; i++)
                    KeyFoldouts.Add(false);
            else if (KeyFoldouts.Count > _KeyHolder.Keys.Count)
                for (int i = KeyFoldouts.Count; i > _KeyHolder.Keys.Count; i--)
                    KeyFoldouts.RemoveAt(KeyFoldouts.Count - 1);

            for (int i = 0; i < _KeyHolder.Keys.Count; i++)
            {
                if (_KeyHolder.Keys[i].PlayerNumber == ShowPlayer)
                {
                    KeyFoldouts[i] = EditorGUILayout.Foldout(KeyFoldouts[i], _KeyHolder.Keys[i].Name, EditorStyles.foldout);
                    if (KeyFoldouts[i])
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Is Axis: ");
                        _KeyHolder.Keys[i].IsAxis = EditorGUILayout.Toggle(_KeyHolder.Keys[i].IsAxis);
                        EditorGUILayout.EndHorizontal();

                        if (!_KeyHolder.Keys[i].IsAxis)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Label("Key Code: ");
                            _KeyHolder.Keys[i].Code = (KeyCode)EditorGUILayout.EnumPopup(_KeyHolder.Keys[i].Code);
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Label("Axis Name: ");
                            _KeyHolder.Keys[i].AxisName = _KeyHolder.AxisNames[EditorGUILayout.Popup(_KeyHolder.AxisNames.IndexOf(_KeyHolder.Keys[i].AxisName), _KeyHolder.AxisNames.ToArray())];
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }
            }
        }
        catch
        { }
    }

    void GetKeyholder()
    {
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

    private void AddAction(string name)
    {
        KeybindHolder.Key newKeyP1 = new KeybindHolder.Key(name, false, KeyCode.None, _KeyHolder.AxisNames[0], InputController.PlayerNumber.One);
        KeybindHolder.Key newKeyP2 = new KeybindHolder.Key(name, false, KeyCode.None, _KeyHolder.AxisNames[0], InputController.PlayerNumber.Two);
        KeybindHolder.Key newKeyP3 = new KeybindHolder.Key(name, false, KeyCode.None, _KeyHolder.AxisNames[0], InputController.PlayerNumber.Three);
        KeybindHolder.Key newKeyP4 = new KeybindHolder.Key(name, false, KeyCode.None, _KeyHolder.AxisNames[0], InputController.PlayerNumber.Four);

        _KeyHolder.Keys.Add(newKeyP1);
        _KeyHolder.Keys.Add(newKeyP2);
        _KeyHolder.Keys.Add(newKeyP3);
        _KeyHolder.Keys.Add(newKeyP4);
    }
}