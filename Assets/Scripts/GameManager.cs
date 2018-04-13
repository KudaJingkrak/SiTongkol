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
    public static GameManager Instance;
    public PlayerStat player_Status;
    public UI_Manager ui_Manager;

    //Slidernya
    public Slider Health_Slider;
    public Slider Mana_Slider;
    public Slider Stamina_Slider;


    public PlayerStat Stat
    {
        get { return Instance.player_Status; }
    }

    public UI_Manager UI
    {
        get { return Instance.ui_Manager; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        //nanti harus panggil PlayerPreferences

	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
