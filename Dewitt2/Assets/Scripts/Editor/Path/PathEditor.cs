using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Path))]
[CanEditMultipleObjects()]
public class PathEditor : Editor
{
	private SerializedProperty pathNodesProperty = null;

	void OnEnable()
	{
		if (pathNodesProperty == null)
			pathNodesProperty = serializedObject.FindProperty("m_pathNodes");
	}

	void OnSceneGUI()
	{
		// cutmoll
		// q(t) = 0.5 * ((2 * P1) + (-P0 + P2) * t + (2 * P0 - 5 * P1 + 4 * P2 - P3) * t2 + (-P0 + 3 * P1 - 3 * P2 + P3) * t3);
		Path targetPath = target as Path;
		if (targetPath != null)
		{
			List<PathNode> nodes = targetPath.GetPathNodes();
			if (nodes != null && nodes.Count >= 4)
			{
				// draw handlers
				for (int i = 0; i < nodes.Count; ++i)
				{
					if (nodes[i] != null)
					{
						Handles.color = new Color(1.0f, 0.2f, 0.2f, 1.0f);
						Handles.SphereCap(i, nodes[i].position, Quaternion.identity, 0.5f);
					}
				}

				// draw curve
				for (int i = 1; i < nodes.Count - 2; ++i)
				{
					if (nodes[i].snapToNext)
					{
						PathNode p0 = nodes[i];
						PathNode p1 = nodes[i + 1];
						if (p0 != null && p1 != null)
						{
							float time = 0.0f;
							while (time < 1.0f)
							{
								Vector3 result = p0.position + (p1.position - p0.position) * time;
								Handles.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
								Handles.SphereCap(i, result, Quaternion.identity, 0.2f);
								time += 0.01f;
							}
						}
					}
					else
					{
						PathNode p0 = nodes[i - 1];
						PathNode p1 = nodes[i];
						PathNode p2 = nodes[i + 1];
						PathNode p3 = nodes[i + 2];

						if (p0 != null && p1 != null && p2 != null && p3 != null)
						{
							float time = 0.0f;
							while (time < 1.0f)
							{
								Vector3 result =
									0.5f *
								   ((2.0f * p1.position) +
									(-p0.position + p2.position) * time +
									(2.0f * p0.position - 5.0f * p1.position + 4.0f * p2.position - p3.position) * (time * time) +
									(-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * (time * time * time));

								Handles.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
								Handles.SphereCap(i, result, Quaternion.identity, 0.2f);

								time += 0.01f;
							}
						}
					}
				}
			}
		}
	}
}
