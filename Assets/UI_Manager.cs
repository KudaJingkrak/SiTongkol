using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public Canvas canvas_Player; 

	// Use this for initialization
	void Start () {
        canvas_Player = this.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canvas_Player.worldCamera == null)
        {
            canvas_Player.worldCamera = Camera.main;
        }
	}
}
