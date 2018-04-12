using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AIMovementComp))]
public class KodokComp : MonoBehaviour, IAttackable {
	public float health = 1f;
	private float _health;
	private AIMovementComp _movementComp;
	private BoxCollider2D _boxColl2D;

	#region Attack
	[Panda.Task]
	public bool isAttacking = false;
	public float damage= 10;
	public Vector2 attackOffset = Vector2.zero;
	public float attackRange = 3f;
	[Panda.Task]
	public bool canAttackPlayer{
		get{
			RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _boxColl2D.size, 0f, _movementComp.GetVectorDirection(), attackRange);
			for(int i =0; i < hits.Length; i++){
				if(hits[i].collider.CompareTag("Player")){
					return true;
				}
			}
			return false;
		}
	}

	[Panda.Task]
	public void doAttack(){
		isAttacking = true;
		//TODO set anim attack

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _boxColl2D.size, 0f, _movementComp.GetVectorDirection(), attackRange);
		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider.CompareTag("Player")){
				Debug.Log("Apply Damage to Player");
			}
		}
		//Delay until anim done
		StartCoroutine(StopAttack(0.1f));
		Panda.Task.current.Succeed();

	}

	IEnumerator StopAttack(float delay){
		yield return new WaitForSeconds(delay);
		isAttacking = false;
	}

	#endregion

	#region Attackable
	public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        
    }

    public void Destruct()
    {
        
    }

    public void Die()
    {
		
    }
	#endregion

	void Awake(){
		_health = health;
	}

    // Use this for initialization
    void Start () {
		_movementComp = GetComponent<AIMovementComp>();
		_boxColl2D = GetComponent<BoxCollider2D>();

		//TODO nanti ganti
		_movementComp.SetPlayerAsTarget();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
			
	}


}
