using UnityEngine;
using System.Collections;

public class TargetLookAt : MonoBehaviour
{
	private Transform	m_targetTransform;
	private Player		m_targetPlayer;

	private float		m_distanceToTarget;

	void Start ()
	{
		m_distanceToTarget = 5.0f;

		GameObject target = GameObject.Find("Player");
		if (target != null)
		{
			m_targetTransform = target.GetComponent<Transform>() as Transform;
			m_targetPlayer = target.GetComponent<Player>() as Player;
        }
    }
	
	void Update ()
	{
		UpdatePosition();
    }

	void UpdatePosition()
	{
		if (m_targetTransform && m_targetPlayer)
		{
			int dir = (int)m_targetPlayer.getDirection();

			transform.position = 
				m_targetTransform.position + 
				m_targetTransform.right * dir * m_distanceToTarget;
		}
	}
}
