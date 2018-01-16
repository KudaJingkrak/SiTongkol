using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriController : MonoBehaviour {
	public GayatriCharacter myCharacter;
	public DialogueManager m_Dialogue;
	float _axisX = 0f, _axisY = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("B")){
			myCharacter.Attack(0.3f);
		}

		if(Input.GetButtonDown("A")){
			myCharacter.Pull();
		}
		if(Input.GetButtonUp("A")){
			myCharacter.UnPull();
			myCharacter.Interact();
			myCharacter.Pickup();
		}
	}

	void FixedUpdate(){
		_axisX = Input.GetAxis("Horizontal");
		_axisY = Input.GetAxis("Vertical");

		myCharacter.Move(_axisX, -_axisY);
	}
}
