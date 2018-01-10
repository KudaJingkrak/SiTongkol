using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayatriCharacter : MonoBehaviour {
	public Rigidbody2D rigid2D;
	public Animator animator;
	public Direction direction = Direction.Front;
	public float speed;
	[Range(0,1)]
	public float linearDampling = 0.05f;
	public bool isPulling;
	public bool isHorizontalPulling;
	public bool isLifting;
	private float _slower = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_slower = 1 - linearDampling;
	}

	void FixedUpdate(){
		rigid2D.velocity = rigid2D.velocity * _slower;

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

	public void Attack(){
		Vector2 _size;
		switch(direction){
			case Direction.Back:
				_size = new Vector2(0f,0f);
				break;
			case Direction.Front:
				_size = new Vector2(0f,0f);
				break;
			case Direction.Left:
				_size = new Vector2(0f,0f);
				break;
			case Direction.Right:
				_size = new Vector2(0f,0f);
				break;
		}
		Physics2D.BoxCastAll(transform.position, Vector2.zero, 0, Vector2.zero);
	}
}
