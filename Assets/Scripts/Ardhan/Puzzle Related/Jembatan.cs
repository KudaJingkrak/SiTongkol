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
            Sprite_Jembatan1.SetActive(true);
            Sprite_Jembatan1.transform.position = new Vector3(0, 12);
            Collider_Jembatan.SetActive(false);
        }
	}
}
