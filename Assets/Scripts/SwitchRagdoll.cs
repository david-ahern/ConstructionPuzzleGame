using UnityEngine;
using System.Collections;

public class SwitchRagdoll : MonoBehaviour {

    public bool Ragdoll = false;

    public bool isRagdoll = false;

    void Start()
    {
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            if (r.gameObject.name != gameObject.name)
                if (Ragdoll)
                {
                    r.useGravity = true;
                    r.isKinematic = false;
                }
                else
                {
                    r.useGravity = false;
                    r.isKinematic = true;
                }
        }

        isRagdoll = Ragdoll;    
    }

    void Update()
    {
        if (Ragdoll != isRagdoll)
        {
            foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
            {
                if (r.gameObject.name != gameObject.name)
                    if (Ragdoll)
                    {
                        r.useGravity = true;
                        r.isKinematic = false;
                    }
                    else
                    {
                        r.useGravity = false;
                        r.isKinematic = true;
                    }
            }

            isRagdoll = Ragdoll;  
        }
    }
}
