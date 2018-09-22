using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guirao.UltimateTextDamage;
using UnityEngine.UI;
using Panda;

[RequireComponent(typeof(AIMovementComp))]
public class GreenGoblin : BaseDungeonEnemy, IAttackable 
{

	private Animator _anim;

    [Header("UI Manager")]
    public UltimateTextDamageManager textDamageManager;
    public GameObject healthCanvas;
    public float healthTimer = 1f;
    private Slider _healthSlider;
    private float _healthTimer;

    private AIMovementComp _move;
	public float attackRange = 2f, canFollowRange = 5f, attackDamage = 15f, quickMovePower = 10f;
    // Attackable Variable
    private IEnumerator knockbackrator = null;

    //Panda Task Variable
    [Task]
    public bool IsAttacking = false;
    private bool __canAttack = false;
    private Vector2 __castDir = Vector2.zero;
	#region PandaTask
	[Task]
    public bool CanAttackPlayer{
        get{
            if(_move) __castDir = _move.GetVectorDirection();
			__canAttack = false;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f,1f), 0f, __castDir, attackRange); 

            for(int i = 0 ; i < hits.Length; i++)
            {
                if(hits[i].transform.CompareTag("Player"))
                {
                    __canAttack = true;
                    break;
                }
            }

            return __canAttack;
        }
    }
	[Task]
	public bool CanFollowPlayer
	{
		get{
			if(!_move || !_move.target) return false;
			return Vector2.Distance(transform.position, _move.target.position) <= canFollowRange;
		}
	}
    [Task]
    public void StartAttack(){
        IsAttacking = true;
        _anim.SetBool("IsCharge", true);

        // if(Task.isInspected)
			Task.current.Succeed();
    }
	public void QuickForward()
	{
		_move.QuickMoveByDirection4(quickMovePower,0.25f);
	}
	[Task]
	public void UnAttack()
	{
		_anim.SetBool("IsCharge", false);
		_anim.SetBool("IsSlash", false);

		StopAttack();
	}
    [Task]
    public void DoAttack(){
       
		 __castDir = _move.GetVectorDirection();

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f,1f), 0f, __castDir, 2f); 

		for(int i = 0 ; i < hits.Length; i++)
		{
			if(hits[i].transform.CompareTag("Player"))
			{
				hits[i].transform.GetComponent<GayatriCharacter>().ApplyDamage(attackDamage, this.gameObject);
				break;
			}
		}

       if(Task.isInspected)
			Task.current.Succeed();
        
    }
	[Task]
	public void DoSlash()
	{
		_anim.SetBool("IsSlash", true);

		// if(Task.isInspected)
			Task.current.Succeed();
	}

    [Task]
    public void StopAttack(){
        IsAttacking = false;

        if(Task.isInspected)
			Task.current.Succeed();
    }
	#endregion

	#region BaseEnemy
	public override void Initialized()
	{
        ReportLiveToRoom();
		_health = health;
	}
	#endregion

	#region IAttackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
       doFlash();
        textDamageManager.Add("" + damage, transform, "default");
        _health -= damage;
        if(causer)
        {
            if(knockbackrator != null)
            {
                StopCoroutine(knockbackrator);
            }

            knockbackrator = doKnockback(causer.transform, 5f, 0.075f);
            StartCoroutine(knockbackrator);
        }
        // Debug.Log("Darah saya tinggal " + _health + " diserang " + damage);
        ShowHealthCanvas();
        if(_health <= 0)
        {
            Die();
        }
    }

    public void Fall()
    {
        /*
         * TO DO:
         * - 1. Freeze into certain location (X,Y,Z)
         * - 2. DO The Fall Animation
         * - 3. Attackable.Die()
         * - 4. Disabled
         * - 5. Done
         */
    }

    public void Destruct()
    {

    }

    public void Die()
    {
        ReportDieToRoom();
        //nanti ganti
        Destroy();
    }

    public void Knockback(Transform causer)
    {
        Knockback(causer, 0f);
    }

    public void Knockback(Transform causer, float power = 0)
    {
        Vector2 force = -(causer.position - transform.position).normalized * power;
        _move.Rigid2D.velocity = force;
    }

	IEnumerator doKnockback(Transform causer, float power = 0f, float delay = 0.3f)
    {
        Knockback(causer, power);
        yield return new WaitForSeconds(delay);
        _move.StopMove();
    }
	#endregion

	#region UI
	void ShowHealthCanvas()
    {
         _healthTimer = healthTimer;
        if(!healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(true);
        }
    }
    void HideHealthCanvas()
    {
        if(healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
        }   
    }
	#endregion

    private void Awake()
	{
		_move = GetComponent<AIMovementComp>();
        _anim = GetComponent<Animator>();
        Initialized();
        _healthSlider = healthCanvas.GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

		if(_move && !_move.isPlayerAsTarget)
		{
			_move.SetPlayerAsTarget();
		}
		
		if(_anim.isActiveAndEnabled)
        {
            _anim.SetFloat("MoveX", _move.x);
            _anim.SetFloat("MoveY", _move.y);
            _anim.SetFloat("Speed", _move.Rigid2D.velocity.magnitude);
        }

		if(healthCanvas.activeSelf && _healthTimer <= 0)
        {
           HideHealthCanvas();
        }else{
            _healthTimer -= Time.deltaTime;
        }
	}

	void LateUpdate()
    {
        
        if(healthCanvas.activeSelf)
        {
            _healthSlider.value = _health/health;
        }
    }
}
