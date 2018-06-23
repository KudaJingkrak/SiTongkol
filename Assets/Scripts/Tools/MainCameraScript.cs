using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class MainCameraScript : BaseClass {
	private ProCamera2D pc2d;
	private void Awake()
	{
		pc2d = GetComponent<ProCamera2D>();		
	}
	
	void Start()
	{
		StartCoroutine(AddTarget(0.1f));
	}

	IEnumerator AddTarget(float delay)
	{
		yield return new WaitForSeconds(delay);
		pc2d.AddCameraTarget(Player.transform);
	}
}
