using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController m_characterController;

	#region Exposed
	[SerializeField] private GameObject m_model = null;

	[SerializeField] private float m_walkSpeed = 14.0f;
	[SerializeField] private float m_jumpSpeed = 28.0f;
	[SerializeField] private float m_climbSpeed = 10.0f;
	[SerializeField] private float m_gravity = 98.0f;
	[SerializeField] private float m_rotateSpeed = 1.0f;
	#endregion

	#region Field
	private float m_verticalSpeed;
	private bool m_canClimbLadder;
	private bool m_isOnLadder;

	private float m_directionRotateTime;
	#endregion

	private void Start ()
	{
		m_characterController = gameObject.AddComponent<CharacterController>();
		m_canClimbLadder = false;
		m_isOnLadder = false;
    }

	public void UpdateMovement(ref Vector3 moveVector, float dt)
	{
		if (moveVector.magnitude > 1)
			moveVector.Normalize();

		moveVector *= m_walkSpeed;
		moveVector = transform.TransformVector(moveVector);

		if (!m_isOnLadder)
			ApplyGravity(ref moveVector, dt);

		Move(moveVector, dt);
    }

	public void UpdateDirectionRotate(Direction direction, bool dirChanged, float dt)
	{
		if (dirChanged)
			m_directionRotateTime = 0.0f;
		
		if (m_model)
		{
			//TODO: ease in and out
			m_directionRotateTime = Mathf.Clamp01(m_directionRotateTime + m_rotateSpeed * dt);
			Vector3 to = direction == Direction.kRight ? Vector3.forward : Vector3.back;
			Quaternion desiredRotation = Quaternion.LookRotation(to);
			Quaternion resultRotation = Quaternion.Slerp(m_model.transform.localRotation, desiredRotation, m_directionRotateTime);
			m_model.transform.localRotation = resultRotation;
		}
	}

	public void UpdateClimb(ref Vector3 climbVector, float dt)
	{
		if (climbVector.magnitude > 1)
			climbVector.Normalize();

		climbVector *= m_climbSpeed;
		climbVector = transform.TransformVector(climbVector);

		Move(climbVector, dt);

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

	private void ApplyGravity(ref Vector3 moveVector, float dt)
	{
		m_verticalSpeed -= m_gravity * dt;
		moveVector.y = m_verticalSpeed;

		if (m_characterController && m_characterController.isGrounded)
		{
			if (m_verticalSpeed < -1.0f)
				m_verticalSpeed = -1.0f;
		}
    }

	private void Move(Vector3 moveVector, float dt)
	{
		if (m_characterController)
			m_characterController.Move(moveVector * dt);
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
