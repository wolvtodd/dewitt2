using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController m_characterController;

	#region Exposed
	[SerializeField] private float m_walkSpeed = 14.0f;
	[SerializeField] private float m_jumpSpeed = 28.0f;
	[SerializeField] private float m_climbSpeed = 10.0f;
	[SerializeField] private float m_gravity = 98.0f;
	#endregion

	#region Field
	private float m_verticalSpeed;
	private bool m_canClimbLadder;
	private bool m_isOnLadder;
	#endregion

	private void Start ()
	{
		m_characterController = gameObject.AddComponent<CharacterController>() as CharacterController;
		m_canClimbLadder = false;
		m_isOnLadder = false;
    }

	public void UpdateMovement(ref Vector3 moveVector)
	{
		if (moveVector.magnitude > 1)
			moveVector.Normalize();

		moveVector *= m_walkSpeed;
		moveVector = transform.TransformVector(moveVector);

		if (!m_isOnLadder)
			ApplyGravity(ref moveVector);

		Move(moveVector);
    }

	public void UpdateClimb(ref Vector3 climbVector)
	{
		if (climbVector.magnitude > 1)
			climbVector.Normalize();

		climbVector *= m_climbSpeed;
		climbVector = transform.TransformVector(climbVector);

		Move(climbVector);

		if (m_characterController && m_characterController.isGrounded)
			m_isOnLadder = false;
	}

	public void Jump()
	{
		if (canJump())
		{
			m_verticalSpeed = m_jumpSpeed;
			m_isOnLadder = false;
		}
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

	private bool canJump()
	{
		return	(m_characterController && m_characterController.isGrounded) ||
				m_isOnLadder;
    }

	public void setCanClimbLadder(bool canClimb)
	{
		m_canClimbLadder = canClimb;
	}

	public bool canClimbLadder()
	{
		return m_canClimbLadder;
	}

	public void setIsOnLadder(bool isOnLadder)
	{
		m_isOnLadder = isOnLadder;
	}

	public bool isOnLadder()
	{
		return m_isOnLadder;
	}
}
