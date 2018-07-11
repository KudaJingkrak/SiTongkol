using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour {

    public Sprite switch_off;
    public Sprite switch_on;
    public GameObject switchButton;
    public bool boolSwitch;
    public bool isTahan;
    public bool isOnce;

	// Use this for initialization
	void Start () {
        boolSwitch = false;
        changeSprite();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void changeSprite()
    {
        if (boolSwitch)
        {
            switchButton.GetComponent<SpriteRenderer>().sprite = switch_on;
            //switchButton.transform.position = new Vector2(switchButton.transform.position.x, switchButton.transform.position.y - 0.25f);
        }
        else
        {
            switchButton.GetComponent<SpriteRenderer>().sprite = switch_off;
            //switchButton.transform.position = new Vector2(switchButton.transform.position.x, switchButton.transform.position.y + 0.25f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (boolSwitch)
        {
            if (isOnce)
            {

            }
            else
            {
                boolSwitch = false;
                changeSprite();
                switchButton.GetComponentInParent<PuzzleManager>().checkPuzzle();
            }
           
        }
        else if (!boolSwitch)
        {
            boolSwitch = true;
            changeSprite();
            switchButton.GetComponentInParent<PuzzleManager>().checkPuzzle();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isTahan)
        {
            if (boolSwitch)
            {
                boolSwitch = true;  
                //tetep ditrue.
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isTahan)
        {
            if (boolSwitch)
            {
                boolSwitch = false;
                changeSprite();
                switchButton.GetComponentInParent<PuzzleManager>().checkPuzzle();
            }
        }
    }

}
