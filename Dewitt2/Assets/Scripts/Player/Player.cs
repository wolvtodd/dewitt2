using UnityEngine;
using System.Collections;

public enum Direction
{
	kLeft = -1,
	kRight = 1,

	kMax,
}

public class Player : MonoBehaviour
{
	private PlayerMotor m_motor;
	private Direction m_direction;
	private bool m_isMoving;

	void Start ()
	{
		m_motor = gameObject.GetComponent<PlayerMotor>();
		m_direction = transform.localEulerAngles.y % 360 == 0 ? Direction.kRight : Direction.kLeft;
    }
	
	void Update ()
	{
		float dt = Time.deltaTime;

		UpdateMovement(dt);
		UpdateClimbLadder(dt);
		UpdateJumpInput();
    }

	void UpdateMovement(float dt)
	{
		Vector3 moveVector = GetMovementInput();

		UpdateDirection(moveVector, dt);
        if (m_motor && !m_motor.isOnLadder())
			m_motor.UpdateMovement(ref moveVector, dt);
	}

	void UpdateClimbLadder(float dt)
	{
		if (m_motor && m_motor.canClimbLadder())
		{
			Vector3 climbVector = GetClimbInput();
			if (climbVector.y != 0.0f)
			{
				if (m_motor)
				{
					m_motor.setIsOnLadder(true);
					m_motor.UpdateClimb(ref climbVector, dt);
					return;
				}
			}
		}
	}

	Vector3 GetMovementInput()
	{
		var deadZone = 0.1f;
		Vector3 moveVector = Vector3.zero;
		moveVector.x = Input.GetAxis("Horizontal");

		if (Mathf.Abs(moveVector.x) > deadZone)
		{
			m_isMoving = true;
            return moveVector;
		}

		m_isMoving = false;
		return Vector3.zero;
	}

	Vector3 GetClimbInput()
	{
		var deadZone = 0.3f;
		Vector3 moveVector = Vector3.zero;
		moveVector.y = Input.GetAxis("Vertical");

		if (Mathf.Abs(moveVector.y) > deadZone)
			return moveVector;

		return Vector3.zero;
	}

	public bool IsPlayerMoving()
	{
		return m_isMoving;
    }

	void UpdateJumpInput()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (m_motor)
				m_motor.Jump();
		}
	}

	void UpdateDirection(Vector3 moveVector, float dt)
	{
		bool dirChanged = false;
		switch (m_direction)
		{
			case Direction.kLeft:
				if (moveVector.x > 0.0f)
				{
					m_direction = Direction.kRight;
					dirChanged = true;
                }
				break;
			case Direction.kRight:
				if (moveVector.x < 0.0f)
				{
					m_direction = Direction.kLeft;
					dirChanged = true;
                }
				break;
			default:
				break;
		}

		if (m_motor)
			m_motor.UpdateDirectionRotate(m_direction, dirChanged, dt);
    }

	public Direction getDirection()
	{
		return m_direction;
	}

	public void setCanClimbLadder(bool canClimb)
	{
		if (m_motor)
			m_motor.setCanClimbLadder(canClimb);
	}
}
