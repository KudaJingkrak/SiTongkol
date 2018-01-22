using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task  {
	public string description;
	[Header("Requirement")]
	public ItemRequire[] items;
	public MosnterRequire[] monsters;
	[Header("Target trigger")]
	public GameObject targetNPC;
	[Header("Status")]
	public bool isCompleted;
	[HideInInspector] public bool requireComplete = false;

	public bool CheckReq(){
		bool itemComplete = true, mosnterComplete = true;
		
		// Check inventory udah selesai belom
		for (int i = 0; i < items.Length; i++)
		{	
			if(Inventory.Instance.GetItemAmount(items[i].type, items[i].label) < items[i].amount){
				itemComplete = false;
				break;
			}
		}

		// Check jumlah mosnter yang dibunuh udah sama sama current
		for (int i = 0; i < monsters.Length; i++)
		{
			if(monsters[i].counter < monsters[i].amount){
				mosnterComplete = false;
				break;
			}
		}

		
		requireComplete = itemComplete && mosnterComplete;
		if(targetNPC == null){
			isCompleted = requireComplete;
		}
		return itemComplete && mosnterComplete;
	}

	public void ResetMosnterCounter(){
		for (int i = 0; i < monsters.Length; i++)
		{
			monsters[i].counter = 0;
		}
	}

	public bool ResetMosnterCounter(int index){
		if(index < 0 || index > monsters.Length - 1){
			return false;
		}

		monsters[index].counter = 0;
		return true;
	}

	public bool ResetMosnterCounter(MonsterName label){
		int index = -1;

		for (int i = 0; i < monsters.Length; i++)
		{
			if(monsters[i].label == label){
				index = i;
			}	
		}

		if(index == -1){
			return false;
		}

		monsters[index].counter = 0;
		return true;
	}
	/**
	 * Default add by 1
	 */
	public bool AddMonsterCounter(MonsterName label, int amount=1){
		for (int i = 0; i < monsters.Length; i++)
		{
			if(monsters[i].label == label){
				monsters[i].counter += amount;
				return true;
			}	
		}

		return false;
	}
	/**
	 * Default substact by 1
	 */
	public bool SubtractMonsterCounter(MonsterName label, int amount=1){
		for (int i = 0; i < monsters.Length; i++)
		{
			if(monsters[i].label == label){
				monsters[i].counter -= amount;
				return true;
			}	
		}

		return false;
	}
	
}
[System.Serializable]
public class ItemRequire{
	public ItemName label;
	public ItemType type;
	[Range(0,99)] public int amount;
}
[System.Serializable]
public class MosnterRequire{
	public MonsterName label;
	[Range(0,99)] public int amount;
	[HideInInspector] public int counter;
}
