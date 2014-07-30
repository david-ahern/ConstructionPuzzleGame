using UnityEngine;
using System.Collections;

public class IKTargetController : MonoBehaviour {

    public Transform HandTarget;
    public Transform ElbowTarget;

    private Vector3 ElbowTargetStartPos;

    public float Offset = 0.5f;

    void Start()
    {
        ElbowTargetStartPos = ElbowTarget.localPosition;
    }

    void Update()
    {
        Vector3 temp = ElbowTargetStartPos;

        temp.y -= HandTarget.localPosition.x - Offset;

        ElbowTarget.localPosition = temp;
    }
}
