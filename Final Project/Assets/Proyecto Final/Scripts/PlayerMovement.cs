using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private CharacterController controller;
	private Vector2 axis;
	public float speed;
	public Vector3 moveDirection;
	private float forceToGround = Physics.gravity.y;

	public float jumpSpeed;
	private bool jump;
	public float gravityMagnitude = 1.0f;

	void Start ()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update ()
	{
		if(controller.isGrounded && !jump)
		{
			moveDirection.y = forceToGround;
		}
		else
		{
			jump = false;
			moveDirection.y += Physics.gravity.y * gravityMagnitude * Time.deltaTime;
		}

		Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward;

		moveDirection.x = transformDirection.x * speed;
		moveDirection.z = transformDirection.z * speed;

		controller.Move(moveDirection * Time.deltaTime);
	}

	public void SetAxis(Vector2 inputAxis)
	{
		axis = inputAxis;
	}

	public void StartJump()
	{
		if(!controller.isGrounded) return;

		moveDirection.y = jumpSpeed;
		jump = true;
	}
}
