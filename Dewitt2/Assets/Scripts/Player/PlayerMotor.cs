using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController m_characterController;
	
	private float m_walkSpeed;
	private float m_verticalSpeed;
	private float m_jumpSpeed;
	private float m_gravity;
	
	private void Start ()
	{
		m_characterController = gameObject.AddComponent<CharacterController>() as CharacterController;

		m_walkSpeed = 14.0f;
		m_gravity	= 98.0f;
		m_jumpSpeed = 28.0f;
    }

	public void UpdateMovement(ref Vector3 moveVector)
	{
		if (moveVector.magnitude > 1)
			moveVector.Normalize();

		moveVector *= m_walkSpeed;
		moveVector = transform.TransformVector(moveVector);

		ApplyGravity(ref moveVector);
		Move(moveVector);
    }

	public void Jump()
	{
		if (m_characterController && m_characterController.isGrounded)
			m_verticalSpeed = m_jumpSpeed;
	}

	private void ApplyGravity(ref Vector3 moveVector)
	{
		m_verticalSpeed -= m_gravity * Time.deltaTime;
		moveVector.y = m_verticalSpeed;

		if (m_characterController && m_characterController.isGrounded)
		{
			if (m_verticalSpeed < -1.0f)
				m_verticalSpeed = -1.0f;
		}
    }

	private void Move(Vector3 moveVector)
	{
		if (m_characterController)
			m_characterController.Move(moveVector * Time.deltaTime);
	}
}
