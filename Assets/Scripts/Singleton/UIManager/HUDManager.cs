using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : BaseClass {

    public Image Health_Image;
    public Image Stamina_Image;
    public Image Mana_Image;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            Health_Image.fillAmount = Status.percentHealth;

            Stamina_Image.fillAmount = Status.percentStamina;
       
        /*
         * TODO:
               Mana_Image filling amount ?
         */
    }
}
