using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {
	public Requirement[] requirement;
	public GameObject targetNPC;
	public bool isCompleted;

	public bool CheckReq(){

		for (int i = 0; i < requirement.Length; i++)
		{	
			switch(requirement[i].type){
				case ItemType.Amulet:
						
					break;
				case ItemType.Consumable:

					break;
				case ItemType.Equipment:

					break;
				case ItemType.QuestItem:

					break;
			}
		}

		return true;
	}

	
}
[System.Serializable]
public class Requirement{
	public ItemName label;
	public ItemType type;
	public int amount;
}
