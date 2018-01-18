using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable: MonoBehaviour{
	public Transform[] items;

	public void DropItem(Transform trans){
		Vector3 pos = trans.position;
		

		foreach(Transform item in items){
			Instantiate(item, (pos + new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-0.5f, 0.5f),-0.01f)), Quaternion.identity);
		}
	}
}
