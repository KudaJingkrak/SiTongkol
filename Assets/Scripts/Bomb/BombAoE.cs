using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAoE : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //does it right?
        if (collision.gameObject.tag != "background") Destroy(collision.gameObject);
    }
}
