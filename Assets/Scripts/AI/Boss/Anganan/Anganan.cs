using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

[RequireComponent(typeof(AITargetComp))]
public class Anganan : BaseEnemy, IAttackable {
	public bool IsAttacking = false;
	public bool IsCharging = false;
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
    }

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
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
	public void HorizontalMove(float isRightMove)
	{
		_move.Move(isRightMove, 0f);
		//Task.current.Succeed();
	}
	public void SlamAttack()
	{

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
