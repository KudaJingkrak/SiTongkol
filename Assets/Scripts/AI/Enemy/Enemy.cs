using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(Droppable), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IAttackable{
	public MonsterName label; 	
	public float health = 10f;
	
	
	[Header("Movement")]
	public float speed = 1f;
	public GameObject targetObject;
	public List<Transform> targetPoints;
	public int currentIndexPoint = 0;
	public float disntanceTolerance = 0f;
	private Direction _direction = Direction.Front;
	public Direction currentDirection {get{return _direction;}}
	public bool isReachedTarget{
		get{
			if(targetObject){
				return Vector3.Distance(targetObject.transform.position, transform.position) <= disntanceTolerance;
			}
			return false;
		}
	}
	public bool isStuck{
		get{return false;}
	}


	Rigidbody2D rigid2D;
	Droppable droppable;
	private float _health;
	float x,y;
    // Use this for initialization
    void Start () {
		_health = health;
		droppable = gameObject.GetComponent<Droppable>();
		rigid2D = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None){
        _health -= damage;

		Debug.Log("Health "+gameObject.name+" : " + _health);
		
		if(_health <= 0) Die();

    } 

    public void Destruct(){
        throw new System.NotImplementedException();
    }

    public void Die()
    {
		Quest.Instance.AddMonsterCounter(label);
		Quest.Instance.CheckProgressingObjective(null, label);
		droppable.DropItem(transform);
		gameObject.SetActive(false);
    }

	#region Movement	
	public void SetDirection(){
		if(Mathf.Abs(x) > Mathf.Abs(y)){
			if(x > 0){
				_direction = Direction.Right;
			}else{
				_direction = Direction.Left;
			}
		}else{
			if(y > 0){
				_direction = Direction.Back;
			}else{
				_direction = Direction.Front;
			}
		}
	}

	public void SetDirection(Direction direction){
		this._direction = direction;
		switch(direction){
			case Direction.Back:
				x = 0f; 
				y = 1f;
				break;
			case Direction.Right:
				x = 1f;
				y = 0f;
				break;
			case Direction.Front:
				x = 0f;
				y = -1f;
				break;
			case Direction.Left:
				x = -1f; 
				y = 0f;
				break;
		}
	}

	public void Move(float x, float y){
		this.x = x;
		this.y = y;
		rigid2D.velocity = new Vector3(x,y,0) * Mathf.Lerp(0f, speed, 1f);
		SetDirection();
	}

	public void Move(Vector2 direction){
		this.x = direction.x;
		this.y = direction.y;
		rigid2D.velocity = new Vector3(x,y,0) * Mathf.Lerp(0f, speed, 1f);
		SetDirection();
	}

	public void MoveToTarget(){
		if(targetObject){
			Vector3 dir = Vector3.Normalize(targetObject.transform.position - transform.position);
			Move(dir);			
		}
	}

	public void MoveToPoint(int index = -1){
		if(index >= targetPoints.Count) return;
		
		Vector3 dir;
		if(index < 0){
			dir = Vector3.Normalize(targetPoints[currentIndexPoint].position - transform.position);
		}else{
			dir = Vector3.Normalize(targetPoints[index].position - transform.position);
		}
		Move(dir);
	}

	public void Launch(Vector2 direction, float power = 1f, bool overrideSpeed = false){
		Vector2 force = direction * power;
		if(overrideSpeed){
			rigid2D.velocity = Vector2.Lerp(Vector2.zero, force, 1f);
		}else{
			rigid2D.velocity = Vector2.Lerp(rigid2D.velocity, force, 1f);
		}
	}

	public float GetTargetDistance(){
		if(targetObject){
			return Vector3.Distance(targetObject.transform.position, transform.position);
		}
		return float.MaxValue;
	}

	#endregion
}
