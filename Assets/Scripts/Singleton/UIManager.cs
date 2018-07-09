using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseClass {

    public HUDManager HUD;
	// Use this for initialization
	void Start () {
		//Nanti disini di check ke PlayerPreferences nya.
	}
	
	// Update is called once per frame
	void Update () {


        
	}

    public void Hidden()
    {
       // Container.enabled = true;
    }

    public void UnHidden()
    {
       // Container.enabled = false;
    }


}
