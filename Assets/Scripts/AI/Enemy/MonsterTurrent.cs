using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(AITargetComp))]
public class MonsterTurrent : MonoBehaviour {
	public float attackRange = 3f;
	bool isFiring;
	AIMovementComp _movementComp;
	AITargetComp _targetComp;
	// Use this for initialization
	void Start () {
		_movementComp = GetComponent<AIMovementComp>();
		_targetComp = GetComponent<AITargetComp>();

		_movementComp.SetDirection(Direction.Right);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isFiring){
			StartCoroutine(Firing(1f));
		}
	}

	public void Fire(){
		_targetComp.FireDirection(transform.position);
	}

	IEnumerator Firing(float delay){
		isFiring = true;
		Fire();
		yield return new WaitForSeconds(delay);
		isFiring = false;
	}
}
