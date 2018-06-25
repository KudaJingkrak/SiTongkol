using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {
	
	public virtual void OnObjectReuse(){}

	protected virtual void Destroy(){
		gameObject.SetActive(false);
	}
}

[System.Serializable]
public class PoolStruct{
	public GameObject prefab;
	public int size;
}

[System.Serializable]
public class PoolMonsterStruct : PoolStruct
{
	public Tier tier;
}
