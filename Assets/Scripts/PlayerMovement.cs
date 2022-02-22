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
    private float JumpForce ;

    [SerializeField]
    private float gravity ;

    public Vector3 velocity;
    private CharacterController characterController;
    private Animator PlayerAnimator;

    Vector3 inputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    public Vector2 lookInput = Vector2.zero;

    bool isMoving = false;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();
    }

	private void Update()
	{
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical") ).normalized;
        if (!(inputVector.magnitude > 0))
        {
            MoveDirection = Vector3.zero;
            isMoving = false;
        }
        else
		{
            isMoving = true;
		}
        PlayerAnimator.SetBool("IsMoving", isMoving);
        velocity.y += gravity * Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
		{
            velocity.y = JumpForce;
		}
        MoveDirection = transform.forward * inputVector.z + transform.right * inputVector.x + velocity.y *Vector3.up;
        Vector3 MovementDirection = MoveDirection * WalkSpeed * Time.deltaTime ;
        characterController.Move(MovementDirection);

    }
}
