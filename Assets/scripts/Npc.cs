using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour {
	
	public string	characterName;

	// responses
	public string[]	options, explains;
	public string	dialogueIntro,
					dialogueOutro,
					weAlreadyTalkedBro,
					isGivenItem,
					alreadyHasItem,
					ifYes,
					ifNo;

	public bool 	canGiveItem,
					hasTalked = false,
					introIsQuestion,
					giveItemAfterIntro,
					exitAfterIntro;

	public int		onStop,
					offStop;

	public bool[]	daysToTriggerOn;

	MeshRenderer r;
	BoxCollider box;

	void Start() {
		try {
			r = GetComponentInChildren<MeshRenderer>();
		} catch {
			//Debug.Log("Couldn't find MeshRenderer in " + characterName);
		}

		box = GetComponent<BoxCollider>();
	}

	public void enter() {
		try {
			r.enabled = true;
			box.enabled = true;
		} catch {
			//Debug.Log("Couldn't find MeshRenderer in " + characterName);
		}
	}

	public void leave() {
		try {
			r.enabled = false;
			box.enabled = false;
		} catch {
			//Debug.Log("Couldn't find MeshRenderer in " + characterName);
		}
	}
}
