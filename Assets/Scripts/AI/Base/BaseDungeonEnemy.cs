using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDungeonEnemy : BaseEnemy {

	public int currentRoom{
		get{
			if(DungeonManager.Instance)
			{
				return DungeonManager.Instance.pc2dr.ComputeCurrentRoom(transform.position);
			
			}else
			{
				return -1;
			
			}	

		}
	}
	
	public void ReportLiveToRoom(){
		if(DungeonManager.Instance)
		{
			DungeonManager.Instance.rooms[currentRoom].AddEnemy(gameObject);
			Debug.Log("Report Live");
		}
	}

	public void ReportDieToRoom()
	{
		if(DungeonManager.Instance)
		{
			DungeonManager.Instance.rooms[currentRoom].RemoveEnemy(gameObject);
		}
	}
}
