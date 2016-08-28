using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PathNode
{
	public PathNode(Vector3 pos, bool snap)
	{
		position = pos;
		snapToNext = snap;
	}

	public bool snapToNext;
	public Vector3 position;
}

public class Path : MonoBehaviour
{
	#region Exposed
	[SerializeField] private List<PathNode> m_pathNodes = new List<PathNode>();
	#endregion

	void Start()
	{

	}

	void Update()
	{

	}

	public Vector3 GetPositionByTime(float t)
	{
		return Vector3.zero;
	}

	public Quaternion GetRotationByTime(float t)
	{
		return Quaternion.identity;
	}

	public List<PathNode> GetPathNodes() { return m_pathNodes; }
}