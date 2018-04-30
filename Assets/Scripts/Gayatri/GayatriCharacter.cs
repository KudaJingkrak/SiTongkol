using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriCharacter : BaseClass, IAttackable {
	public Rigidbody2D rigid2D;
	public BoxCollider2D boxCollider2D;
	public Animator animator;
	public Direction direction = Direction.Front;
	public float speed;
	public float attackDistance = 0.1f;
	public float inteactDistance = 0.1f;

	[Header("Pull")]
	public bool isPulling;
	public bool isHorizontalPulling;
	private float linearDrag;

    [Header("Crouch")] 
    public bool isCrouching;
    public float Crouch_Stamina;

    [Header("Dodge")]
    public bool isDodging;
	public bool onceDodging;
	public bool isFreeze;
	Coroutine DodgeCoroutine;
    public float dodgePower;
    public float Dodge_Stamina;
	
	[Header("Reflect or Defend")]
	public bool isReflect;

	[Header("Bomb")]
	public BombSystem systemBomb;
    public float Bomb_Stamina;

	[Header("Interact")]
	public bool isInteracting;
	public bool onDialogue;
	private IInteractable interactable;
	
	[Header("Movable")]
    private Moveable _moveable;
	private float _moveX = 0.0f, _moveY = 0.0f;
	private Direction _moveDir  = Direction.Front;
	private BoxCollider2D _moveableColl = null;

    [Header("Attacking")]
    public float Attack_Stamina;
	public bool isAttacking;
	private int comboCounter = 0;
	public Equipment senjata;
    public float attackLaunch;
    public float knockbackForce;

	public GameObject Slider_Gayatri;
	private ComboSystem combo_Sys;

	public float Persentase_Perfect;
	public float Persentase_Good;
	public float Persentase_Miss;
    

	//private float _slower = 0.0f;
	// Use this for initialization
	void Start () {
        SetPlayer(this.gameObject);
		linearDrag = rigid2D.drag;
        combo_Sys = Slider_Gayatri.GetComponentInParent<ComboSystem>();
        isReflect = false;
        isFreeze = false;
        isCrouching = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ngetest
            DeployBomb();
        }
	}

	void FixedUpdate(){
        //rigid2D.velocity = rigid2D.velocity * _slower;
        if (isCrouching)
        {
            rigid2D.velocity = rigid2D.velocity * 2 / 5;
            boxCollider2D.isTrigger = true;
            Status.Decreased_Stamina(Crouch_Stamina*Time.deltaTime) ;
        }
        else
        {
           // boxCollider2D.isTrigger = false;
        }

		animator.SetFloat("Speed", rigid2D.velocity.sqrMagnitude);
		animator.SetBool("IsPulling", isPulling);
        animator.SetBool("IsDodging", isDodging);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isAttacking", isAttacking);
		
		switch(direction){
			case Direction.Front:
				animator.SetFloat("MoveX", 0);
				animator.SetFloat("MoveY", -1);
				_moveX = 0;
				_moveY = -1;
				break;
			case Direction.Right:
				animator.SetFloat("MoveX", 1);
				animator.SetFloat("MoveY", 0);
				_moveX = 1;
				_moveY = 0;
				break;
			case Direction.Left:
				animator.SetFloat("MoveX", -1);
				animator.SetFloat("MoveY", 0);
				_moveX = -1;
				_moveY = 0;
				break;
			case Direction.Back:
				animator.SetFloat("MoveX", 0);
				animator.SetFloat("MoveY", 1);
				_moveX = 0;
				_moveY = 1;
				break;
		}

		if(isPulling){
			if(_moveable != null){
				Vector2 _moveableNewPos = Vector2.zero;
				switch(_moveDir){
					case Direction.Back:
						_moveableNewPos = new Vector2(_moveable.transform.position.x, transform.position.y+_moveableColl.size.y+boxCollider2D.offset.y*0.5f);
						break;
					case Direction.Front:
						_moveableNewPos = new Vector2(_moveable.transform.position.x, transform.position.y-_moveableColl.size.y+boxCollider2D.offset.y*0.5f);
						break;
					case Direction.Left:
						_moveableNewPos = new Vector2(transform.position.x-_moveableColl.size.x+boxCollider2D.offset.x*0.5f, _moveable.transform.position.y);
						break;
					case Direction.Right:
						_moveableNewPos = new Vector2(transform.position.x+_moveableColl.size.x+boxCollider2D.offset.x*0.5f, _moveable.transform.position.y);
						break;
				}
				_moveable.rb_Object.MovePosition(_moveableNewPos);
			}
			//Debug.Log("Panjang move " + Vector2.Distance(_moveable.transform.position, transform.position));
			if(Vector2.Distance(_moveable.transform.position, (transform.position + (Vector3) boxCollider2D.offset*0.5f ) )> 1.5f){
				UnPull();
			}
		}
		
	}


	public void SetDirection(Direction _direction){
		if(!isPulling){
			direction = _direction;
		}else{
			if(isHorizontalPulling && (_direction == Direction.Left || _direction == Direction.Right)){
				direction = _direction;
			}else if(!isHorizontalPulling && (_direction == Direction.Back || _direction == Direction.Front)){
				direction = _direction;
			}
		}
	}

	public void Move(float x, float y){

		if(onDialogue || isAttacking)
		{
			return;
		}

		if(x > 0.6f){
			SetDirection(Direction.Right);
		}else if(x < -0.6f){
			SetDirection(Direction.Left);
		}

		if(y > 0.6f){
			SetDirection(Direction.Back);
		}else if(y < -0.6f){
			SetDirection(Direction.Front);
		}

		float _speed = speed;
		if(isPulling){
			_speed = speed * 0.25f;
			rigid2D.drag = linearDrag * 2f;
		}else{
			rigid2D.drag = linearDrag;
		}

        if (!isFreeze)
        {
            if (rigid2D.velocity.sqrMagnitude < _speed)
            {
                if (!isPulling)
                {
                    rigid2D.AddForce(new Vector2(x, y) * rigid2D.mass / Time.fixedDeltaTime);
                }
                else
                {
                    if (isHorizontalPulling)
                    {
                        rigid2D.AddForce(new Vector2(x, 0f) * rigid2D.mass / Time.fixedDeltaTime);
                    }
                    else
                    {
                        rigid2D.AddForce(new Vector2(0f, y) * rigid2D.mass / Time.fixedDeltaTime);
                    }
                }
            }
        }
	}
	public void Interact(){
		if(!isInteracting){
			isInteracting = true;
			
			Vector2 _castDir = Vector2.zero; 
			switch(direction){
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

			RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCollider2D.transform.position, boxCollider2D.size, 0.0f, _castDir, inteactDistance);
			Debug.Log("Jumlah hits ada "+hits.Length);

			for(int i =0; i < hits.Length; i++){
				if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
					interactable = hits[i].collider.gameObject.GetComponent<IInteractable>();
					if(interactable != null){
						Debug.Log("Berinteraksi dengan "+ hits[i].collider.gameObject.name);
						interactable.ApplyInteract(gameObject);
					}
				}
			}

			if(hits.Length == 1 && !onDialogue){
				isInteracting = false;
			}

		}else{
			//TODO disini harusnya pas interact ngapain kayak dialog, bisa pilih gitu juga, atau notifikasi
			if(interactable != null){
				interactable.ApplyInteract(gameObject);
			}else{
				isInteracting = false;
			}
		}
    }

    public void Stop_RegenStamina()
    {
        GameManager.Instance.m_StatusManager.Stop_Regenerating_Stamina();
    }

    public void Start_RegenStamina()
    {
        GameManager.Instance.m_StatusManager.Start_Regenerating_Stamina();
    }

    public void Decreased_Stamina()
    {
        if (isAttacking)
        {
            //disini diisi Attacknya berapa
            Status.Decreased_Stamina(Attack_Stamina);
        }
        if (isDodging)
        {
            Status.Decreased_Stamina(Dodge_Stamina);
        }
    }

    #region Attack	
    
    public void Start_Attacking()
    {
        if (Status.currentStamina > Attack_Stamina)
        {
            isAttacking = true;
        }
    }
	public void Attacking()
	{
        rigid2D.velocity = Vector2.zero;

        ComboFeedback feedback = combo_Sys.FilterCombo(comboCounter, senjata);
		ComboEnum comboPlayer = feedback.combo;
		comboCounter = feedback.counter;
		
		float TempDamage = 0;
    	Debug.Log("Attacking is " + comboPlayer + " dengan combo " + comboCounter);
        
		if(comboPlayer == ComboEnum.Perfect)
		{
			TempDamage = (senjata.Damage/100) * Persentase_Perfect;
		}
		else if(comboPlayer == ComboEnum.Good)
		{
			TempDamage = (senjata.Damage/100) * Persentase_Good;
		}
		else if(comboPlayer == ComboEnum.Miss)
		{
			TempDamage = (senjata.Damage/100) * Persentase_Miss;
		}
		/*
		Manggil Combonya gimana?

		Method Combonya -> ComboSystem.FilterCombo(Equipment,ComboBerapa)
		 */

		Vector2 _castDir = Vector2.zero; 
		switch(direction){
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

        rigid2D.AddForce(_castDir * attackLaunch);
		RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCollider2D.transform.position, boxCollider2D.size, 0.0f, _castDir, attackDistance);
		//Debug.Log("Jumlah hits ada "+hits.Length);

		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				IAttackable attackable = hits[i].collider.gameObject.GetComponent<IAttackable>();
				if(attackable != null){
					Debug.Log("Menyerang "+ hits[i].collider.gameObject.name);
                    if (comboPlayer == ComboEnum.Perfect)
                    {
                        attackable.ApplyDamage(TempDamage, gameObject, DamageType.Critical);
                    }
                    else
                    {
                        attackable.ApplyDamage(TempDamage, gameObject, DamageType.Normal);
                    }
				}
			}
		}
		//CancelInvoke("UnAttack");
		//Invoke("UnAttack",senjata.attackSpeed[comboCounter].wait);
	}

	public void UnAttack(){

		isAttacking = false;
	}

	#endregion Attack

	#region Pull
	public bool Pull()
	{
		if(isPulling) return false;

		animator.SetFloat("TempX", _moveX);
		animator.SetFloat("TempY", _moveY);

		Vector2 _castDir = Vector2.zero; 
		switch(direction){
			case Direction.Back:
				_castDir = Vector2.up;
				isHorizontalPulling = false;
				break;
			case Direction.Front:
				_castDir = Vector2.down;
				isHorizontalPulling = false;
				break;
			case Direction.Left:
				_castDir = Vector2.left;
				isHorizontalPulling = true;
				break;
			case Direction.Right:
				_castDir = Vector2.right;
				isHorizontalPulling = true;
				break;
		}

		RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCollider2D.transform.position, boxCollider2D.size, 0.0f, _castDir, 0.25f);
		//Debug.Log("Jumlah hits ada "+hits.Length);

		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				_moveable = hits[i].collider.gameObject.GetComponent<Moveable>();
				if(_moveable != null){
					
					Debug.Log("Moveable object "+ hits[i].collider.gameObject.name);
					isPulling = true;
					_moveableColl = _moveable.boxCollider;
					_moveDir = direction;		
					
					return true;
				}
			}
		}
		return false;
	}

	IEnumerator MovingObject(float delay)
	{
		yield return new WaitForSeconds(delay);
		if(_moveable != null){
			switch(direction){
				case Direction.Back:
					_moveable.transform.position = new Vector2(_moveable.transform.position.x, transform.position.y -1.29f);
					break;
				case Direction.Front:
					_moveable.transform.position = new Vector2(_moveable.transform.position.x, transform.position.y+1.29f);
					break;
				case Direction.Left:
					_moveable.transform.position = new Vector2(transform.position.x-1.29f, _moveable.transform.position.y);
					break;
				case Direction.Right:
					_moveable.transform.position = new Vector2(transform.position.x+1.29f, _moveable.transform.position.y);
					break;
			}
		}
		
	}

	public void UnPull()
	{
		if(isPulling){
			_moveable.transform.SetParent(null);
			_moveable.rb_Object.isKinematic = false;
			_moveable = null;
			isPulling = false;
		}
	}

	#endregion Pull

	public void Pickup()
	{
		Debug.Log("Pickup");
		RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCollider2D.transform.position, boxCollider2D.size, 0.0f, Vector3.zero);
		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				Pickupable pickupable = hits[i].collider.gameObject.GetComponent<Pickupable>();
				Debug.Log("Pickup Item");
				if(pickupable != null){
					pickupable.PickupItem(gameObject);
					return;
				}
			}
		}
	}

    public void DeployBomb()
    {
        /*
         * Disini harusnya masuk ke state Start_Bomb(transform.position)
         */
        if (Status.currentStamina >= Bomb_Stamina)
        {
            systemBomb.Start_Bomb(transform.position);
            Status.Decreased_Stamina(Bomb_Stamina);
        }

    }

    public void OnReflect()
    {
        isReflect = true;
        
    }

	#region Dodge
    public void OnDodging()
    {
        animator.SetFloat("Speed", 0);
        if (!onceDodging)
            {
				onceDodging = true;
                isFreeze = true;
                isDodging = true;
            //DodgeCoroutine = 
            // StartCoroutine(Dodging());
            //StartDodge();
        }
    }
	public bool CheckCanDodge()
	{
		Vector2 _castDir = Vector2.zero; 
		switch(direction){
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

		RaycastHit2D[] hits = Physics2D.BoxCastAll((boxCollider2D.transform.position + (Vector3)boxCollider2D.offset), boxCollider2D.size, 0.0f, _castDir, 0.25f);
		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				if(hits[i].collider.gameObject.layer == 9) // Jika unwalkable
				{
					return false;
				}
			}
		}

		return true;
	}

	public void StartDodge()
	{
        if (Status.currentStamina >= Dodge_Stamina)
        {
            isDodging = true;
        }
	}

    public void Dodge()
    {
        boxCollider2D.isTrigger = true;
        rigid2D.AddForce(new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")) * rigid2D.mass * dodgePower / Time.deltaTime);
        if (CheckCanDodge())
        {
            rigid2D.velocity = Vector2.zero;
            //switch (direction)
            //{
            //	case Direction.Back:
            //		rigid2D.AddForce(Vector2.up * rigid2D.mass * dodgePower / Time.deltaTime);
            //		break;
            //	case Direction.Front:
            //		rigid2D.AddForce(Vector2.down * rigid2D.mass * dodgePower / Time.deltaTime);
            //		break;
            //	case Direction.Left:
            //		rigid2D.AddForce(Vector2.left * rigid2D.mass * dodgePower / Time.deltaTime);
            //		break;
            //	case Direction.Right:
            //		rigid2D.AddForce(Vector2.right * rigid2D.mass * dodgePower / Time.deltaTime);
            //		break;
            //}
        }
    }

	public void EndDodge()
    {
		//StopCoroutine(DodgeCoroutine);
		boxCollider2D.isTrigger = false;
        isDodging = false;
        onceDodging = false;
        isFreeze = false;
	}

    #endregion Dodge

    #region Crouch

    public void StartCrouch()
    {
        isCrouching = true;

        /*         
            Apa yang dilakukan disini?
            -  isCrouching is true
            - Animate Crouching.
            - Start Decreasing Stamina
        */


    }

    public void EndCrouch()
    {
        isCrouching = false;

        //Apa yang dilakukan disini?
        /*
         * - isCrouching is False
         * - Stop Animate Crouching
         * - Stop Decreasing Stamina
         */
    }

#endregion
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        if (isReflect)
        {
            //masih ngaco.
            Status.Stop_Regenerating_Stamina();
        }
        else
        {
            Debug.Log("On attack");
            Status.Decreased_Health(damage);
            Knockback(causer.transform);
        }

    }

    public void Destruct()
    {
        
    }

    public void Die()
    {
        
    }

    public void Knockback(Transform causer)
    {
        Knockback(causer, knockbackForce);
    }

    public void Knockback(Transform causer, float power = 0)
    {
        Vector2 force = (transform.position - causer.position).normalized * power;
        rigid2D.AddForce(force);
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other){
            if (onceDodging && other.gameObject.layer == 9) //jika dodge dan layer unwalkable
            {
                rigid2D.velocity = Vector2.zero;
            }
            else if (isCrouching && other.gameObject.layer == 9)
            {
                rigid2D.velocity = Vector2.zero;
                isCrouching = false;
            }
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other){
            if (onceDodging && other.gameObject.layer == 9) //jika dodge dan layer unwalkable
            {
                rigid2D.velocity = Vector2.zero;
            }
            else if (isCrouching && other.gameObject.layer == 9)
            {
                rigid2D.velocity = Vector2.zero;
                isCrouching = false;
            }
		}
	}

}
