using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

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

        Health_Slider.value = GameManager.Stat.get_Health() / GameManager.Stat.Max_Health;
        Mana_Slider.value = GameManager.Stat.get_Mana() / GameManager.Stat.Max_Mana;
        Stamina_Slider.value = GameManager.Stat.get_Stamina() / GameManager.Stat.Max_Stamina;
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
