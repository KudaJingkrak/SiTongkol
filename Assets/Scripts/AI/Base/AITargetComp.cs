using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(AIMovementComp))]
public class AITargetComp : MonoBehaviour {
	public GameObject target;
	private AIMovementComp _movement;
	
	[Header("Projectile")]
	public GameObject prefab;

	[Panda.Task]
	public void Fire(){
		Fire(transform.position, transform.rotation);
		Panda.Task.current.Succeed();
	}
	
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

	[Panda.Task]
	public void FireDirection(float x, float y){
		FireDirection(new Vector2(x,y));
		Panda.Task.current.Succeed();
	}

	// Use this for initialization
	void Start () {
		_movement = GetComponent<AIMovementComp>();
	}
	
}
