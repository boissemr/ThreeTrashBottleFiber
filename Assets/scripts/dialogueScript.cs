﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class dialogueScript : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject playerButtons;
    public GameObject npcDialogue;
    
    //dialogue options
    public Text npcName; 
    //npc dialogue
    public Text dialogue1;
 
    //questions
    public Text Option1;
    public Text Option2;
    public Text Option3;



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
        if (Input.GetKeyDown("space"))
        {
            if (dialogue1.text == npc.dialogueIntro)
            {
                npcDialogue.SetActive(false);

                Invoke("askQuestions", 0);

            }
            else if (dialogue1.text == npc.weAlreadyTalkedBro)
            {
                playerController.m_WalkSpeed = 5;
                playerController.m_MouseLook.XSensitivity = 2;
                playerController.m_MouseLook.YSensitivity = 2;
                npcDialogue.SetActive(false);
                Cursor.visible = false;
            }
            else if (dialogue1.text == npc.explain1)
            {
                npcDialogue.SetActive(false);
                playerButtons.SetActive(true);
            }

            else if (dialogue1.text == npc.dialogueOutro)
            {
                playerController.m_WalkSpeed = 5;
                playerController.m_MouseLook.XSensitivity = 2;
                playerController.m_MouseLook.YSensitivity = 2;
                npcDialogue.SetActive(false);
                Cursor.visible = false;
            }

            else if (dialogue1.text == npc.isGivenItem)
            {
                if (npc.gameObject.name == "Driver")
                {
                    Invoke("exitDialogue", 0);
                }
            }
 
        }
	
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
            playerButtons.SetActive(false);
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
            Invoke("npcResponse", 0); 
        }
    }

    void npcResponse()
    {
        playerController.m_WalkSpeed = 0;
        playerController.m_MouseLook.XSensitivity = 0;
        playerController.m_MouseLook.YSensitivity = 0;
        npcDialogue.SetActive(true);
        Cursor.visible = true;

        if (npc.hasTalked == false)
        {
            dialogue1.text = npc.dialogueIntro;
        }

        else if (npc.hasTalked == true)
        {
            dialogue1.text = npc.weAlreadyTalkedBro;
        }    
    }

    void askQuestions()
    {
        playerButtons.SetActive(true);
        Option1.text = npc.playerOption1;
        Option2.text = npc.playerOption2;
        Option3.text = npc.playerOption3; 
    }

    public void explain()
    {
        npcDialogue.SetActive(true);
        playerButtons.SetActive(false);

        dialogue1.text = npc.explain1;
    }

    public void yesOrNo()
    {
 
    }

   public void giveItem()
    {
        if (npc.canGiveItem == true)
        {
            playerButtons.SetActive(false);
            
            dialogue1.text = npc.isGivenItem;
           
            npcDialogue.SetActive(true);
        }

        else if (npc.canGiveItem == false)
        {
            playerButtons.SetActive(false);

            dialogue1.text = npc.alreadyHasItem;

            npcDialogue.SetActive(true);
        }
    }

   void exitDialogue()
   {
       dialogue1.text = npc.dialogueOutro;
       npc.hasTalked = true; 
   }

}
