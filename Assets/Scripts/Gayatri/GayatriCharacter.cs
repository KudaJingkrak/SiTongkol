using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriCharacter : MonoBehaviour {
	public Rigidbody2D rigid2D;
	public BoxCollider2D boxCollider2D;
	public Animator animator;
	public Direction direction = Direction.Front;
	public float speed;
	public float attackDistance = 0.1f;
	public float inteactDistance = 0.1f;
	public bool isPulling;
	public bool isHorizontalPulling;
	public bool isLifting;
	public bool isAttacking;
	public bool isInteracting;
	public bool onDialogue;
    public bool isDefend;
	private IInteractable interactable;
	private float linearDrag;
    public BombSystem systemBomb;

	//Movable
	private Moveable _moveable;
	private float _moveX = 0.0f, _moveY = 0.0f;
	private Direction _moveDir  = Direction.Front;
	private BoxCollider2D _moveableColl = null;

	private int comboCounter = 0;
	public Equipment senjata;

	public GameObject Slider_Gayatri;
	private ComboSystem combo_Sys;

	public float Persentase_Perfect;
	public float Persentase_Good;
	public float Persentase_Miss;


	//private float _slower = 0.0f;
	// Use this for initialization
	void Start () {
		linearDrag = rigid2D.drag;
        combo_Sys = Slider_Gayatri.GetComponentInParent<ComboSystem>();
        isDefend = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ngetest
            DeployBomb();
        }
		//_slower = 1 - linearDampling;
	}

	void FixedUpdate(){
		//rigid2D.velocity = rigid2D.velocity * _slower;

		animator.SetFloat("Speed", rigid2D.velocity.sqrMagnitude);
		animator.SetBool("IsPulling", isPulling);
		animator.SetBool("IsLifting", isLifting);
		
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
		if(onDialogue)
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
		
		if(rigid2D.velocity.sqrMagnitude < _speed){
			if(!isPulling){
				rigid2D.AddForce(new Vector2(x,y)*rigid2D.mass / Time.fixedDeltaTime);
			}else{
				if(isHorizontalPulling){
					rigid2D.AddForce(new Vector2(x,0f)*rigid2D.mass / Time.fixedDeltaTime);
				}else{
					rigid2D.AddForce(new Vector2(0f,y)*rigid2D.mass / Time.fixedDeltaTime);
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
	
	public void Attack(float delay = 0.1f){
		if(!isAttacking){
			StartCoroutine(Attacking(delay));
		}
	}

	/*
	Ketika dia men
	 */
	
	IEnumerator Attacking(float delay){

		isAttacking = true;
		float TempDamage = 0;
		if(combo_Sys.FilterCombo(senjata,comboCounter) == ComboEnum.Perfect)
		{
			TempDamage = (senjata.Damage/100) * Persentase_Perfect;
		}
		else if(combo_Sys.FilterCombo(senjata,comboCounter) == ComboEnum.Good)
		{
			TempDamage = (senjata.Damage/100) * Persentase_Good;
		}
		else if(combo_Sys.FilterCombo(senjata,comboCounter) == ComboEnum.Miss)
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

		RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCollider2D.transform.position, boxCollider2D.size, 0.0f, _castDir, attackDistance);
		//Debug.Log("Jumlah hits ada "+hits.Length);

		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				IAttackable attackable = hits[i].collider.gameObject.GetComponent<IAttackable>();
				if(attackable != null){
					Debug.Log("Menyerang "+ hits[i].collider.gameObject.name);
					attackable.ApplyDamage(TempDamage);
				}
			}
		}
		CancelInvoke("");
		yield return new WaitForSeconds(0.02f);
		Invoke("UnAttack",senjata.attackSpeed[comboCounter].wait);
	}

	void UnAttack(){
		isAttacking = false;
	}

	public bool Pull(){
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

	IEnumerator MovingObject(float delay){
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

	public void UnPull(){
		if(isPulling){
			_moveable.transform.SetParent(null);
			_moveable.rb_Object.isKinematic = false;
			_moveable = null;
			isPulling = false;
		}
	}

	public void Pickup(){
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
        systemBomb.DeployBomb(transform.position);
    }

    public void OnDefense()
    {
        isDefend = true;
        //terus nanti kaya masuk ke method OnTriggerEnter/OnCollision aja terus tinggal ganti ArahBulletnya aja. done.
    }

    
}
