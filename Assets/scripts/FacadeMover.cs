using UnityEngine;
using System.Collections;

public class FacadeMover : MonoBehaviour {

	public float speed;

	void Update () {
		transform.position += Vector3.back * speed * Time.deltaTime;
	}
}
