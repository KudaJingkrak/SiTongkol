using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

[RequireComponent(typeof(AIMovementComp))]
public class AITargetComp : MonoBehaviour {
	public Transform targetAim;

	[Panda.Task]
	public bool isCanFire{get{return fireRateCounter <= 0;}}
	public ProjectileSpawner[] spawners;
	public float turnRate = 30f;
	public float fireRate = 1f;
	private float fireRateCounter, tempFireRate = -1f, tempFireCacl;
	private AIMovementComp _movementComp;
	
	[Header("Projectile")]
	public GameObject prefabSelected;
	public GameObject[] prefabs = new GameObject[1];

	#region prefabs_management
	[Task]
	public void SelectPrefab(int index){
		if(index < prefabs.Length){
			prefabSelected = prefabs[index];
		}
		if(Task.isInspected)
			Task.current.Succeed();
	}

	#endregion
	

	[Panda.Task]
	public void Fire(){
		Fire(transform.position, transform.rotation);
		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithOffset(){
		if(spawners != null)
		{
			for(int i = 0; i < spawners.Length; i++)
			{
				for(int j = 0; j < spawners[i].offsetsProjectile.Length; j++)
				{
					Fire(spawners[i].offsetsProjectile[j].transform);
				
				}
			
			}
		
		}

		if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithOffset(int spawnerIndex = 0){
		if(spawners != null)
		{
			if(spawnerIndex > -1 && spawnerIndex < spawners.Length)
			{
				for(int j = 0; j < spawners[spawnerIndex].offsetsProjectile.Length; j++)
				{
					Fire(spawners[spawnerIndex].offsetsProjectile[j].transform);

				}

			}

		}

		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithOffset(int spawnerIndex, int offsetIndex){
		if(spawners != null)
		{
			if(spawnerIndex > -1 && spawnerIndex < spawners.Length)
			{
				if(offsetIndex > -1 && offsetIndex < spawners[spawnerIndex].offsetsProjectile.Length)
				{
					Fire(spawners[spawnerIndex].offsetsProjectile[offsetIndex].transform);

				}

			}

		}
		
		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithRandomSpawner(){
		int spawnerIndex = Random.Range(0, spawners.Length);
		
		for(int i = 0; i < spawners[spawnerIndex].offsetsProjectile.Length; i++)
		{
			Fire(spawners[spawnerIndex].offsetsProjectile[i].transform);

		}
		
		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireWithRandomOffset(int spawnerIndex){
		if(spawnerIndex > -1 && spawnerIndex < spawners.Length)
		{
			int offsetIndex = Random.Range(0, spawners[spawnerIndex].offsetsProjectile.Length);
			Fire(spawners[spawnerIndex].offsetsProjectile[offsetIndex].transform);

		}

		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Panda.Task]
	public void FireDirection(float x, float y){
		FireDirection(new Vector2(x,y));
		// if(Task.isInspected)
			Task.current.Succeed();

	}

	public void Fire(Transform objTransform){
		CalcFireRate();
		PoolManager.Instance.ReuseObject(prefabSelected, objTransform.position, objTransform.rotation);	
	}
	
	public void Fire(Vector2 position, Quaternion rotation){
		CalcFireRate();
		PoolManager.Instance.ReuseObject(prefabSelected, position, rotation);	
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
		PoolManager.Instance.ReuseObject(prefabSelected, position, rotation);
	}

	public void CalcFireRate(){
		if(tempFireRate != fireRate){
			tempFireRate = fireRate;
			tempFireCacl = 1/fireRate;
		}
		fireRateCounter = tempFireCacl;
	}

	[Task]
	public void AimTarget(){
		if(!targetAim && _movementComp.target){
			targetAim = _movementComp.target;
		}

		if(targetAim){
			for (int i= 0; i < spawners.Length; i++)
			{
				Quaternion newRotation  = Quaternion.LookRotation(spawners[i].rootAiming.transform.position - targetAim.position, Vector3.forward);
				newRotation.x = spawners[i].rootAiming.transform.rotation.x;
				newRotation.y = spawners[i].rootAiming.transform.rotation.y;
				spawners[i].rootAiming.transform.rotation = Quaternion.Slerp(spawners[i].rootAiming.transform.rotation, newRotation, Time.deltaTime * turnRate );

			}
			// if(Task.isInspected)
				Task.current.Succeed();
		}else{
			// if(Task.isInspected)
				Task.current.Fail();
		}

	}

	[Task]
	public void AimTarget(int spawnerIndex = 0){
		if(!targetAim && _movementComp.target){
			targetAim = _movementComp.target;
		}

		if(targetAim){
			if (spawnerIndex > -1 && spawnerIndex < spawners.Length)
			{
				Quaternion newRotation  = Quaternion.LookRotation(spawners[spawnerIndex].rootAiming.transform.position - targetAim.position, Vector3.forward);
				newRotation.x = spawners[spawnerIndex].rootAiming.transform.rotation.x;
				newRotation.y = spawners[spawnerIndex].rootAiming.transform.rotation.y;
				spawners[spawnerIndex].rootAiming.transform.rotation = Quaternion.Slerp(spawners[spawnerIndex].rootAiming.transform.rotation, newRotation, Time.deltaTime * turnRate );

			}
			// if(Task.isInspected)
				Task.current.Succeed();
		}else{
			// if(Task.isInspected)
				Task.current.Fail();
		}

	}

	[Task]
	public void AimTurnClockwise(float speed = 10f){
		for (int i= 0; i < spawners.Length; i++)
		{
			spawners[i].rootAiming.transform.Rotate(Vector3.back * speed * turnRate * Time.deltaTime);
		}
		
		// if(Task.isInspected)
			Task.current.Succeed();
		
	}

	[Task]
	public void AimTurnClockwise(float speed = 10f, int index = 0){
		if(index > 0 && index < spawners.Length)
		{
			spawners[index].rootAiming.transform.Rotate(Vector3.back * speed * turnRate * Time.deltaTime);
			// if(Task.isInspected)
				Task.current.Succeed();

		}
		else
		{
			// if(Task.isInspected)
				Task.current.Fail();
		}
	}

	[Task]
	public void AimTurnCounterClockwise(float speed = 10f){
		for (int i= 0; i < spawners.Length; i++)
		{
			spawners[i].rootAiming.transform.Rotate(Vector3.forward * speed * turnRate * Time.deltaTime);
		}
		// if(Task.isInspected)
			Task.current.Succeed();
	}

	[Task]
	public void AimTurnCounterClockwise(float speed = 10f, int index = 0){
		if(index > 0 && index < spawners.Length){
			spawners[index].rootAiming.transform.Rotate(Vector3.forward * speed * turnRate * Time.deltaTime);
			// if(Task.isInspected)
				Task.current.Succeed();
		}
		else
		{
			// if(Task.isInspected)
				Task.current.Fail();

		}
	}

	[Task]
	public void SetAimRotation(float angle){
		for (int i= 0; i < spawners.Length; i++)
		{
			spawners[i].rootAiming.transform.rotation = Quaternion.Euler(0,0,angle);
		}
		
		if(Task.isInspected)
			Task.current.Succeed();
		
	}

	[Task]
	public void SetAimRotation(float angle, int index = 0){
		if(index > 0 && index < spawners.Length)
		{
			spawners[index].rootAiming.transform.rotation = Quaternion.Euler(0,0,angle);
			// if(Task.isInspected)
				Task.current.Succeed();

		}
		else
		{
			// if(Task.isInspected)
			try{
				Task.current.Fail();	
			}finally{}
		
		}
	}

	// Use this for initialization
	void Start () {
		_movementComp = GetComponent<AIMovementComp>();
		CalcFireRate();
		SelectPrefab(0);
	}

	void Update(){

        // if (isCanFire)
        // {
            //FireWithOffset();
        // }
		if(fireRateCounter > 0 ){
			fireRateCounter -= Time.deltaTime;
		}
	}
	
}

[System.Serializable]
public class ProjectileSpawner{
	public GameObject rootAiming;
	public Transform[] offsetsProjectile;
}
