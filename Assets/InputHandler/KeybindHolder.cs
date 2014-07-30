using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeybindHolder : ScriptableObject
{
    public List<Key> Keys;

    public List<string> AxisNames = new List<string>();

    public float Threshold = 0.5f;

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
                float input = GetAxis(name);
                if (input > Threshold)
                    return true;
                else
                    return false;
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

    public IEnumerator RebindKey(string name)
    {
        Key keyToSet = null;
        foreach (Key k in Keys)
        {
            if (k.Name == name)
                keyToSet = k;
        }

        if (keyToSet == null)
            yield break;

        bool gotKey = false;
        while (!gotKey)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (!key.ToString().Contains("JoystickB"))
                    if (Input.GetKey(key))
                    {
                        Debug.Log("Got new button: " + key);
                        keyToSet.Update(false, key, "");
                        gotKey = true;
                        break;
                    }
            }
            if (!gotKey)
            {
                foreach (string axis in AxisNames)
                {
                    if (Input.GetAxis(axis) != 0)
                    {
                        Debug.Log("Got new axis: " + axis);
                        keyToSet.Update(true, KeyCode.None, axis);
                    }
                }
            }
            Debug.Log("Waiting for new button");
            yield return new WaitForEndOfFrame();
        }
    }
}


