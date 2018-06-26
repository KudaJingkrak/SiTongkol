using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jembatan : MonoBehaviour {

    public GameObject Collider_Jembatan;
    public GameObject Sprite_Jembatan1;
    public PuzzleManager puzzlenya;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (puzzlenya.PuzzleCompleted)
        {
            Collider_Jembatan.active = false;
            Sprite_Jembatan1.active = true;
        }
	}
}
