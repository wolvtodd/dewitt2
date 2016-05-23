using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
	private LadderVolume m_ladderVolume;
	private Transform m_upperPoint;
	private Transform m_downPoint;

	// Use this for initialization
	void Start()
	{
		InitVolume();
		InitArrivingPoints();
	}

	void InitVolume()
	{
		Transform result = transform.FindChild("LadderVolume");
		if (result)
			m_ladderVolume = result.gameObject.AddComponent<LadderVolume>();
	}

	void InitArrivingPoints()
	{
		m_upperPoint = transform.FindChild("LadderPoint_upper");
		m_downPoint = transform.FindChild("LadderPoint_down");
	}

}