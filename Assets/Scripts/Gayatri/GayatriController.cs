using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GayatriCharacter))]
public class GayatriController : MonoBehaviour {
	GayatriCharacter myCharacter;
	public DialogueManager m_Dialogue;
	float _axisX = 0f, _axisY = 0f;
	// Use this for initialization
	void Awake(){
		myCharacter = gameObject.GetComponent<GayatriCharacter>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("B")){
            myCharacter.Start_Attacking();
		}

		if(Input.GetButtonDown("A")){
			myCharacter.Pull();
		}
		
		if(Input.GetButtonUp("A")){
			myCharacter.UnPull();
			myCharacter.Interact();
			myCharacter.Pickup();
		}
        if (Input.GetButtonDown("Right_Bumper"))
        {
            myCharacter.StartDodge();
        }

        if (Input.GetButtonDown("Y"))
        {
            myCharacter.StartCrouch();
        }
        else if (Input.GetButtonUp("Y"))
        {
            myCharacter.EndCrouch();
        }
        if (Input.GetButtonDown("Left_Bumper"))
        {
            myCharacter.DeployBomb();
        }
	}

	void FixedUpdate(){
		_axisX = Input.GetAxis("Horizontal");
		_axisY = Input.GetAxis("Vertical");

		myCharacter.Move(_axisX, -_axisY);
	}
}
