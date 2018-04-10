using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class AIMovementComp : MonoBehaviour {
	public MovementType movementType = MovementType.Walk;
	private MovementType _movType;
	
	public Direction direction = Direction.Front;
	private Direction _dir;
	
	public float movementSpeed = 1f;
	private float _speed, x, y;
	private BoxCollider2D _boxColl2D;
	private Animator _anim;
	private Rigidbody2D _rigid2D;

	#region Movement
	public void SetDirection(Direction direction){
		this._dir = direction;
		switch(_dir){
			case Direction.Back:
				x = 0f; 
				y = 1f;
				break;
			case Direction.Right:
				x = 1f;
				y = 0f;
				break;
			case Direction.Front:
				x = 0f;
				y = -1f;
				break;
			case Direction.Left:
				x = -1f; 
				y = 0f;
				break;
		}
	}

	public void SetDirection(){
		if(Mathf.Abs(x) > Mathf.Abs(y)){
			if(x > 0){
				_dir = Direction.Right;
			}else{
				_dir = Direction.Left;
			}
		}else{
			if(y > 0){
				_dir = Direction.Back;
			}else{
				_dir = Direction.Front;
			}
		}
	}

	public Direction GetDirection(){
		return _dir;
	}

	public Vector2 GetVectorDirection(){
		switch(_dir){
			case Direction.Back:
				return Vector2.up;
			case Direction.Right:
				return Vector2.right;
			case Direction.Front:
				return Vector2.down;
			case Direction.Left:
				return Vector2.left;
		}
		return Vector2.zero;
	}
	[Panda.Task]
	public bool SetRandomDirection(){
		SetDirection((Direction)Random.Range(0, 4));
		return true;
	}

	public void Move(float x, float y){
		this.x = x;
		this.y = y;
		_rigid2D.velocity = new Vector3(x,y,0) * Mathf.Lerp(0f, _speed, 1f);
		SetDirection();
	}

	public void Move(Vector2 direction){
		this.x = direction.x;
		this.y = direction.y;
		_rigid2D.velocity = new Vector3(x,y,0) * Mathf.Lerp(0f, _speed, 1f);
		SetDirection();
	}
	[Panda.Task]
	public void MoveByDirection(){
		Move(x,y);
		Panda.Task.current.Succeed();
	}

	public void Launch(Vector2 direction, float power = 1f){
		Vector2 force = direction * power;
		this.x = direction.normalized.x;
		this.y = direction.normalized.y;
		_rigid2D.AddForce(force);
		SetDirection();
	}
	private IEnumerator QMoveEnum;
	public void QuickMove(Vector2 direction, float power = 1f, float timeStop = 0.1f){
		Vector2 velocity = direction * power;
		QuickMove(velocity, timeStop);
	}
	public void QuickMove(Vector2 velocity, float timeStop = 0.1f){
		this.x = velocity.normalized.x;
		this.y = velocity.normalized.y;
		if(QMoveEnum != null){
			StopCoroutine(QMoveEnum);
		}
		QMoveEnum = QMove(velocity, timeStop);
		StartCoroutine(QMoveEnum);
	}

	IEnumerator QMove(Vector2 velocity, float delay){
		_rigid2D.velocity = velocity;
		yield return new WaitForSeconds(delay);
		_rigid2D.velocity = Vector2.zero;
	}


	#endregion
	
	#region Stuck_Movement
	[Header("Stuck Movement")]
	[Tooltip("Distance to check is stuck")] public float stuckError = 0.25f;
	[Tooltip("Max step after stucked")] public int maxStep = 10;
	[Tooltip("Min step after stucked")] public int minStep = 0;
	private int _step;
	public bool isEndStep{get{return _step < 1;}}
	public bool isStuck{
		get{
			Vector2 _castDir;

			switch(_dir){
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
				default:
					_castDir = Vector2.down;
					break;
			}

			RaycastHit2D[] hits = Physics2D.BoxCastAll(_boxColl2D.transform.position, 
				_boxColl2D.size, 0f, _castDir, stuckError);
			
			for(int i = 0; i < hits.Length; i++){
				if(hits[i].collider.gameObject.layer == 9){ // if unwalkable
					return _movType != MovementType.Fly;
				}
			}
			return false;
		}
	}
	#endregion

	#region  Movement_Point
	[Header("Movement Point")]
	public Transform target;
	[Tooltip("Distance tolerance to target")]public float distanceError = 1f;
	public List<Transform> targetPoints;
	private int _indexPoint;
	public bool isReachTarget{
		get{
			if(target){
				return Vector2.Distance(target.position, transform.position) 
					<= distanceError;
			}
			return false;
		}
	}

	public bool SetPlayerToTarget(){
		if(target && target.gameObject.CompareTag("Player")) return true;

		//TODO kalo udah ada global manager diganti
		GayatriCharacter player = FindObjectOfType<GayatriCharacter>();	
		if(player){
			target = player.gameObject.transform;
			return true;
		}

		return false;
	}

	public void MoveToTarget(){
		if(target && !isReachTarget){
			Vector3 dir = (target.position - transform.position).normalized;
			Move(dir);			
		}
	}

	/**
	 * Move 1,2,3,1,2,3,1,...
	 */
	public void CircularMove(int index = -1){
		if(index >= targetPoints.Count) return;
		
		if(!target.position.Equals(targetPoints[_indexPoint].position)){
			target = targetPoints[_indexPoint];
		}

		if(isReachTarget){
			_indexPoint++;
			if(_indexPoint >= targetPoints.Count){
				_indexPoint = 0;
			}
		}

		MoveToTarget();
	}

	private bool isMoveDec = false; // move decrement or increment
	/**
	 * Move 1,2,3,2,1,2,3,2,...
	 */
	public void SequenceMove(int index = -1){
		if(index >= targetPoints.Count || targetPoints.Count < 2) return;
		
		if(!target.position.Equals(targetPoints[_indexPoint].position)){
			target = targetPoints[_indexPoint];
		}

		if(isReachTarget){
			if(!isMoveDec){
				_indexPoint++;
				if(_indexPoint >= targetPoints.Count){
					isMoveDec = true;
					_indexPoint = _indexPoint - 2;
				}
			}else{
				_indexPoint--;
				if(_indexPoint < 0){
					isMoveDec = false;
					_indexPoint = 1;
				}
			}
		}

		MoveToTarget();
	}

	#endregion

	void Awake(){
		_movType = movementType;
		_dir = direction;
		_speed = movementSpeed;
		_boxColl2D = GetComponent<BoxCollider2D>();
		_anim = GetComponent<Animator>();
		_rigid2D = GetComponent<Rigidbody2D>();
	}

	void Start(){
		
	}

	void Update(){
		if(_anim){
			_anim.SetFloat("MoveX", x);
			_anim.SetFloat("MoveY", y);
		}
	}
}
