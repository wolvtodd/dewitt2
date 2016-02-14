using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	private Transform m_targetLookAt;
	private Vector3 m_cameraRelativePosition; // relative to targetLookAt

	void Start()
	{
		GameObject targetLookAtObj = GameObject.Find("TargetLookAt");
		if (targetLookAtObj != null)
			m_targetLookAt = targetLookAtObj.transform;

		SetCameraRelativePosition(new Vector3(-15.0f, 5.0f, -15.0f));
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

	public void SetCameraRelativePosition(Vector3 position)
	{
		m_cameraRelativePosition = position;
	}
}
