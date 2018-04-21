using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPath : MonoBehaviour {

	public AnimationCurve x;
	public AnimationCurve y;
	public AnimationCurve z;
	public Bounds bounds;

	Matrix4x4 m_matrix;

	private void Awake() {
		 UpdateMatrix ();
	}

	public void UpdateMatrix () {
		m_matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
	}

	public Vector3 Evaluate (float t) {
		Vector3 pos = bounds.min;
		pos.x += x.Evaluate (t) * bounds.size.x;
		pos.y += y.Evaluate (t) * bounds.size.y;
		pos.z += z.Evaluate (t) * bounds.size.z;
		pos = m_matrix.MultiplyPoint (pos);
		return pos;
	}

	public Vector3 LocalEvaluate (float t) {
		Vector3 pos = bounds.min;
		pos.x += x.Evaluate (t) * bounds.size.x;
		pos.y += y.Evaluate (t) * bounds.size.y;
		pos.z += z.Evaluate (t) * bounds.size.z;
		return pos;
	}

#if UNITY_EDITOR
	void OnDrawGizmos () {
		UpdateMatrix ();
		Gizmos.matrix = m_matrix;
		Gizmos.color = Color.red;
		for (int i = 0; i < 100; i++) {
			Gizmos.DrawLine (LocalEvaluate(0.01f * i), LocalEvaluate(0.01f * i + 0.01f));
		}
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (bounds.center, bounds.size);
	}
#endif
}
