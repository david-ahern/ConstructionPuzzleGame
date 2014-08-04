using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour 
{
    static public InputController instance;

    public enum PlayerNumber { One, Two, Three, Four, Any };

    public KeybindHolder Keybinds;

    bool GettingButton = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            DestroyImmediate(this);
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        Keybinds.LateUpdate();
    }

    static public bool GetButton(string ButtonName, PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.GetButton(ButtonName, Player);
        else
            return false;
    }

    static public bool GetButtonDown(string ButtonName, PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.GetButtonDown(ButtonName, Player);
        else
            return false;
    }

    static public bool GetButtonUp(string ButtonName, PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.GetButtonUp(ButtonName, Player);
        else
            return false;
    }

    static public float GetAxis(string AxisName, PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.GetAxis(AxisName, Player);
        else
            return 0;
    }

    static public bool AnyButton(PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.AnyButton(Player);
        else
            return false;
    }

    static public bool AnyButtonDown(PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.AnyButtonDown(Player);
        else
            return false;
    }

    static public bool AnyButtonUp(PlayerNumber Player = PlayerNumber.Any)
    {
        if (instance)
            return instance.Keybinds.AnyButtonUp(Player);
        else
            return false;
    }

    public IEnumerator coGetButton()
    {
        Debug.Log("Getting new button");
        yield return StartCoroutine(Keybinds.RebindKey("Jump", PlayerNumber.One));
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
