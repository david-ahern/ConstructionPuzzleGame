using UnityEngine;
using System.Collections;

public class XboxControllerTestScript : MonoBehaviour 
{
	
	void Update () 
    {
        //if (Input.GetAxis("Xbox-AButton") > 0)
        //    Debug.Log("A Pressed - P1");
        //if (Input.GetButtonDown("Xbox-AButtonP2"))
        //    Debug.Log("A Pressed - P2");
        //if (Input.GetButtonDown("Xbox-BButton"))
        //    Debug.Log("B Pressed");
        //if (Input.GetButtonDown("Xbox-XButton"))
        //    Debug.Log("X Pressed");
        //if (Input.GetButtonDown("Xbox-YButton"))
        //    Debug.Log("Y Pressed");
        //if (Input.GetButtonDown("Xbox-LBump"))
        //    Debug.Log("Left Bumper Pressed");
        //if (Input.GetButtonDown("Xbox-RBump"))
        //    Debug.Log("Right Bumper Pressed");
        //if (Input.GetButtonDown("Xbox-BackButton"))
        //    Debug.Log("Back Pressed");
        //if (Input.GetButtonDown("Xbox-StartButton"))
        //    Debug.Log("Start Pressed");
        //if (Input.GetButtonDown("Xbox-LStick"))
        //    Debug.Log("Left Stick Pressed");
        //if (Input.GetButtonDown("Xbox-RStick"))
        //    Debug.Log("Right Stick Pressed");
        
        /*if (Input.GetAxis("Trigger_Left_1") > 0)
            Debug.Log("P1 Left trigger: " + Input.GetAxis("Trigger_Left_1"));
        if (Input.GetAxis("Trigger_Right_1") > 0)
            Debug.Log("P1 Right Trigger: " + Input.GetAxis("Trigger_Right_1"));

        if (Input.GetAxis("DPad_Left_1") > 0)
            Debug.Log("P1 D Pad Left");
        if (Input.GetAxis("DPad_Right_1") > 0)
            Debug.Log("P1 D Pad Right");
        if (Input.GetAxis("DPad_Up_1") > 0)
            Debug.Log("P1 D Pad Up");
        if (Input.GetAxis("DPad_Down_1") > 0)
            Debug.Log("P1 D Pad Down");

        if (Input.GetAxis("LStick_Right_1") > 0)
            Debug.Log("P1 Left stick right: " + Input.GetAxis("LStick_Right_1"));
        if (Input.GetAxis("LStick_Left_1") > 0)
            Debug.Log("P1 Left stick left: " + Input.GetAxis("LStick_Left_1"));
        if (Input.GetAxis("LStick_Up_1") > 0)
            Debug.Log("P1 Left stick up: " + Input.GetAxis("LStick_Up_1"));
        if (Input.GetAxis("LStick_Down_1") > 0)
            Debug.Log("P1 Left stick down: " + Input.GetAxis("LStick_Down_1"));

        if (Input.GetAxis("RStick_Right_1") > 0)
            Debug.Log("P1 Right stick right: " + Input.GetAxis("RStick_Right_1"));
        if (Input.GetAxis("RStick_Left_1") > 0)
            Debug.Log("P1 Right stick left: " + Input.GetAxis("RStick_Left_1"));
        if (Input.GetAxis("RStick_Up_1") > 0)
            Debug.Log("P1 Right stick up: " + Input.GetAxis("RStick_Up_1"));
        if (Input.GetAxis("RStick_Down_1") > 0)
            Debug.Log("P1 Right stick down: " + Input.GetAxis("RStick_Down_1"));
        */

        if (InputController.GetButton("Jump"))
            Debug.Log("Key pressed");
	}
}
