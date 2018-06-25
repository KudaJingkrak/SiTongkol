using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : PoolObject {
	public override void OnObjectReuse()
	{
		gameObject.SetActive(true);
		Initialized();
	}

	protected override void Destroy()
	{
		gameObject.SetActive(false);
	}
	

	public virtual void Initialized(){}
}
