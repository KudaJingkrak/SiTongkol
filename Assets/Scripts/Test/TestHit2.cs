using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHit2 : MonoBehaviour {
	SpriteRenderer render;
	// Use this for initialization
	private void Awake()
	{
		render = GetComponent<SpriteRenderer>();	
	}

	// Use this for initialization
	void Start () {
		render.material.SetFloat("_FlashAmount", 1.0f);
		render.material.SetFloat("_IsFlash2", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
