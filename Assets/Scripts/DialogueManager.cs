using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartDialogue(Dialogue dialogue)
	{
		//TODO : Nanti disini akan nge load Dialogue apa yang akan dilakukan, dengan apa ?
		nameText.text = dialogue.name;
		sentences.Clear();
		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}
	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;

	}

	void EndDialogue()
	{
		dialogueText.text = "End of Conversation";
	}
}
