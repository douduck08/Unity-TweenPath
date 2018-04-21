using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTweening : MonoBehaviour {

	public TweenPath path;
	public float duration = 1f;

	float time;
	void Update () {
		this.transform.position = path.Evaluate (time / duration);
		time += Time.deltaTime;
		time %= duration;
	}
}
