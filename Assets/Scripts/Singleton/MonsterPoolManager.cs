using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoBehaviour {
	Dictionary<int, Queue<MonsterInstance>> tierOneDictionary = new Dictionary<int, Queue<MonsterInstance>>();
	Dictionary<int, Queue<MonsterInstance>> tierTwoDictionary = new Dictionary<int, Queue<MonsterInstance>>();
	Dictionary<int, Queue<MonsterInstance>> tierThreeDictionary = new Dictionary<int, Queue<MonsterInstance>>();
	
	public static MonsterPoolManager _instance;

	 public static MonsterPoolManager Instance
    {
        get
        {
			if (_instance == null) {
				_instance = FindObjectOfType<MonsterPoolManager> ();
			}
            return _instance;
        }
    }

	public void CreatePool(Tier tier, GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID ();
		
		switch(tier)
		{
			case Tier.TierOne:
			if(!tierOneDictionary.ContainsKey(poolKey))
			{
				tierOneDictionary.Add(poolKey, new Queue<MonsterInstance>());
				GameObject poolHolder = new GameObject (prefab.name + " pool");
				poolHolder.transform.parent = transform;

				for (int i = 0; i < poolSize; i++) {
					MonsterInstance newMonster = new MonsterInstance(Instantiate(prefab) as GameObject);
					tierOneDictionary[poolKey].Enqueue(newMonster);
					newMonster.SetParent(poolHolder.transform);

				}

			}

			break;
			
			case Tier.TierTwo:
			if(!tierTwoDictionary.ContainsKey(poolKey))
			{
				tierTwoDictionary.Add(poolKey, new Queue<MonsterInstance>());
				GameObject poolHolder = new GameObject (prefab.name + " pool");
				poolHolder.transform.parent = transform;

				for (int i = 0; i < poolSize; i++) {
					MonsterInstance newMonster = new MonsterInstance(Instantiate(prefab) as GameObject);
					tierTwoDictionary[poolKey].Enqueue(newMonster);
					newMonster.SetParent(poolHolder.transform);

				}

			}

			break;
			
			case Tier.TierThree:
			if(!tierThreeDictionary.ContainsKey(poolKey))
			{
				tierThreeDictionary.Add(poolKey, new Queue<MonsterInstance>());
				GameObject poolHolder = new GameObject (prefab.name + " pool");
				poolHolder.transform.parent = transform;

				for (int i = 0; i < poolSize; i++) {
					MonsterInstance newMonster = new MonsterInstance(Instantiate(prefab) as GameObject);
					tierThreeDictionary[poolKey].Enqueue(newMonster);
					newMonster.SetParent(poolHolder.transform);

				}

			}

			break;

		}

	}

	public void ReuseObject(Tier tier, GameObject prefab, Vector3 position, Quaternion rotation) {
		int poolKey = prefab.GetInstanceID ();

		switch(tier)
		{
			case Tier.TierOne:
			if (tierOneDictionary.ContainsKey (poolKey)) {
				MonsterInstance monsterToReuse = tierOneDictionary [poolKey].Dequeue ();
				tierOneDictionary [poolKey].Enqueue (monsterToReuse);

				monsterToReuse.Reuse (position, rotation);
			}

			break;
			
			case Tier.TierTwo:
			if (tierTwoDictionary.ContainsKey (poolKey)) {
				MonsterInstance monsterToReuse = tierTwoDictionary [poolKey].Dequeue ();
				tierTwoDictionary [poolKey].Enqueue (monsterToReuse);

				monsterToReuse.Reuse (position, rotation);
			}

			break;
			
			case Tier.TierThree:
			if (tierThreeDictionary.ContainsKey (poolKey)) {
				MonsterInstance monsterToReuse = tierThreeDictionary [poolKey].Dequeue ();
				tierThreeDictionary [poolKey].Enqueue (monsterToReuse);

				monsterToReuse.Reuse (position, rotation);
			}

			break;

		}
	}
	
	public class MonsterInstance 
	{

		GameObject gameObject;
		Transform transform;

		bool hasPoolObjectComponent;
		PoolObject poolObjectScript;

		public MonsterInstance(GameObject objectInstance) {
			gameObject = objectInstance;
			transform = gameObject.transform;
			gameObject.SetActive(false);

			if (gameObject.GetComponent<PoolObject>()) {
				hasPoolObjectComponent = true;
				poolObjectScript = gameObject.GetComponent<PoolObject>();
			}
		}

		public void Reuse(Vector3 position, Quaternion rotation) {
			gameObject.SetActive (true);
			transform.position = position;
			transform.rotation = rotation;

			if (hasPoolObjectComponent) {
				poolObjectScript.OnObjectReuse ();
			}
		}

		public void SetParent(Transform parent) {
			transform.parent = parent;
		}
	}
}
