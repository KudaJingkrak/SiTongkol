using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : MonoBehaviour {
    public BombSystem sysBomb;
	// Use this for initialization
	void Start () {
		
	}

    public void StartExplosion()
    {
        sysBomb.Exploded_Bomb();
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
