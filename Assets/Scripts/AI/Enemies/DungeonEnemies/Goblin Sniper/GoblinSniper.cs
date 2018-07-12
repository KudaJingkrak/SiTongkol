using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guirao.UltimateTextDamage;
using UnityEngine.UI;
using Panda;

[RequireComponent(typeof(AIMovementComp), typeof(AITargetComp))]
public class GoblinSniper : BaseDungeonEnemy, IAttackable
{   
    private Animator _anim;

    [Header("UI Manager")]
    public UltimateTextDamageManager textDamageManager;
    public GameObject healthCanvas;
    public float healthTimer = 1;
    private Slider _healthSlider;
    private float _healthTimer;

    private AIMovementComp _move;
    private AITargetComp _aim;

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
            __canAttack = false; 
			__castDir = _move.GetVectorDirection();

            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f,1f), 0f, __castDir, 10f); 

            for(int i = 0 ; i < hits.Length; i++)
            {
                //Debug.Log(hits[i].transform.name);
                if(hits[i].transform.CompareTag("Player"))
                {
                    __canAttack = true;
                    break;
                }
            }
            // __canAttack = hit && hit.transform.CompareTag("Player");
            

            return __canAttack;
        }
    }

    [Task]
    public void StartAttack(){
        IsAttacking = true;
        

        if(Task.isInspected)
        {
            Task.current.Succeed();
        }
    }
    [Task]
    public void DoAttack(){
        _anim.SetBool("IsAttacking", true);

        if(Task.isInspected)
        {         
            Task.current.Succeed();
        }
        
    }
    public void Attack()
    {
        _aim.FireWithOffset();
    }
    [Task]
    public void UnAttack()
    {
        _anim.SetBool("IsAttacking", false);
    }

    [Task]
    public void StopAttack(){
        IsAttacking = false;

        if(Task.isInspected)
        {
            Task.current.Succeed();
        }
    }
    #endregion

    #region BaseEnemy
    public override void Initialized()
    {
        _health = health;
    }
    #endregion

	#region Attackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        doFlash();
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

    public void Destruct()
    {
        //throw new System.NotImplementedException();
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
    
    void Awake()
    {
        _aim = GetComponent<AITargetComp>();
        _move = GetComponent<AIMovementComp>();
        _anim = GetComponent<Animator>();
        Initialized();
        _healthSlider = healthCanvas.GetComponentInChildren<Slider>();
    }
	void Update()
    {
        // Set target to player
        if(!_aim.targetAim)
        {
            _aim.targetAim = GameManager.Instance.m_Player.transform;
        }

        if(_anim.isActiveAndEnabled)
        {
            _anim.SetFloat("MoveX", _move.x);
            _anim.SetFloat("MoveY", _move.y);
            _anim.SetFloat("Velocity", _move.Rigid2D.velocity.magnitude);
        }

        // Update Aim Direction
        switch(_move.GetDirection())
        {
            case(Direction.Back):
                _aim.SetAimRotation(180f);
            break;
            case(Direction.Front):
                _aim.SetAimRotation(0f);
            break;
            case(Direction.Left):
                _aim.SetAimRotation(270f);
            break;
            case(Direction.Right):
                _aim.SetAimRotation(90f);
            break;
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
