using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Bullet {
	public float blingTime = 4f;
	private float _blinkTimer = 0f;
	Material _mat;

	void LateUpdate()
	{
		_blinkTimer += Time.deltaTime;

		if(_blinkTimer > blingTime)
		{
			_blinkTimer = 0;
		}


		if(!_mat)
		{
			_mat = GetComponent<SpriteRenderer>().material;
		}
		else
		{
			_mat.SetFloat("_ShineLocation", _blinkTimer/blingTime);
		}
		
	}

}

