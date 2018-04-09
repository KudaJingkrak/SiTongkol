using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScene : MonoBehaviour {
	
	public PoolStruct[] poolObjects;
	
	void Start()
	{
		for(int i=0; i < poolObjects.Length; i++){
			PoolManager.Instance.CreatePool(poolObjects[i].prefab, poolObjects[i].size);	
		}
		
	}
}
