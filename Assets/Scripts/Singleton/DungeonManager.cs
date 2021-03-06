﻿using System.Collections;
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
					CleanDumpEnemies(rooms[i]);
					if(!rooms[i].isRespawned && !rooms[i].isBossRoom)
					{
						RespawnAllEnemies(rooms[i]);
					}

					if(!rooms[i].isSkipable)
					{
						if(rooms[i].numberOfEnemies > 0)
						{
							rooms[i].CloseAllPintu();
						}else{
							rooms[i].OpenAllPintu();
						}
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

	public void CleanDumpEnemies(DungeonRoom room)
	{
		bool isClean = false;
		int i = 0;

		while(!isClean)
		{
			if( i < room.enemies.Count && !room.enemies[i].activeSelf){
				room.enemies.RemoveAt(i);
			}else{
				i++;
			}

			if(i >= room.enemies.Count)
			{
				isClean = true;
			}
		}
	}

	public void RespawnAllEnemies(DungeonRoom room)
	{
		room.isRespawned = true;
		if(!room.isRespawnable) return;

		int baseIndex  = 0;
		for(int i = 0; i < room.monster.Count; i++)
		{	
			for(int j = 0; j < room.monster[i].size; j++)
			{
				MonsterDungeon instance = null;
				switch(room.monster[i].tier)
				{
					case Tier.TierOne:
						instance = tierOne[Random.Range(0, tierOne.Count)];
						MonsterPoolManager.Instance.ReuseObject(Tier.TierOne, instance.monster, room.respwanPoint[baseIndex + j].position, room.respwanPoint[baseIndex + j].rotation);
						break;
					case Tier.TierTwo:
						instance = tierTwo[Random.Range(0, tierTwo.Count)];
						MonsterPoolManager.Instance.ReuseObject(Tier.TierTwo, instance.monster, room.respwanPoint[baseIndex + j].position, room.respwanPoint[baseIndex + j].rotation);
						break;
					case Tier.TierThree:
						instance = tierThree[Random.Range(0, tierThree.Count)];
						MonsterPoolManager.Instance.ReuseObject(Tier.TierThree, instance.monster, room.respwanPoint[baseIndex + j].position, room.respwanPoint[baseIndex + j].rotation);
						break;
				}

			}
			baseIndex += room.monster[i].size;

		}
	}

	public void SpawnMonster(Tier tier, DungeonRoom room, int index)
	{
		MonsterDungeon instance = null;
		switch(tier)
		{
			case Tier.TierOne:
				instance = tierOne[Random.Range(0, tierOne.Count)];
				MonsterPoolManager.Instance.ReuseObject(Tier.TierOne, instance.monster, room.respwanPoint[index].position, room.respwanPoint[index].rotation);
				break;
			case Tier.TierTwo:
				instance = tierTwo[Random.Range(0, tierTwo.Count)];
				MonsterPoolManager.Instance.ReuseObject(Tier.TierTwo, instance.monster, room.respwanPoint[index].position, room.respwanPoint[index].rotation);
				break;
			case Tier.TierThree:
				instance = tierThree[Random.Range(0, tierThree.Count)];
				MonsterPoolManager.Instance.ReuseObject(Tier.TierThree, instance.monster, room.respwanPoint[index].position, room.respwanPoint[index].rotation);
				break;
		}
	}

	public void DestroyAllEnemies(DungeonRoom room)
	{
		if(!room.isRespawnable) return;

		while(room.enemies.Count > 0)
		{
			
			IAttackable attakable = room.enemies[0].GetComponent<IAttackable>();
			if(attakable != null)
			{
				room.enemies.RemoveAt(0);
				attakable.Die();
			}
			else
			{
				attakable = room.enemies[0].GetComponentInChildren<IAttackable>();
				if(attakable != null)
				{
					room.enemies.RemoveAt(0);
					attakable.Die();
				}
			}

		}

		room.isRespawned = false;
	}

}
[System.Serializable]
public class MonsterDungeon
{
	public GameObject monster;
	public int size;
}
[System.Serializable]
public class DungeonRoom{
	public Vector2 position = Vector2.zero;
	public bool isExplored = false;
	public bool isPlayerHere = false;
    public bool isBossRoom = false;
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

	[Header("Gate Management")]
	public bool isOpened = false;
	public List<Pintu> pintu = new List<Pintu>();

	[Header("Monster Respawn Management")]
	public List<MonsterSelector> monster = new List<MonsterSelector>();
	public List<Transform> respwanPoint = new List<Transform>();
	// [HideInInspector]
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

	public void OpenAllPintu()
	{
		for(int i=0; i < pintu.Count; i++)
		{
			pintu[i].PintuKebuka();
		}
		isOpened = true;
	}

	public void CloseAllPintu()
	{
		for(int i=0; i < pintu.Count; i++)
		{
			pintu[i].PintuKetutup();
		}
		isOpened = false;
	}


	[System.Serializable]
	public class MonsterSelector
	{
		public Tier tier;
		public int size;
	}
}
