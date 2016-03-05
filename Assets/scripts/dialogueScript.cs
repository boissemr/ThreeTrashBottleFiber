using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class dialogueScript : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject questionButtons;
    
    //dialogue options
    public Text Option1;
    public Text Option2;
    public Text Option3;
    public Text npcName; 


    private bool isInteracting = false;
    public FirstPersonController playerController;
    public npcDialogueOptions npc; 

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "NPC" && isInteracting == false)
        {
            npc = other.gameObject.GetComponent<npcDialogueOptions>(); 
            Invoke("startInteraction", 0);
            
            interactButton.SetActive(true);
            npcName.text = npc.characterName; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            questionButtons.SetActive(false);
            interactButton.SetActive(false);
            isInteracting = false; 
        }
    }

    void startInteraction()
    {
        if (Input.GetKeyDown("e"))
        {
            isInteracting = true; 
            interactButton.SetActive(false);
            Invoke("askQuestions", 0);
        }
    }

    void askQuestions()
    {
        Option1.text = npc.dialogueOption1;
        Option2.text = npc.dialogueOption2;
        Option3.text = npc.dialogueOption3;
        
        

        playerController.m_WalkSpeed = 0;
        playerController.m_MouseLook.XSensitivity = 0;
        playerController.m_MouseLook.YSensitivity = 0; 
        questionButtons.SetActive(true);
        Cursor.visible = true;
    }

}
