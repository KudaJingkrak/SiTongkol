using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriCharacter : MonoBehaviour {
	public Rigidbody2D rigid2D;
	public Animator animator;
	public Direction direction = Direction.Front;
	public float speed;
	public float attackDistance = 0.1f;
	public bool isPulling;
	public bool isHorizontalPulling;
	public bool isLifting;
	public bool isAttacking;
	public bool isInteracting;
	public bool onDialogue;
	private IInteractable interactable;
	//private float _slower = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
				break;
			case Direction.Right:
				animator.SetFloat("MoveX", 1);
				animator.SetFloat("MoveY", 0);
				break;
			case Direction.Left:
				animator.SetFloat("MoveX", -1);
				animator.SetFloat("MoveY", 0);
				break;
			case Direction.Back:
				animator.SetFloat("MoveX", 0);
				animator.SetFloat("MoveY", 1);
				break;
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
		if(rigid2D.velocity.sqrMagnitude < speed){
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

			RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + new Vector3(0f,-0.1f), new Vector2(0.5f, 0.5f), 0.0f, _castDir, 0.1f);
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
			interactable.ApplyInteract(gameObject);
		}
	}
	
	public void Attack(float delay = 0.1f){
		if(!isAttacking){
			StartCoroutine(Attacking(delay));
		}
	}

	IEnumerator Attacking(float delay){
		isAttacking = true;

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

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + new Vector3(0f,-0.1f), new Vector2(0.5f, 0.5f), 0.0f, _castDir, attackDistance);
		//Debug.Log("Jumlah hits ada "+hits.Length);

		for(int i =0; i < hits.Length; i++){
			if(hits[i].collider != null && !hits[i].collider.gameObject.Equals(this.gameObject)){
				IAttackable attackable = hits[i].collider.gameObject.GetComponent<IAttackable>();
				if(attackable != null){
					Debug.Log("Menyerang "+ hits[i].collider.gameObject.name);
					attackable.ApplyDamage(1.0f);
				}
			}
		}

		yield return new WaitForSeconds(delay);
		isAttacking = false;
	}
}
