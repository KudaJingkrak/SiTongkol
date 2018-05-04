using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

	SpriteRenderer tempRenderer;
	
	void Awake(){
		tempRenderer = GetComponentInParent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		tempRenderer.sortingOrder = (int) Camera.main.WorldToScreenPoint(tempRenderer.bounds.min).y * -1;
	}
}
