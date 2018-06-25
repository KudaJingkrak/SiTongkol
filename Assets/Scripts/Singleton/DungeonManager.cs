using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

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
			rooms = new DungeonRoom[pc2dr.Rooms.Count];

			for(int i = 0; i < rooms.Length; i++)
			{
				rooms[i] = new DungeonRoom();
				rooms[i].position = pc2dr.Rooms[i].Dimensions.position;

			}
		}

	}

	void Start () 
	{
		
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
				}else{
					rooms[i].isPlayerHere = false;
				}

				Debug.Log("Room " + i + " terdapat " + rooms[i].numberOfEnemies + " musuh");
				
			}
		}
	}

}
[System.Serializable]
public class DungeonRoom{
	public Vector2 position = Vector2.zero;
	public bool isExplored = false;
	public bool isPlayerHere = false;
	[Header("Enemies Handle")]
	public bool isRespawnable = true;
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

	public List<GameObject> enemies = new List<GameObject>();

	public void AddEnemy(GameObject enemy){
		if(!enemies.Contains(enemy))
		{
			enemies.Add(enemy);
		}
	}
}
