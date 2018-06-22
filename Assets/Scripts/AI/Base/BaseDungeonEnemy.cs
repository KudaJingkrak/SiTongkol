using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDungeonEnemy : MonoBehaviour {

	public int currentRoom{get{return DungeonManager.Instance.pc2dr.ComputeCurrentRoom(transform.position);}}
	
	public void ReportLiveToRoom(){
		DungeonManager.Instance.rooms[currentRoom].AddEnemy(gameObject);
	}

	public void ReportDieToRoom(){

	}
}
