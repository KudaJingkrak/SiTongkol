﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Guirao.UltimateTextDamage;

[RequireComponent(typeof(Droppable), typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour, IAttackable{
    public UltimateTextDamageManager textDamage_Manager;
	public MonsterName label; 	
	public float health = 10f;
	public BoxCollider2D boxColl2D;
	
	[Header("Attack")]
	public float attackSpeed = 1f;
	public float attackDistance = 1f;
    public float knockbackForce = 3f;
	
	[Header("Movement")]
	private int _step;
	public MovementType movementType = MovementType.Walk;
	public float speed = 1f;
	public GameObject targetObject;
	public List<Transform> targetPoints;
	public int currentIndexPoint = 0;
	public float disntanceTolerance = 1f;
	public float stuckDistance = 0.25f;
	private Direction _direction = Direction.Front;
	public Direction currentDirection {get{return _direction;}}

	public int maxStuckStep = 10;
	public int minStuckStep = 0;
    public bool isKnockback = false;
	[Panda.Task]
	public bool IsEndStep{
		get{return _step < 1;}
	}
	[Panda.Task]
	public bool IsReachedTarget{
		get{
			if(targetObject){
				//Debug.Log(Vector3.Distance(targetObject.transform.position, transform.position)+" unit disntace");
				return Vector3.Distance(targetObject.transform.position, transform.position) <= disntanceTolerance;
			}
			return false;
		}
	}
	[Panda.Task]
	public bool IsStuck{
		get{
			Vector2 _castDir = Vector2.zero; 
			
			switch(_direction){
				case Direction.Back:
					_castDir = Vector2.up;
					break;
				case Direction.Front:
					_castDir = Vector2.down;
					break;
				case Direction.Left:
					_castDir = Vector2.left;
					break;
				case Direction.Right:
					_castDir = Vector2.right;
					break;
			}

			RaycastHit2D[] hits = Physics2D.BoxCastAll(boxColl2D.transform.position, boxColl2D.size, 0, _castDir, stuckDistance);
			for(int i =0; i < hits.Length; i++){
				if(hits[i].collider.CompareTag("Wall")){
					return movementType != MovementType.Fly;	
				}
			}
			return false;
		}
	}

	Animator anim;
	Rigidbody2D rigid2D;
	Droppable droppable;
	private float _health;
	float x,y;
    // Use this for initialization
    void Start () {
		anim = gameObject.GetComponent<Animator>();
		_health = health;
		droppable = gameObject.GetComponent<Droppable>();
		rigid2D = gameObject.GetComponent<Rigidbody2D>();
		boxColl2D = gameObject.GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat("MoveX", x);
		anim.SetFloat("MoveY", y);
	}

	public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None){
        if (type == DamageType.Normal)
        {
            textDamage_Manager.Add("" + damage, transform, "default");
        }
        else if (type == DamageType.Critical)
        {
            textDamage_Manager.Add("" + damage, transform, "critical");
        }
        Debug.Log("harusnya ada damage");
        _health -= damage;
        Knockback(causer.transform);

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

    public void Knockback(Transform causer)
    {
        Knockback(causer, knockbackForce);
    }

    public void Knockback(Transform causer, float power = 0)
    {
        if (isKnockback == false)
        {
            StartCoroutine(StartKnockback(0.3f,causer.position,power));
           
        }
    }

    IEnumerator StartKnockback(float delay,Vector3 position,float power)
    {
        isKnockback = true;
        Vector2 force = -(position - transform.position).normalized * power;
        rigid2D.velocity = force;
        rigid2D.AddForce(force);
        yield return new WaitForSeconds(delay);
        isKnockback = false;
    }

    [Panda.Task]
	bool SetPlayerTarget(){
		if(targetObject && targetObject.CompareTag("Player")) return true;

		GayatriCharacter gayatri = FindObjectOfType<GayatriCharacter>();
		if(gayatri){
			targetObject = gayatri.gameObject;
			return true;
		}
		return false;
	}

	[Panda.Task]
	void MoveToPlayer(){
		if(targetObject.CompareTag("Player")){
			MoveToTarget();
			Panda.Task.current.Succeed();
		}else{
			SetPlayerTarget();
			Panda.Task.current.Fail();
		}
		
	}

	[Panda.Task]
	void SetRandomStep(){
		_step =	Random.Range(minStuckStep, maxStuckStep);
		Panda.Task.current.Succeed();
	}

	[Panda.Task]
	void MoveUntilEndStep()
	{
		_step--;
		MoveByDirection();
		if(_step < 1){
			Panda.Task.current.Succeed();
		}else{
			Panda.Task.current.Fail();
		}
	}

	#region Movement

	[Panda.Task]
	public void SetRandomDirection(){
		SetDirection((Direction)Random.Range(0, 4));
		Panda.Task.current.Succeed();
	}

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
	public void MoveByDirection(){
		SetDirection(_direction);
		Move(x,y);
	}

	public void Move(float x, float y){
		this.x = x;
		this.y = y;
        if(isKnockback)
        {
            return;
        }
		rigid2D.velocity = new Vector3(x,y,0) * Mathf.Lerp(0f, speed, 1f);
		SetDirection();
	}

	public void Move(Vector2 direction){
		this.x = direction.x;
		this.y = direction.y;
        if (!isKnockback)
        {
            rigid2D.velocity = new Vector3(x, y, 0) * Mathf.Lerp(0f, speed, 1f);
        }
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<GayatriCharacter>().ApplyDamage(5,this.gameObject);
        }
    }

    public void Fall(){}

    #endregion
}
