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


    private Rigidbody rigidbody;
    private Animator PlayerAnimator;

    Vector3 inputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    public Vector2 lookInput = Vector2.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
    }

	private void Update()
	{
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical") ).normalized;
        if (!(inputVector.magnitude > 0)) MoveDirection = Vector3.zero;
        MoveDirection = transform.forward * inputVector.z + transform.right * inputVector.x;
        Vector3 MovementDirection = MoveDirection * WalkSpeed * Time.deltaTime;
        transform.position += MovementDirection;
    }
}
