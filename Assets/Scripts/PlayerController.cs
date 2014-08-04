using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public InputController.PlayerNumber PlayerNum = InputController.PlayerNumber.One;

    public float MovementSpeed = 1;

    public Animator Anim;

    public bool ShouldRagdoll = false;
    public bool Ragdoll
    {
        set { SwitchRagdoll(value); }
        get { return isRagdoll; }
    }

    private bool isRagdoll = false;

    void Start()
    {
        Ragdoll = ShouldRagdoll;
    }

    void Update()
    {
        Ragdoll = ShouldRagdoll;
        Vector3 movement = Vector3.zero;
        movement.x = MovementSpeed * (InputController.GetAxis("MoveRight", PlayerNum) - InputController.GetAxis("MoveLeft", PlayerNum));
        movement.z = MovementSpeed * (InputController.GetAxis("MoveUp", PlayerNum) - InputController.GetAxis("MoveDown", PlayerNum));

        gameObject.transform.position += movement * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            Anim.SetTrigger("Walk");
            Anim.ResetTrigger("Stop");
            gameObject.transform.forward = movement;
            Debug.Log(movement);
        }
        else
        {
            Anim.SetTrigger("Stop");
            Anim.ResetTrigger("Walk");
        }
    }


    void SwitchRagdoll(bool rag)
    {
        if (rag != isRagdoll)
        {
            foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
            {
                if (r.gameObject.name != gameObject.name && r.gameObject.name != "hips")
                    if (rag)
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

            isRagdoll = rag;

            Anim.enabled = !isRagdoll;
        }
    }
}
