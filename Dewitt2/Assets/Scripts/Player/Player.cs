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

	void Start ()
	{
		m_motor = gameObject.AddComponent<PlayerMotor>() as PlayerMotor;
		m_direction = transform.localEulerAngles.y % 360 == 0 ? Direction.kRight : Direction.kLeft;
    }
	
	void Update ()
	{
		UpdateMovement();
		UpdateJumpInput();
    }

	void UpdateMovement()
	{
		Vector3 moveVector = getMovementInput();

		UpdateDirection(moveVector);
        if (m_motor)
			m_motor.UpdateMovement(ref moveVector);
	}

	Vector3 getMovementInput()
	{
		var deadZone = 0.1f;
		Vector3 moveVector = Vector3.zero;
		moveVector.x = Input.GetAxis("Horizontal");

		if (Mathf.Abs(moveVector.x) > deadZone)
			return moveVector;

		return Vector3.zero;
	}

	void UpdateJumpInput()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (m_motor)
				m_motor.Jump();
		}
	}

	void UpdateDirection(Vector3 moveVector)
	{
		switch (m_direction)
		{
			case Direction.kLeft:
				if (moveVector.x > 0.0f)
					m_direction = Direction.kRight;
				break;
			case Direction.kRight:
				if (moveVector.x < 0.0f)
					m_direction = Direction.kLeft;
				break;
			default:
				break;
		}
	}

	public Direction getDirection()
	{
		return m_direction;
	}
}
