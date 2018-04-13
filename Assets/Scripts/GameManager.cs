using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    /*
     * This Game Manager would became a Main Game Controller
     * 1. Don't Destroy on Load
     */
    // Use this for initialization

        //Player Status Inheritence.
    public PlayerStat player_Status;

    //Slidernya
    public Slider Health_Slider;
    public Slider Mana_Slider;
    public Slider Stamina_Slider;

    

	void Start () {
        DontDestroyOnLoad(this.gameObject);
        //nanti harus panggil PlayerPreferences

	}
	
	// Update is called once per frame
	void Update () {

        //Updating UI
        Health_Slider.value = (player_Status.get_Health() / player_Status.Max_Health);
        Mana_Slider.value = (player_Status.get_Mana() / player_Status.Max_Mana);
        Stamina_Slider.value = (player_Status.get_Stamina() / player_Status.Max_Stamina); 
        
	}
}
