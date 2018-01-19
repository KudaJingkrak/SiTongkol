using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable: MonoBehaviour{
	public DropTuple[] items;

	public void DropItem(Transform trans){
		Vector3 pos = trans.position;
		

		foreach(DropTuple item in items){
			if(Random.value < item.probability){
				Instantiate(item.itemObj, (pos + new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-0.5f, 0.5f),-0.01f)), Quaternion.identity);	
			}
		}
	}
}

[System.Serializable]
public class DropTuple{
	[Range(0,1)]
	public float probability;
	public GameObject itemObj;
}