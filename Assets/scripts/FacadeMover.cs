using UnityEngine;
using System.Collections;

public class FacadeMover : MonoBehaviour {

	public float	speed,
					timeToWaitAtStops;

	float			stopTime;

	void Update () {

		if(stopTime <= 0) {
			transform.position += Vector3.back * speed * Time.deltaTime;
		} else {
			stopTime -= Time.deltaTime;
		}
	}

	public void stop() {

		stopTime = timeToWaitAtStops;
	}

	public void reset() {

		transform.position = Vector3.zero;
	}
}
