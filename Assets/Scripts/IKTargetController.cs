using UnityEngine;
using System.Collections;

public class IKTargetController : MonoBehaviour 
{
    public bool LeftArm = false;
    public Transform HandTarget;
    public Transform ElbowTarget;
    public IKLimb IKScript;

    private Vector3 ElbowTargetStartPos;

    public float Offset = 0.5f;

    public Transform TargetObject;

    void Start()
    {
        ElbowTargetStartPos = ElbowTarget.localPosition;
    }

    void Update()
    {
        if (TargetObject)
        {
            IKScript.IsEnabled = true;
            HandTarget.position = TargetObject.position;
        }

        Vector3 temp = ElbowTargetStartPos;

        if (LeftArm)
            temp.y -= HandTarget.localPosition.x - Offset;
        else
            temp.y += HandTarget.localPosition.x + Offset;

        ElbowTarget.localPosition = temp;
    }
}
