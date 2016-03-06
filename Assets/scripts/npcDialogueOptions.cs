using UnityEngine;
using System.Collections;

public class npcDialogueOptions : MonoBehaviour 
{
    public string characterName;

    public string playerOption1;
    public string playerOption2;
    public string playerOption3;

    public string dialogueIntro;
    public string dialogueOutro;
    public string weAlreadyTalkedBro; 

    //explain responses
    public string explain1;
    public string explain2;
    public string explain3;
    public string explain4;

    //Give Item Responses
    public string isGivenItem;
    public string alreadyHasItem;

    public bool canGiveItem;
    public bool hasTalked = false;

    public int totalDialoguePhases; 

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
