using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(AITargetComp))]
public class MonsterTurrent : MonoBehaviour {
	AIMovementComp _movementComp;
	AITargetComp _targetComp;
	// Use this for initialization
	void Start () {
		_movementComp = GetComponent<AIMovementComp>();
		_targetComp = GetComponent<AITargetComp>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
