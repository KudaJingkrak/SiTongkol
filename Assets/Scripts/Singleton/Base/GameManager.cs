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
    private static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public StatusManager m_StatusManager;
    public UIManager m_UIManager;
    public GameObject m_Player;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
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
