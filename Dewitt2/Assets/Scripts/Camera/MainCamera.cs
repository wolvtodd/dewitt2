using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	private Transform m_targetLookAt;
	[SerializeField] private Vector3 m_cameraRelativePosition; // relative to targetLookAt

	void Start()
	{
		InitTargetLookAt();
    }

	void LateUpdate()
	{
		if (m_targetLookAt)
		{
			transform.position =	m_targetLookAt.position +
									m_targetLookAt.TransformDirection(m_cameraRelativePosition);
			transform.LookAt(m_targetLookAt);
		}
	}

	void InitTargetLookAt()
	{
		GameObject targetLookAtObj = GameObject.Find("TargetLookAt");
		if (targetLookAtObj != null)
			m_targetLookAt = targetLookAtObj.transform;
	}

	public void SetCameraRelativePosition(Vector3 position)
	{
		m_cameraRelativePosition = position;
	}
}
