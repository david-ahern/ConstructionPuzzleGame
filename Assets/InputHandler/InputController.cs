using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour 
{
    public KeybindHolder Keybinds;

    bool GettingButton = false;

    void Awake()
    {
        StartCoroutine(coGetButton());
    }

    void Update()
    {
        bool buttonDown = Keybinds.GetButton("Jump");

        if (buttonDown && !GettingButton)
            StartCoroutine(coGetButton());
    }

    public IEnumerator coGetButton()
    {
        Debug.Log("Getting new button");
        yield return StartCoroutine(Keybinds.RebindKey("Jump"));
    }

    void Get()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (!key.ToString().Contains("JoystickB"))
                if (Input.GetKey(key))
                    Debug.Log(key + " pressed");
        }
    }
}
