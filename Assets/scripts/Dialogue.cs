using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Dialogue : MonoBehaviour {

	// parameters
	public GameObject	interactButton,
						explainButton,
						yesButton,
						noButton,
						giveItemButton,
						npcDialogue;
	public int			dialoguePhase,
						money;
	public Text			npcName,
						dialogue1,
						moneyUI;
	public Text[]		options;
	public Npc			npc;
	public GameController gameController;

	// variables
	FirstPersonController playerController;
	bool				isInteracting;

	void Start() {

		// keep gameController persistent
		DontDestroyOnLoad(this.gameObject);

		// initalization
		isInteracting = false;
		playerController = GetComponent<FirstPersonController>();
		setPlayerController(true);
	}

	void Update() {
		
		// interaction with NPC
		if(Input.GetKeyDown("space")) {
			
			if(dialogue1.text == npc.dialogueIntro) {
				if(npc.giveItemAfterIntro) {
					gameController.hasPalmReading = true;
				}
				if(npc.exitAfterIntro) {
					Debug.Log("Exited after intro");
					exitDialogue();
				} else {
					npcDialogue.SetActive(false);
					askQuestions();
				}

			} else if(dialogue1.text == npc.weAlreadyTalkedBro) {
				setPlayerController(true);
				npcDialogue.SetActive(false);

			} else if((!npc.introIsQuestion) && (dialogue1.text == npc.explains[0] || dialogue1.text == npc.explains[1] || dialogue1.text == npc.explains[2])) {
				npcDialogue.SetActive(false);
				setPlayerButtons(true, true, true);

			} else if(dialogue1.text == npc.dialogueOutro) {
				setPlayerController(true);
				npcDialogue.SetActive(false);

			} else if(	(dialogue1.text == npc.isGivenItem || dialogue1.text == npc.ifYes || dialogue1.text == npc.ifNo) ||
						(dialoguePhase == npc.explains.Length && npc.characterName != "Bus Driver")) {
				exitDialogue();
			}
		}
	}

	void OnTriggerStay(Collider other) {
		
		if(other.gameObject.tag == "NPC" && isInteracting == false) {

			// set NPC
			foreach(Npc o in other.gameObject.GetComponents<Npc>()) {
				if(o.daysToTriggerOn[gameController.day]) {
					npc = o;
				}
			}
			if(Input.GetKeyDown("e")) {
				isInteracting = true; 
				interactButton.SetActive(false);
				npcResponse();
			}
            
			interactButton.SetActive(true);
			npcName.text = npc.characterName; 
		}
	}

	void OnTriggerExit(Collider other) {

		// set npc
		if(other.gameObject.tag == "NPC") {
			setPlayerButtons(false, false, false);
			interactButton.SetActive(false);
			isInteracting = false;
		}
	}

	void npcResponse() {
		setPlayerController(false);
		npcDialogue.SetActive(true);

		if(npc.hasTalked == false) {
			dialogue1.text = npc.dialogueIntro;
		}
		else if(npc.hasTalked == true) {
			dialogue1.text = npc.weAlreadyTalkedBro;
		}    
	}

	// player options
	void askQuestions() {

		if(npc.introIsQuestion) {
			setPlayerButtons(false, true, false);
		} else {
			dialoguePhase = 1; 
			setPlayerButtons(true, false, true);
			for(int i = 0; i < options.Length; i++) {
				options[i].text = npc.options[i];
			}
		}
	}

	// if explain selected
	public void explain() {
		
		npcDialogue.SetActive(true);
		setPlayerButtons(false, false, false);
		dialogue1.text = npc.explains[dialoguePhase - 1];
		dialoguePhase++;
	}

	// if give item selected
	public void giveItem() {
		
		if(npc.canGiveItem) { // NPC accepts item

			setPlayerButtons(false, false, false);
			dialogue1.text = npc.isGivenItem;
			npcDialogue.SetActive(true);

			// item interaction switch
			switch(npc.characterName) {
				case "Bus Driver":
					addMoney(-gameController.busFare);
					break;
			}
		} else { // NPC refuses item
			setPlayerButtons(false, false, false);
			dialogue1.text = npc.alreadyHasItem;
			npcDialogue.SetActive(true);
		}
	}

	// if yes selected
	public void yes() {

		npcDialogue.SetActive(true);
		setPlayerButtons(false, false, false);
		dialogue1.text = npc.ifYes;
	}

	// if no selected
	public void no() {

		npcDialogue.SetActive(true);
		setPlayerButtons(false, false, false);
		dialogue1.text = npc.ifNo;
	}

	// exit dialogue
	void exitDialogue() {
		npcDialogue.SetActive(true);
		setPlayerButtons(false, false, false);
		dialogue1.text = npc.dialogueOutro;
		npc.hasTalked = true; 
	}

	// activate or deactivate movement
	void setPlayerController(bool state) {
		playerController.enabled = state;
		Cursor.visible = !state;
	}

	// change the player's money amount
	void addMoney(int amount) {
		money += amount;
		moneyUI.text = "$" + money;
	}

	void setPlayerButtons(bool explain, bool yesno, bool item) {

		//Debug.Log("Setting player buttons: " + explain + ", " + yesno + ", " + item);

		explainButton.SetActive(explain);
		yesButton.SetActive(yesno);
		noButton.SetActive(yesno);
		giveItemButton.SetActive(item);
	}
}
