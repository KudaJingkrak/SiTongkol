using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolManager : MonoBehaviour {
	public GameObject prefab;
	public int size;

	public Transform point;

	void Start(){
		PoolManager.Instance.CreatePool(prefab, size);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			PoolManager.Instance.ReuseObject(prefab, point.position, point.rotation);
		}
	}
}
