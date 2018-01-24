using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task  {
	public string description;
	[Header("Requirement")]
	public ItemPointer[] items;
	public MosnterRequire[] monsters;
	[Header("Target trigger")]
	public GameObject targetNPC;
	[Header("Reward")]
	public ItemPointer[] rewards;
	[Header("Status")]
	public bool isCompleted;
	[HideInInspector] public bool requireComplete = false;

	public void SetCompleted(bool _isCompleted){
		isCompleted = _isCompleted;
	}

	public bool GetCompleted(){
		return isCompleted;
	}

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
	/**
	 * Remove Items requirement from inventory when is completed
	 */
	public void FetchRequirement(){
		if(!isCompleted) return;

		for (int i = 0; i < items.Length; i++)
		{
			switch(items[i].type){
				case ItemType.Amulet:
					AmuletPointer amuletPointer = new AmuletPointer();
					amuletPointer.label = items[i].label;
					Inventory.Instance.RemoveAmulet(amuletPointer);
					break;
				case ItemType.Consumable:
					ConsumablePointer consumablePointer = new ConsumablePointer();
					consumablePointer.label = items[i].label;
					consumablePointer.amount = items[i].amount;
					Inventory.Instance.RemoveConsumable(consumablePointer);
					break;
				case ItemType.Equipment:
					EquipmentPointer equipmentPointer = new EquipmentPointer();
					equipmentPointer.label = items[i].label;
					Inventory.Instance.RemoveEquipment(equipmentPointer);
					break;
				case ItemType.QuestItem:
					QuestItemPointer questItemPointer = new QuestItemPointer();
					questItemPointer.label = items[i].label;
					questItemPointer.amount = items[i].amount;
					Inventory.Instance.RemoveQuestItem(questItemPointer);
					break;
			}
		}
	}
	/**
	 * Add reward to Invetory whes is completed
	 */
	public void GetRewards(){
		if(!isCompleted) return;

		for (int i = 0; i < rewards.Length; i++)
		{
			switch(rewards[i].type){
				case ItemType.Amulet:
					AmuletPointer amuletPointer = new AmuletPointer();
					amuletPointer.label = rewards[i].label;
					rewards[i].amount = Inventory.Instance.AddAmulet(amuletPointer);
					break;
				case ItemType.Consumable:
					ConsumablePointer consumablePointer = new ConsumablePointer();
					consumablePointer.label = rewards[i].label;
					consumablePointer.amount = rewards[i].amount;
					rewards[i].amount = Inventory.Instance.AddConsumable(consumablePointer);
					break;
				case ItemType.Equipment:
					EquipmentPointer equipmentPointer = new EquipmentPointer();
					equipmentPointer.label = rewards[i].label;
					rewards[i].amount = Inventory.Instance.AddEquipment(equipmentPointer);
					break;
				case ItemType.QuestItem:
					QuestItemPointer questItemPointer = new QuestItemPointer();
					questItemPointer.label = rewards[i].label;
					questItemPointer.amount = rewards[i].amount;
					rewards[i].amount = Inventory.Instance.AddQuestItem(questItemPointer);
					break;
			}
		}
	}
	/**
	 * Call this function for completed task
	 * If is completed, call FecthRequirement  and GetReward function
	 */
	public bool CompletedTask(){
		if(!isCompleted) return false;
		FetchRequirement();
		GetRewards();
		return true;
	}
	
}
[System.Serializable]
public class ItemPointer{
	public ItemType type;
	public ItemName label;
	[Range(0,99)] public int amount = 1;
}
[System.Serializable]
public class MosnterRequire{
	public MonsterName label;
	[Range(0,99)] public int amount;
	[HideInInspector] public int counter;
}
