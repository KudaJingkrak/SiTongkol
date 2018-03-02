using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(Droppable))]
public class Enemy : MonoBehaviour, IAttackable{
	public MonsterName label; 	
	public float health = 10f;
	public Direction direction{get{return _direction;}}
	public GameObject targetObject;
	public float disntanceTolerance = 0f;
	Droppable droppable;
	private float _health;
	private Direction _direction = Direction.Front;
	float x,y;
    // Use this for initialization
    void Start () {
		_health = health;
		droppable = gameObject.GetComponent<Droppable>();
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
		SetDirection();
	}

	public void Move(Vector2 direction){
		Move(direction.x, direction.y);
	}

	public void Move(Vector3 direction){
		Move(direction.x, direction.y);
	}

	public void MoveToObject(){
		if(targetObject){
			Vector3 dir = Vector3.Normalize(targetObject.transform.position - transform.position);

			Move(dir);

			if(GetTargetDistance() < disntanceTolerance){
				
			}
			
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
