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
    public float healthTimer = 1;
    private Slider _healthSlider;
    private float _healthTimer;

    private AIMovementComp _move;

    // Attackable Variable
    private IEnumerator knockbackrator = null;

    //Panda Task Variable
    [Task]
    public bool IsAttacking = false;
    private bool __canAttack = false;
    private Vector2 __castDir = Vector2.zero;
	#region PandaTask

	#endregion

	#region BaseEnemy
	public override void Initialized()
	{
		_health = health;
	}
	#endregion

	#region IAttackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
       
    }

    public void Destruct()
    {

    }

    public void Die()
    {
        
    }

    public void Knockback(Transform causer)
    {
        
    }

    public void Knockback(Transform causer, float power = 0)
    {
        
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

		if(!_move.isPlayerAsTarget)
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
