﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeybindHolder : ScriptableObject
{
    public List<Key> Keys;
    public float Threshold = 0.2f;

    public List<string> AxisNames = new List<string>();

    private List<bool> WasDown;
    private List<bool> Downed;
    private List<bool> Upped;


    public void LateUpdate()
    {
        foreach (Key k in Keys)
        {
            if (!k.WasDown && (Input.GetAxis(k.AxisName) > Threshold))
            {
                k.Downed = true;
                k.WasDown = true;
            }
            else
                k.Downed = false;

            if (k.WasDown && Input.GetAxis(k.AxisName) < Threshold)
            {
                k.Upped = true;
                k.WasDown = false;
            }
            else
                k.Upped = false;
        }
    }


    public float GetAxis(string name, InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
        {
            if (k.Name == name && (k.PlayerNumber == player || player == InputController.PlayerNumber.Any))
            {
                if (k.IsAxis)
                    return Mathf.Max(0, Input.GetAxis(k.AxisName));
                else
                    return Convert.ToSingle(Input.GetKey(k.Code));
            }
        }
        return 0;
    }

    public bool GetButton(string name, InputController.PlayerNumber player)
    {
        foreach (Key K in Keys)
        {
            if (K.Name == name && (K.PlayerNumber == player || player == InputController.PlayerNumber.Any))
            {
                float input = GetAxis(name, player);
                if (input > Threshold)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }

    public bool GetButtonDown(string name, InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
        {
            if (k.Name == name && (k.PlayerNumber == player || player == InputController.PlayerNumber.Any))
            {
                if (!k.IsAxis)
                    return Input.GetKeyDown(k.Code);
                else
                    return k.Downed;
            }
        }
        return false;
    }

    public bool GetButtonUp(string name, InputController.PlayerNumber player)
    {
        foreach(Key k in Keys)
        {
            if (k.Name == name && (k.PlayerNumber == player || player == InputController.PlayerNumber.Any))
            {
                if (!k.IsAxis)
                    return Input.GetKeyDown(k.Code);
                else
                    return k.Upped;
            }
        }
        return false;
    }

    public bool AnyButton(InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
            if (k.PlayerNumber == player || player == InputController.PlayerNumber.Any)
                return GetButton(k.Name, player);

        return Input.anyKey;
    }

    public bool AnyButtonDown(InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
            if (k.PlayerNumber == player || player == InputController.PlayerNumber.Any)
                return GetButtonDown(k.Name, player);

        return Input.anyKeyDown;
    }

    public bool AnyButtonUp(InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
            if (k.PlayerNumber == player || player == InputController.PlayerNumber.Any)
                return GetButtonUp(k.Name, player);

        return false;
    }

    public void SetButton(string name, bool isAxis, KeyCode key, string axis, InputController.PlayerNumber player)
    {
        foreach (Key k in Keys)
        {
            if (k.Name == name)
            {
                k.Update(isAxis, key, axis);
                return;
            }
        }
        Keys.Add(new Key(name, isAxis, key, axis, player));
    }

    public IEnumerator RebindKey(string name, InputController.PlayerNumber player)
    {
        Key keyToSet = null;
        foreach (Key k in Keys)
        {
            if (k.Name == name && k.PlayerNumber == player)
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
                    if (Input.GetAxis(axis) > Threshold)
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
        [SerializeField]
        public InputController.PlayerNumber PlayerNumber;
        [SerializeField]
        public bool WasDown = false;
        [SerializeField]
        public bool Downed = false;
        [SerializeField]
        public bool Upped = false;

        public Key(string n, bool i, KeyCode c, string a, InputController.PlayerNumber p) { Name = n; IsAxis = i; Code = c; AxisName = a; PlayerNumber = p; }
        public void Update(bool i, KeyCode c, string a) { IsAxis = i; Code = c; AxisName = a; }
    }
}


