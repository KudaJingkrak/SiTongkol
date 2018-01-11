using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour,IInteractable {

	public Dialogue dialogue;
	public DialogueManager m_Dialogue;
	public void TriggerDialogue()
	{
		m_Dialogue = FindObjectOfType<DialogueManager>();
		m_Dialogue.StartDialogue(dialogue);
	}

	void Update()
	{
		
	}

    public void ApplyInteract(GameObject instigator = null)
    {
       GayatriCharacter c_Gayatri = instigator.GetComponent<GayatriCharacter>();
	   if(c_Gayatri != null)
	   {
		   if(c_Gayatri.onDialogue)
		   {
			   
			   if(!m_Dialogue.DisplayNextSentence())
			   {
				   c_Gayatri.onDialogue = false;
				   c_Gayatri.isInteracting = false;
			   }
		   }
		   else
		   {
			   TriggerDialogue();
			   c_Gayatri.onDialogue = true;
		   }
		   
	   }
	   else
	   {

	   }
    }
}
