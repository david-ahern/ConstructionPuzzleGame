using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeybindHolder : ScriptableObject
{
    public List<Key> Keys;

    public float GetAxis(string name)
    {
        foreach (Key k in Keys)
        {
            if (k.Name == name)
            {
                if (k.IsAxis)
                    return Input.GetAxis(k.AxisName);
                else
                    return Convert.ToSingle(Input.GetKey(k.Code));
            }
        }
        return 0;
    }

    public bool GetButton(string name)
    {
        foreach (Key K in Keys)
        {
            if (K.Name == name)
            {
                return Convert.ToBoolean(GetAxis(K.Name));
            }
        }
        return false;
    }

    public void SetButton(string name, bool isAxis, KeyCode key, string axis)
    {
        foreach (Key k in Keys)
        {
            if (k.Name == name)
            {
                k.Update(isAxis, key, axis);
                return;
            }
        }
        Keys.Add(new Key(name, isAxis, key, axis));
    }

    [System.Serializable]
    public class Key
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public bool IsAxis = false;
        [SerializeField]
        public KeyCode Code = KeyCode.None;
        [SerializeField]
        public string AxisName = "";

        public Key(string n, bool i, KeyCode c, string a) { Name = n; IsAxis = i; Code = c; AxisName = a; }
        public void Update(bool i, KeyCode c, string a) { IsAxis = i; Code = c; AxisName = a; }
    }
}


