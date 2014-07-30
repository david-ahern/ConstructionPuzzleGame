using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class HierarchyIcons 
{
    static List<int> markedObjects;

    static HierarchyIcons()
    {
        EditorApplication.update += Update;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyUpdate;
    }

    private static void Update()
    {
        GameObject[] go = Object.FindObjectsOfType<GameObject>();

        markedObjects = new List<int>();

        foreach (GameObject g in go)
        {
            if (g.GetComponent<SoundController>() != null)
                markedObjects.Add(g.GetInstanceID());
        }
    }

    private static void HierarchyUpdate(int instanceID, Rect selectionRect)
    {
        Rect r = new Rect(selectionRect);
        r.x = r.width - 20;
        r.width = 19;
        r.height = 19;

        if (markedObjects != null && markedObjects.Contains(instanceID))
            GUI.Label(r, GUIIconEditor.GetIcon("Sound Icon"));
    }
}
