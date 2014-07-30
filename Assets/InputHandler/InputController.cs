using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour 
{
    public KeybindHolder Keybinds;

    bool GettingButton = false;

    void Awake()
    {
    }

    void Update()
    {
        if (Keybinds.GetButton("Jump"))
            Debug.Log("Jump");

        if (Mathf.Abs(Keybinds.GetAxis("Jump")) > Keybinds.Threshold)
            Debug.Log("JUMPING");
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
