using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private PlayerMotor m_motor;

	void Start ()
	{
		m_motor = gameObject.AddComponent<PlayerMotor>() as PlayerMotor;
	}
	
	void Update ()
	{
		UpdateMovement();
		UpdateJumpInput();
    }

	void UpdateMovement()
	{
		Vector3 moveVector = getMovementInput();
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
}
