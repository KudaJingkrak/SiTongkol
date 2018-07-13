using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;
using Naga.Dungeon;

[RequireComponent(typeof(AITargetComp))]
public class Anganan : BaseEnemy, IAttackable {
	[Header("Spawn point location")]
	public Transform[] spawnPoints;
	[Header("Attack Variable")]
	[Task]
	public bool IsAttacking = false;
	public bool IsCharging = false;
	public int indexAttack = 0;
	public float slamAttackDamage = 50f, sweepAttackDamage = 60f;
	public GameObject[] damageCollider;
	public int[] hitTrigger;
	private Stack<int> _hitTrigger = new Stack<int>();
	private Slider _healthBar;
	private Animator _anim;
	private AIMovementComp _move;
	private AITargetComp _aim;

	#region IAttackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        doFlash();

		_health -= damage;

		if(_health < 0) Die();
    }

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        Destroy();
    }
	#endregion

    #region BaseEnemy
    public override void Initialized()
	{
		_health = health;
	}

    public void Knockback(Transform causer)
    {
        throw new System.NotImplementedException();
    }

    public void Knockback(Transform causer, float power = 0)
    {
        throw new System.NotImplementedException();
    }
    #endregion

	#region Animation
	public void DoAttacking()
	{
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsAttacking", true);
		}
	}
	public void UnAttacking()
	{
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsAttacking", false);
		}
		IsAttacking = false;
	}
	public void DoOnHit()
	{
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsOnHit", true);
		}
	}
	public void UnOnHit()
	{
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsOnHit", false);

			for(int i = 0; i < damageCollider.Length; i++)
			{
				if(damageCollider[i].activeSelf)
				{
					damageCollider[i].SetActive(false);
				}
			}
		}
	}
	#endregion

	#region PandaTask

	[Task]
	public bool CanAttackPlayer
	{
		get{
			RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(3f,3f), 0f,  Vector2.down, Random.value * 2f); 

			for(int i = 0 ; i < hits.Length; i++)
			{
				if(hits[i].transform.CompareTag("Player"))
				{
					return true;
				}
			}
			return false;
		}
	}
	[Task]
	public void HorizontalMove(float isRightMove)
	{
		_move.Move(isRightMove, 0f);
		Task.current.Succeed();
	}
	[Task]
	public void Discharge()
	{
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsCharging", false);
		}
		Task.current.Succeed();
	}
	[Task]
	public void StartAttack()
	{
		IsAttacking = true;
		indexAttack = Random.Range(0,3);
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsAttacking", true);
			_anim.SetBool("IsCharging", true);
			_anim.SetInteger("IndexAttack", indexAttack);
		}
		
		Task.current.Succeed();
	}

	[Task]
	public void StopAttack()
	{
		IsAttacking = false;
		if(_anim.isActiveAndEnabled)
		{
			_anim.SetBool("IsAttacking", false);
		}
		Task.current.Succeed();
	}
	[Task]
	public void HorizontalFollow()
	{
		float myMove = _aim.targetAim.position.x - transform.position.x;
		if(myMove < -1)
		{
			myMove = -1;
		}else if(myMove > 1)
		{
			myMove = 1;
		}else{
			myMove = 0;
		}
		_move.Move(myMove, 0f);

		Task.current.Succeed();
	}
	public void SlamAttackCast()
	{

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(3f,3f), 0f,  Vector2.down, 1.75f); 

		for(int i = 0 ; i < hits.Length; i++)
		{
			if(hits[i].transform.CompareTag("Player"))
			{
				hits[i].transform.GetComponent<GayatriCharacter>().ApplyDamage(slamAttackDamage, this.gameObject);
				break;
			}
		}

		DungeonRoom room = DungeonManager.Instance.rooms[0];
		int size = 0;
		for(int i = 0 ; i < room.monster.Count; i++)
		{
			size += room.monster[i].size;
		}

		Stack<int> indexSpawn = new Stack<int>();
		indexSpawn.Push(Random.Range(0,room.respwanPoint.Count));
		size--;

		while(size > 0)
		{
			int i = Random.Range(0,room.respwanPoint.Count);
			if(!indexSpawn.Contains(i))
			{
				indexSpawn.Push(i);
				size--;
			}

		}

		for(int i = 0 ; i < room.monster.Count; i++)
		{
			for(int j = 0; j < room.monster[i].size; j++)
			{
				DungeonManager.Instance.SpawnMonster(room.monster[i].tier, room, indexSpawn.Pop());
			}
		}
		
	}

	public void SweepAttackCast()
	{

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(5f,3f), 0f,  Vector2.down, 0.95f); 

		for(int i = 0 ; i < hits.Length; i++)
		{
			if(hits[i].transform.CompareTag("Player"))
			{
				hits[i].transform.GetComponent<GayatriCharacter>().ApplyDamage(sweepAttackDamage, this.gameObject);
				break;
			}
		}
	}
	public void SpawnOrb()
	{
		GameObject go = _aim.FireWithOffset(0,0);
		if(go)
		{
			Bullet bullet = go.GetComponent<Bullet>();
			bullet.SetTarget(_aim.targetAim);
		}
	}
	#endregion

    void Awake()
	{
		_anim = GetComponent<Animator>();
		_move = GetComponent<AIMovementComp>();
		_aim = GetComponent<AITargetComp>();
		Initialized();
	}

	void Start()
	{
		for(int i= 0; i < hitTrigger.Length; i++){
			_hitTrigger.Push(hitTrigger[i]);
		}	
	}

	void Update()
	{

		if(!_aim.targetAim)
        {
            _aim.targetAim = GameManager.Instance.m_Player.transform;
        }

		if(_healthBar)
		{
			_healthBar.value = _health / health;
		}else{
			if(!GameManager.Instance.m_UIManager.BossHealthBar.activeSelf)
			{
				GameManager.Instance.m_UIManager.BossHealthBar.SetActive(true);
			}

			_healthBar = GameManager.Instance.m_UIManager.BossHealthBar.GetComponent<Slider>();
		}


		if(_health < _hitTrigger.Peek())
		{

		}
	}

}
