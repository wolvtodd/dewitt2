using UnityEngine;
using System.Collections;

public class TargetLookAt : MonoBehaviour
{
	//
	private Transform	m_targetTransform;
	private Player		m_targetPlayer;

	//
	private float		m_distanceToTarget;

	// smooth related
	private Vector3		m_currentVelocity;
	private float		m_smoothTime;

	//
	private Vector3		m_directionPosition;

	void Start()
	{
		SetDistance(2.5f);
		m_smoothTime = 0.1f;

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
			m_directionPosition = m_targetTransform.right * dir * m_distanceToTarget; ;
			Vector3 desiredDirPosition = Vector3.SmoothDamp(transform.position - m_targetTransform.position, m_directionPosition, ref m_currentVelocity, m_smoothTime);

			transform.position = m_targetTransform.position + desiredDirPosition;
			transform.localEulerAngles = m_targetTransform.localEulerAngles;
		}
	}

	public void SetDistance(float distance)
	{
		m_distanceToTarget = distance;
	}
}
