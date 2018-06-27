using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using Naga.Dungeon;

public class DungeonManager : MonoBehaviour { 
	private static DungeonManager _instance;
    public static DungeonManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<DungeonManager>();
            }
            return _instance;
        }
    }
	public int currentRoomIndex;
	public DungeonRoom[] rooms = new DungeonRoom[1];
	[HideInInspector]
	public ProCamera2DRooms pc2dr;

	[Header("Monster Pool Management")]
	public List<MonsterDungeon> tierOne = new List<MonsterDungeon>();
	public List<MonsterDungeon> tierTwo = new List<MonsterDungeon>();
	public List<MonsterDungeon> tierThree = new List<MonsterDungeon>();

	private void CreatePoolMonster()
	{
		for(int i = 0; i < tierOne.Count; i++)
		{
			MonsterPoolManager.Instance.CreatePool(Tier.TierOne, tierOne[i].monster, tierOne[i].size);
		}

		for(int i = 0; i < tierTwo.Count; i++)
		{
			MonsterPoolManager.Instance.CreatePool(Tier.TierTwo, tierTwo[i].monster, tierTwo[i].size);
		}

		for(int i = 0; i < tierThree.Count; i++)
		{
			MonsterPoolManager.Instance.CreatePool(Tier.TierThree, tierThree[i].monster, tierThree[i].size);
		}
	}

	private void Awake()
	{
		// Instantiate Dungeon Manager
		if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

		// Catch Pro Camera 2D Room
		pc2dr = Camera.main.gameObject.GetComponent<ProCamera2DRooms>();
		if(pc2dr)
		{
			//rooms = new DungeonRoom[pc2dr.Rooms.Count];

			for(int i = 0; i < rooms.Length; i++)
			{
				//rooms[i] = new DungeonRoom();
				rooms[i].position = pc2dr.Rooms[i].Dimensions.position;

			}
		}

	}

	void Start () 
	{
		CreatePoolMonster();
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(pc2dr)
		{	
			currentRoomIndex = pc2dr.CurrentRoomIndex;
			for(int i = 0; i < rooms.Length; i++)
			{
				if(i == currentRoomIndex){
					rooms[i].isPlayerHere = true;
					rooms[i].isExplored = true;
					if(!rooms[i].isRespawned)
					{
						RespawnAllEnemies(rooms[i]);

					}
					
				}else{
					rooms[i].isPlayerHere = false;
					if(rooms[i].isRespawned)
					{
						DestroyAllEnemies(rooms[i]);

					}

				}
				
			}
		}
	}

	public void RespawnAllEnemies(DungeonRoom room)
	{
		room.isRespawned = true;
		for(int i = 0; i < room.monster.Count; i++)
		{	
			MonsterDungeon instance = null;
			switch(room.monster[i].tier)
			{
				case Tier.TierOne:
					instance = tierOne[Random.Range(0, tierOne.Count)];
					MonsterPoolManager.Instance.ReuseObject(Tier.TierOne, instance.monster, room.respwanPoint[i].position, room.respwanPoint[i].rotation);
					break;
				case Tier.TierTwo:
					instance = tierTwo[Random.Range(0, tierTwo.Count)];
					MonsterPoolManager.Instance.ReuseObject(Tier.TierTwo, instance.monster, room.respwanPoint[i].position, room.respwanPoint[i].rotation);
					break;
				case Tier.TierThree:
					instance = tierThree[Random.Range(0, tierThree.Count)];
					MonsterPoolManager.Instance.ReuseObject(Tier.TierThree, instance.monster, room.respwanPoint[i].position, room.respwanPoint[i].rotation);
					break;
			}
		}
	}

	public void DestroyAllEnemies(DungeonRoom room)
	{
		for(int i = 0; i < room.enemies.Count; i++)
		{
			IAttackable attakable = room.enemies[i].GetComponent<IAttackable>();
			if(attakable != null)
			{
				attakable.Die();
				return;
			}
			
			attakable = room.enemies[i].GetComponentInChildren<IAttackable>();
			if(attakable != null)
			{
				attakable.Die();
				return;
			}

		}
		room.isRespawned = false;
	}

	[System.Serializable]
	public class MonsterDungeon
	{
		public GameObject monster;
		public int size;
	}

}
[System.Serializable]
public class DungeonRoom{
	public Vector2 position = Vector2.zero;
	public bool isExplored = false;
	public bool isPlayerHere = false;
	[Header("Enemies Handle")]
	public bool isRespawnable = true;
	[HideInInspector]
	public bool isRespawned = false;
	public bool isSkipable = true;
	public int numberOfEnemies
	{
		get
		{	
			int counter = 0;
			for(int i = 0; i < enemies.Count; i++)
			{
				if(enemies[i].activeSelf){
					counter++;
				}
			}
			return counter;
		}
	}
	[Header("Monster Respawn Management")]
	public List<MonsterSelector> monster = new List<MonsterSelector>();
	public List<Transform> respwanPoint = new List<Transform>();
	[HideInInspector]
	public List<GameObject> enemies = new List<GameObject>();

	public void AddEnemy(GameObject enemy){
		if(!enemies.Contains(enemy) && !isSkipable)
		{
			enemies.Add(enemy);
		}
	}

	public void RemoveEnemy(GameObject enemy)
	{
		if(!isSkipable)
		{
			enemies.Remove(enemy);
		}
	}

	

	[System.Serializable]
	public class MonsterSelector
	{
		public Tier tier;
		public int size;
	}
}
