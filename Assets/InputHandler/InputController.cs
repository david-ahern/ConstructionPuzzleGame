using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour 
{
    void Update()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (!key.ToString().Contains("JoystickB"))
                if (Input.GetKey(key))
                    Debug.Log(key + " pressed");
        }
    }
}
