using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(AIMovementComp))]
public class AITargetComp : MonoBehaviour {
	
	[Panda.Task]
	public bool isCanFire{get{return fireRateCounter <= 0;}}	
	public GameObject objectAiming;
	public Transform[] offsetProjectile;
	public float turnRate = 30f;
	public float fireRate = 1f;
	private float fireRateCounter, tempFireRate = -1f, tempFireCacl;
	private AIMovementComp _movementComp;
	
	[Header("Projectile")]
	public GameObject prefab;

	[Panda.Task]
	public void Fire(){
		Fire(transform.position, transform.rotation);
		Panda.Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithOffset(){
		if(offsetProjectile.Length > 0){
			for(int i = 0; i < offsetProjectile.Length; i++){
				Fire(offsetProjectile[i].position, offsetProjectile[i].rotation);
			}
			Panda.Task.current.Succeed();
		}else{
			Panda.Task.current.Fail();
		}
	}

	[Panda.Task]
	public void FireWithOffset(int index){
		if(offsetProjectile.Length > 0 && index < offsetProjectile.Length){
			Fire(offsetProjectile[index].position, offsetProjectile[index].rotation);
			Panda.Task.current.Succeed();
		}else{
			Panda.Task.current.Fail();
		}
	}

	[Panda.Task]
	public void FireWithRandomOffset(){
		int index = Random.Range(0, offsetProjectile.Length);
		if(offsetProjectile.Length > 0 && index < offsetProjectile.Length){
			Fire(offsetProjectile[index].position, offsetProjectile[index].rotation);
			Panda.Task.current.Succeed();
		}else{
			Panda.Task.current.Fail();
		}
	}

	[Panda.Task]
	public void FireDirection(float x, float y){
		FireDirection(new Vector2(x,y));
		Panda.Task.current.Succeed();
	}
	
	public void Fire(Vector2 position, Quaternion rotation){
		CalcFireRate();
		PoolManager.Instance.ReuseObject(prefab, position, rotation);	
	}

	public void FireDirection(Vector2 position){
		Quaternion rotation;

		switch(_movementComp.GetDirection()){
			case Direction.Back:
				rotation = Quaternion.Euler(0f,0f,0f);
				break;
			case Direction.Front:
				rotation = Quaternion.Euler(0f,0f,180f);
				break;
			case Direction.Left:
				rotation = Quaternion.Euler(0f,0f,90f);
				break;
			case Direction.Right:
				rotation = Quaternion.Euler(0f,0f,270f);
				break;
			default:
				rotation = Quaternion.Euler(0f,0f,180f);
				break;
		}
		CalcFireRate();
		PoolManager.Instance.ReuseObject(prefab, position, rotation);
	}

	public void CalcFireRate(){
		if(tempFireRate != fireRate){
			tempFireRate = fireRate;
			tempFireCacl = 1/fireRate;
		}
		fireRateCounter = tempFireCacl;
	}

	public void AimTarget(){
		if(objectAiming && _movementComp.target){
			Quaternion newRotation  = Quaternion.LookRotation(objectAiming.transform.position - _movementComp.target.position, Vector3.forward);
			newRotation.x = objectAiming.transform.rotation.x;
			newRotation.y = objectAiming.transform.rotation.y;
			objectAiming.transform.rotation = Quaternion.Slerp(objectAiming.transform.rotation, newRotation, Time.deltaTime * turnRate );
		}
	}

	public void AimTurnClockwise(float speed = 10f){
		objectAiming.transform.Rotate(Vector3.back * speed * turnRate * Time.deltaTime);
	}

	public void AimTurnCounterClockwise(float speed = 10f){
		objectAiming.transform.Rotate(Vector3.forward * speed * turnRate * Time.deltaTime);
	}

	public void SetAimRotation(float angle){
		objectAiming.transform.Rotate(0,0,angle);
	}

	// Use this for initialization
	void Start () {
		_movementComp = GetComponent<AIMovementComp>();
		CalcFireRate();
	}

	void Update(){
		
		if(isCanFire){
			//SetAimRotation(Random.Range(0,360));
			for(int i = 0; i < offsetProjectile.Length; i++){
				Fire(offsetProjectile[i].position, offsetProjectile[i].rotation);
			}
		}
		if(fireRateCounter > 0 ){
			fireRateCounter -= Time.deltaTime;
		}
	}
	
}
