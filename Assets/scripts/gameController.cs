using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class gameController : MonoBehaviour {
	
	public dialogueScript dialoguescript;

	public int day = 0;

	public float timeToStop;

	public bool dayStarted = false;

	void Awake() {
		DontDestroyOnLoad(this.gameObject); 
	}

	void Start() {
		dialoguescript = GameObject.FindGameObjectWithTag("Player").GetComponent<dialogueScript>(); 
	}

	void Update() {

		if(dayStarted == false) {
			day = day + 1; 
			dayStarted = true; 
		} else if(dayStarted == true) {
			timeToStop -= Time.deltaTime;
		}

		if(timeToStop <= 0) {
			Invoke("endDay", 0);
		}
	}

	void endDay() {
 
	}
}
