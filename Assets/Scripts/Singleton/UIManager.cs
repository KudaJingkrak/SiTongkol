﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseClass {

    public Slider Health_Slider;
    public Slider Mana_Slider;
    public Slider Stamina_Slider;

    public Image Container;

	// Use this for initialization
	void Start () {
		//Nanti disini di check ke PlayerPreferences nya.
	}
	
	// Update is called once per frame
	void Update () {

        if(Health_Slider){
            Health_Slider.value = Status.percentHealth;

        }

        if(Mana_Slider){
            Mana_Slider.value = Status.percentMana;
        
        }

        if(Stamina_Slider){
            Stamina_Slider.value = Status.percentStamina;

        }
        
	}

    public void Hidden()
    {
        Container.enabled = true;
    }

    public void UnHidden()
    {
        Container.enabled = false;
    }


}
