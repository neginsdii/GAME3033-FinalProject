using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed = 5;
    [SerializeField]
    private float runSpeed = 10;
    [SerializeField]
    private float JumpForce = 5;

    //Components
    private PlayerController playerController;
    private Rigidbody rigidbody;
    private Animator PlayerAnimator;
    public GameObject followTarget;

    Vector2 inputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    public Vector2 lookInput = Vector2.zero;
    public float AimSensetivity = 1;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");
    public readonly int VerticalAimHash = Animator.StringToHash("AimVertical");

    private void Awake()
    {

        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    void Update()
    {
        //Movement
        if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        MoveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : WalkSpeed;
        Vector3 MovementDirection = MoveDirection * currentSpeed * Time.deltaTime;
        transform.position += MovementDirection;

        //Aiming/Looking
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * AimSensetivity, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * AimSensetivity, Vector3.left);
        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;
        var angle = followTarget.transform.localEulerAngles.x;
        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }
        followTarget.transform.localEulerAngles = angles;

        //player rotation
        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        PlayerAnimator.SetFloat(movementXHash, inputVector.x);
        PlayerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    public void OnJump(InputValue value)
    {
        if (playerController.isJumping)
        {
            return;
        }
        playerController.isJumping = value.isPressed;
        rigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        PlayerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }
    public void onAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }


    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        PlayerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        PlayerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }
}
