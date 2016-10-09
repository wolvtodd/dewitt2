using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PathNode
{
	public PathNode(Vector3 pos, bool snap, int seg)
	{
		segment = seg;
		position = pos;
		snapToNext = snap;
	}

	public int segment;
	public bool snapToNext;
	public Vector3 position;
}

public struct PathClip
{
	public PathNode startNode;
	public PathNode endNode;
	public float length;
}

public class Path : MonoBehaviour
{
	#region Exposed
	[SerializeField]
	private int m_segmentPerNode = 10;
	[SerializeField]
	private List<PathNode> m_pathNodes = new List<PathNode>();
	#endregion

	void Start()
	{
	}

	void Update()
	{

	}

	void InitPathClips()
	{
		if (m_pathNodes != null && m_pathNodes.Count > 0)
		{
			for (int i = 0; i < m_pathNodes.Count - 1; ++i)
			{
				PathNode start = m_pathNodes[i];
				PathNode end = m_pathNodes[i + 1];
				if (start != null && end != null)
				{
					PathClip clip = new PathClip();
					clip.startNode = start;
					clip.endNode = end;
					if (start.snapToNext)
						clip.length = Vector3.Distance(clip.startNode.position, clip.endNode.position);
					else
					{
						// calculate the length on the whole curve,
						// based on segment.
					}
				}
			}
		}
	}

	public Vector3 GetPositionOnCurve(float time)
	{
		int sideControlPointCount = 2;
		int minNodeCount = 4;
		if (m_pathNodes != null && m_pathNodes.Count >= minNodeCount)
		{
			float trippedTime = time;
			int startIndex = 1;

			// remove head and tail nodes coz they are for controls,
			// and reduce one coz we need the clip counts not nodes.
			int validNodeCount = m_pathNodes.Count - sideControlPointCount - 1;
			for (int i = 0; i < validNodeCount; ++i)
			{
				if ((float)(i + 1) / (float)validNodeCount >= time)
				{
					trippedTime = (time - (float)i / (float)validNodeCount) * validNodeCount;
					startIndex = i + 1;
					break;
				}
			}
			return GetPositionOnClip(trippedTime, startIndex);
		}

		return Vector3.zero;
	}

	Vector3 GetPositionOnClip(float time, int startIndex)
	{
		int minNodeCount = 4;
		if (m_pathNodes != null && m_pathNodes.Count >= minNodeCount)
		{
			PathNode startNode = m_pathNodes[startIndex];
			if (startNode != null)
			{
				if (startNode.snapToNext)
				{
					PathNode p0 = startNode;
					PathNode p1 = m_pathNodes[startIndex + 1];
					if (p0 != null && p1 != null)
						return p0.position + (p1.position - p0.position) * time;
				}
				else
				{
					PathNode p0 = m_pathNodes[startIndex - 1];
					PathNode p1 = startNode;
					PathNode p2 = m_pathNodes[startIndex + 1];
					PathNode p3 = m_pathNodes[startIndex + 2];

					if (p0 != null && p1 != null && p2 != null && p3 != null)
					{
						// catmull curve, 
						// controlled by p0 and p3, connects p1 and p2.
						// q(t) = 0.5 * ((2 * P1) + (-P0 + P2) * t + (2 * P0 - 5 * P1 + 4 * P2 - P3) * t2 + (-P0 + 3 * P1 - 3 * P2 + P3) * t3);
						return
							0.5f * ((2.0f * p1.position) +
							(-p0.position + p2.position) * time +
							(2.0f * p0.position - 5.0f * p1.position + 4.0f * p2.position - p3.position) * (time * time) +
							(-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * (time * time * time));
					}
				}
			}
		}
		return Vector3.zero;
	}

	List<PathNode> GetPathNodes() { return m_pathNodes; }
}