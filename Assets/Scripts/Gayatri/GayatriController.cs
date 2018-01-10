using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriController : MonoBehaviour {
	public GayatriCharacter myCharacter;
	float _axisX = 0f, _axisY = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("B")){
			myCharacter.Attack(0.3f);
		}

		if(Input.GetButtonUp("A")){
			myCharacter.Interact();
		}
	}

	void FixedUpdate(){
		_axisX = Input.GetAxis("Horizontal");
		_axisY = Input.GetAxis("Vertical");

		if(_axisX > 0.6f){
			myCharacter.SetDirection(Direction.Right);
		}else if(_axisX < -0.6f){
			myCharacter.SetDirection(Direction.Left);
		}

		if(_axisY > 0.6f){
			myCharacter.SetDirection(Direction.Front);
		}else if(_axisY < -0.6f){
			myCharacter.SetDirection(Direction.Back);
		}

		myCharacter.Move(_axisX, -_axisY);
	}
}
