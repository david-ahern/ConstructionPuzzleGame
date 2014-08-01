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
        if (Keybinds.GetButtonUp("MoveUp", KeybindHolder.PlayerNumber.One))
            Debug.Log("Jump");

        //if (Keybinds.GetAxis("MoveUp", KeybindHolder.PlayerNumber.One) > 0)
          //  Debug.Log("Moving up");
    }

    void LateUpdate()
    {
        Keybinds.LateUpdate();
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
