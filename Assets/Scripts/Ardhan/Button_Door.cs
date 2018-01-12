using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door : MonoBehaviour {
    public GameObject Door;
    public ButtonDoor_Type buttonType = ButtonDoor_Type.Door;
    public bool isOpen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Door.gameObject.SetActive(false);
        isOpen = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Door.gameObject.SetActive(true);
        isOpen = false;
    }

}
