using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public InputController.PlayerNumber PlayerNum = InputController.PlayerNumber.One;

    public float MovementForce = 1;
    public float MaxVelocity = 5;
    public float TurnEasing = 0.2f;

    public float JumpForce = 10;
    public float JumpedMovementMultiplyer = 0.2f;
    public float MaxJumpTime = 1;
    public Animator Anim;
    public Transform Body;

    private RigidbodyConstraints RagdollConstraints = RigidbodyConstraints.None;
    private RigidbodyConstraints NormalConstraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

    public float AtRestThreshold = 0;
    public float RestForSeconds = 1;

    private Vector3 TargetForward;

    private float TotalMass = 0;

    private bool DisableMovement = false;

    public bool Grounded = true;
    public bool BecameGrounded = false;
    private float JumpTime = 0;

    public IKTargetController LeftHandIKController;
    public IKTargetController RightHandIKController;

    public bool Ragdoll
    {
        set { SwitchRagdoll(value); }
        get { return isRagdoll; }
    }

    private bool isRagdoll = false;

    void Awake()
    {
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
            TotalMass += r.mass;
    }

    void Start()
    {
        SwitchRagdoll(false, true);
    }

    void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleJumpTime();
    }

    void HandleMovement()
    {
        if (!Ragdoll && !DisableMovement)
        {
            float moveHorizontal = InputController.GetAxis("MoveRight", PlayerNum) - InputController.GetAxis("MoveLeft", PlayerNum);
            float moveVertical = InputController.GetAxis("MoveUp", PlayerNum) - InputController.GetAxis("MoveDown", PlayerNum);

            Vector3 movement = Vector3.zero;

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                movement.x = moveHorizontal * MovementForce;
                movement.z = moveVertical * MovementForce;

                movement = Vector3.ClampMagnitude(movement, MaxVelocity * (Grounded ? 1 : JumpedMovementMultiplyer));

                transform.forward += (movement.normalized - transform.forward) * TurnEasing;

                Anim.SetTrigger("Walk");
                Anim.ResetTrigger("Stop");
            }
            else
            {
                Anim.SetTrigger("Stop");
                Anim.ResetTrigger("Walk");
            }

            if (InputController.GetButtonDown("Jump") && Grounded)
            {
                TargetForward = transform.forward;
                movement.y = JumpForce * TotalMass;
                JumpTime = 0;
            }

            rigidbody.AddForce(movement * TotalMass);
        }
    }

    void HandleJumpTime()
    {
        if (!Grounded && !Ragdoll)
        {
            JumpTime += Time.deltaTime;

            if (JumpTime > MaxJumpTime)
            {
                Ragdoll = true;
                JumpTime = 0;
            }
        }

        if (BecameGrounded)
            JumpTime = 0;
    }

    private void CheckGrounded()
    {
        if (!Ragdoll)
        {
            RaycastHit Hit;
            float Distance = 0.1f;
            Vector3 Direction = -Vector3.up;

            Debug.DrawRay(gameObject.transform.position, Direction * Distance, Color.green);

            if (Physics.Raycast(gameObject.transform.position, Direction, out Hit, Distance))
            {
                if (Hit.collider.tag == "Level")
                {
                    if (!Grounded)
                        BecameGrounded = true;
                    else
                        BecameGrounded = false;

                    Grounded = true;
                }
                else
                    Grounded = false;
            }
            else
                Grounded = false;
        }
    }

    IEnumerator CheckRagdollAtRest()
    {
        float atRestTime = 0;

        while (atRestTime < RestForSeconds)
        {
            if (rigidbody.velocity.magnitude < AtRestThreshold)
                atRestTime += Time.deltaTime;
            else
                atRestTime = 0;
            yield return new WaitForEndOfFrame();
        }

        DisableMovement = true;
        Ragdoll = false;

        float startTime = Time.time;
        Vector3 StartForward = transform.forward;

        while (Time.time - startTime < 1)
        {
            transform.forward = Vector3.Lerp(StartForward, TargetForward, Time.time - startTime);
            yield return new WaitForEndOfFrame();
        }
        DisableMovement = false;
    }

    void SwitchRagdoll(bool rag, bool force = false)
    {
        if (rag != isRagdoll || force)
        {
            if (rag)
            {
                Body.gameObject.SetActive(true);
                collider.enabled = false;
                rigidbody.constraints = RagdollConstraints;

                foreach (Rigidbody r in Body.GetComponentsInChildren<Rigidbody>())
                {
                    r.velocity = rigidbody.velocity;

                    if (r.name == "Helmet")
                        r.transform.parent = null;
                }
                StartCoroutine(CheckRagdollAtRest());
            }
            else
            {
                Body.gameObject.SetActive(false);
                collider.enabled = true;
                rigidbody.constraints = NormalConstraints;
            }
            isRagdoll = rag;

            Anim.enabled = !isRagdoll;
            LeftHandIKController.IKScript.IsEnabled = !isRagdoll;
            RightHandIKController.IKScript.IsEnabled = !isRagdoll;
        }
    }
}
