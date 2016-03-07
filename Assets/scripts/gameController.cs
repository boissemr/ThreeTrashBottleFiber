using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public Dialogue	dialoguescript;
	public FacadeMover facade;

	public int		busFare,
					day = 0,
					stop = 0;

	public float[]	timeBetweenStops;

	float			timeToNextStop;
	int				nextStop;
	List<Npc>[]		allNpcs;

	// items
	public bool		hasWallet,
					hasResume,
					hasPalmReading,
					hasSketchbook,
					hasSketches;

	void Start() {

		// initialize
		nextStop = 0;
		timeToNextStop = 0;
		hasWallet = false;
		hasResume = false;
		hasPalmReading = false;
		hasSketchbook = false;
		hasSketches = false;

		// keep gameController persistent
		DontDestroyOnLoad(this.gameObject);

		// oh jeez
		allNpcs = new List<Npc>[7];
		for(int i = 0; i < allNpcs.Length; i++) allNpcs[i] = new List<Npc>();

		// find all NPCS and sort scripts by day
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("NPC")) {	// all GameObjects that have Npc scripts
			foreach(Npc script in o.GetComponents<Npc>()) {					// all Npc scripts
				for(int i = 0; i < script.daysToTriggerOn.Length; i++) {	// 7 times for 7 days
					if(script.daysToTriggerOn[i]) {							// if script is set to trigger on this day
						allNpcs[i].Add(script);
					}
				}
			}
		}

		// start with no NPCs on the bus
		clearAllNpcs();
	}

	void Update() {

		// count down until we arrive at the next stop
		timeToNextStop -= Time.deltaTime;

		if(timeToNextStop <= 0) {

			// stop animation
			facade.stop();

			// check who should or shouldn't be on the bus
			checkNpcs();

			// assign next stop
			nextStop++;

			// if we have done all the stops, on to next day
			if(nextStop > timeBetweenStops.Length) {
				// TODO: add day transition
				nextStop = 0;
				clearAllNpcs();
				checkNpcs();
				facade.reset();
				day++;
				Debug.Log("It is now day " + day);
			}

			// reset timer
			timeToNextStop += timeBetweenStops[nextStop];

			// debug
			Debug.Log("We just arrived at " + (nextStop - 1) + ". The next stop is " + nextStop + ".\nTime to next stop: " + timeToNextStop);
		}

		if(Input.GetButtonDown("Skip")) {
			Debug.Log("SKIP");
			timeToNextStop = 0;
		}
	}
	
	void checkNpcs() {
		foreach(Npc o in allNpcs[day]) {
			if(o.onStop == nextStop) {
				Debug.Log(o.characterName + " entered.");
				o.enter();
			}
			if(o.offStop == nextStop) {
				Debug.Log(o.characterName + " left.");
				o.leave();
			}
		}
	}

	void clearAllNpcs() {
		for(int i = 0; i < 7; i++) {
			foreach(Npc o in allNpcs[i]) o.leave();
		}
	}
}
