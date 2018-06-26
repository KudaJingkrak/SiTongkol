using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

	SpriteRenderer tempRenderer;
	
	public ChildRender[] childs;
	
	void Awake(){
		tempRenderer = GetComponentInParent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		tempRenderer.sortingOrder = (int) Camera.main.WorldToScreenPoint(tempRenderer.bounds.min).y * -1;

		for(int i = 0; i < childs.Length; i++)
		{
			childs[i].render.sortingOrder = tempRenderer.sortingOrder + childs[i].orderLayer; 
		}
	}

	[System.Serializable]
	public class ChildRender
	{
		public SpriteRenderer render;
		[Tooltip("This order is base on parent")]
		public int orderLayer;
	}
}
