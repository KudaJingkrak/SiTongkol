﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {
	
	public virtual void OnObjectReuse(){}

	protected virtual void Destroy(){
		gameObject.SetActive(false);
	}
}