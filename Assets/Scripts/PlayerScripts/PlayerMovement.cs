using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Movement
        if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        MoveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : WalkSpeed;
        Vector3 MovementDirection = MoveDirection * currentSpeed * Time.deltaTime;
        transform.position += MovementDirection;
        PlayerAnimator.SetFloat(movementXHash, inputVector.x);
        PlayerAnimator.SetFloat(movementYHash, inputVector.y);
        //Aiming/Looking
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * AimSensetivity, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * AimSensetivity, Vector3.left);
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

        if (Input.GetButtonDown("Jump"))
        {
            if (playerController.isJumping)
            {
                return;
            }
            playerController.isJumping = true;
            rigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
            PlayerAnimator.SetBool(isJumpingHash, playerController.isJumping);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        PlayerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }
}
