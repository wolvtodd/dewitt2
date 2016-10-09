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
		// catmull
		// q(t) = 0.5 * ((2 * P1) + (-P0 + P2) * t + (2 * P0 - 5 * P1 + 4 * P2 - P3) * t2 + (-P0 + 3 * P1 - 3 * P2 + P3) * t3);
		Path targetPath = target as Path;
		if (targetPath != null)
		{
			float time = 0.0f;
			while (time <= 1.0f)
			{
				Vector3 pos = targetPath.GetPositionOnCurve(time);
				Handles.SphereCap(0, pos, Quaternion.identity, 0.1f);
				time += 0.01f;
			}
		}
	}
}
