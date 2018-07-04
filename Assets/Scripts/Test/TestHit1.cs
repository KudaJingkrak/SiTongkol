using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHit1 : BaseEnemy {


	void Start () {
		StartCoroutine(fla());
	}
	
	IEnumerator fla()
	{
		doFlash();
		yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));
		StartCoroutine(fla());
	}
}
