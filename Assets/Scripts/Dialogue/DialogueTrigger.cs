using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour,IInteractable {

	public Dialogue dialogue;
	public DialogueManager m_Dialogue;
	public int index_Dialogue;
	public int indexQuest;
	public QuestDialogue[] dialogueQuest;

	public void dialoguePicker()
	{
		bool hadQuest = false;
		for(int i = 0; i < dialogueQuest.Length; i++)
		{
			if(Quest.Instance.HasProgressionQuest(dialogueQuest[i].q_Name))
			{
				hadQuest = true;
				indexQuest = i;
				break;
			}
		}
		if(hadQuest)
		{
			TriggerDialogue(dialogueQuest[indexQuest].dialogue);
		}
		else
		{
			TriggerDialogue(dialogue);
		}
		//intinya sih disini buat milih dialogue mana terus di StartDialogue(dialogue[number])
	}
	public void TriggerDialogue(Dialogue dialogue)
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
			   dialoguePicker();
			   c_Gayatri.onDialogue = true;
		   }
		   
	   }
	   else
	   {

	   }
    }
}
