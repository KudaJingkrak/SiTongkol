﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIMovementComp))]
public class AITargetComp : MonoBehaviour {
	public GameObject target;
	private AIMovementComp _movement;
	
	[Header("Projectile")]
	public GameObject prefab;
	
	public void Fire(Vector2 position, Quaternion rotation){
		PoolManager.Instance.ReuseObject(prefab, position, rotation);	
	}

	public void FireDirection(Vector2 position){
		Quaternion rotation;

		switch(_movement.GetDirection()){
			case Direction.Back:
				rotation = Quaternion.Euler(0f,0f,0f);
				break;
			case Direction.Front:
				rotation = Quaternion.Euler(0f,0f,180f);
				break;
			case Direction.Left:
				rotation = Quaternion.Euler(0f,0f,90f);
				break;
			case Direction.Right:
				rotation = Quaternion.Euler(0f,0f,270f);
				break;
			default:
				rotation = Quaternion.Euler(0f,0f,180f);
				break;
		}
		PoolManager.Instance.ReuseObject(prefab, position, rotation);
	}

	// Use this for initialization
	void Start () {
		_movement = GetComponent<AIMovementComp>();
	}
	
}