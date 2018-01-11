using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour {

	public Image UI_Controller;
	public Text nameText;
	public Text dialogueText;
	private Queue<string> sentences;
	public Vector3 Posisi_Depan;
	public Vector3 Posisi_Belakang;


	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartDialogue(Dialogue dialogue)
	{
		UI_Controller.rectTransform.DOLocalMoveY(Posisi_Depan.y,0.5f);
		StartCoroutine(wait(0.5f));
		//TODO : Nanti disini akan nge load Dialogue apa yang akan dilakukan, dengan apa ?
		nameText.text = dialogue.name;
		sentences.Clear();
		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}
	public bool DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return false;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		return true;
	}
	IEnumerator wait(float second)
	{
		yield return new WaitForSeconds(second);
	}
	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.025f);
		}
	}

	void EndDialogue()
	{
		UI_Controller.rectTransform.DOLocalMoveY(Posisi_Belakang.y,0.5f);
		dialogueText.text = "";
		nameText.text = "";
	}
}
