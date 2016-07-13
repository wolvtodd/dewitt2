using UnityEngine;
using System.Collections;

public class TargetLookAt : MonoBehaviour
{
	private Transform	m_targetTransform;
	private Player		m_targetPlayer;
	private float		m_distanceToTarget;

	// smooth related
	private Vector3		m_positionVelocity;
	private Vector3		m_directionVelocity;
	private float		m_positionSmoothTime;
	private float		m_directionSmoothTime;
	private Vector3		m_directionPosition;

	void Start()
	{
		SetDistance(2.5f);
		m_positionSmoothTime = 0.1f;
		m_directionSmoothTime = 0.5f;

		GameObject target = GameObject.Find("Player");
		if (target != null)
		{
			m_targetTransform = target.GetComponent<Transform>() as Transform;
			m_targetPlayer = target.GetComponent<Player>() as Player;
        }
    }
	
	void Update()
	{
		UpdatePosition();
    }

	void UpdatePosition()
	{
		if (m_targetTransform && m_targetPlayer)
		{
			int dir = (int)m_targetPlayer.getDirection();

			if (m_targetPlayer.IsPlayerMoving())
				m_directionPosition = Vector3.SmoothDamp(m_directionPosition, m_targetTransform.right * dir * m_distanceToTarget, ref m_directionVelocity, m_directionSmoothTime);

			Vector3 desiredDirPosition = transform.position - m_targetTransform.position;

			desiredDirPosition = Vector3.SmoothDamp(desiredDirPosition, m_directionPosition, ref m_positionVelocity, m_positionSmoothTime);			
			transform.position = m_targetTransform.position + desiredDirPosition;
			transform.localEulerAngles = m_targetTransform.localEulerAngles;
		}
	}

	public void SetDistance(float distance)
	{
		m_distanceToTarget = distance;
	}
}
