using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseClass {

    public HUDManager HUD;
    public GameObject HUD_Object;
	// Use this for initialization
	void Start () {
		//Nanti disini di check ke PlayerPreferences nya.
	}
	
	// Update is called once per frame
	void Update () {


        
	}

    public void Hidden()
    {
        HUD_Object.SetActive(false);
       // Container.enabled = true;
    }

    public void UnHidden()
    {
        HUD_Object.SetActive(true);
       // Container.enabled = false;
    }


}
