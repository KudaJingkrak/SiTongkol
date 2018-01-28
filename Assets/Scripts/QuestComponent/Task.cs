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
	public GameObject target;
	[Header("Reward")]
	public ItemPointer[] rewards;

	public bool CheckRequirement(GameObject _target, MosnterRequire[] _monsters){

		if(target != null && !target.Equals(_target)){
			return false;
		}

		bool itemComplete = true, monsterComplete = true;
		
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
			if(_monsters[i].amount < monsters[i].amount){
				monsterComplete = false;
				break;
			}
		}

		Debug.Log("Item complete is"+itemComplete+" & Monster complete is " +monsterComplete);
	
		return itemComplete && monsterComplete;
	}

	#region Completed Task

	/**
	 * Remove Items requirement from inventory when is completed
	 */
	private void FetchRequirement(){

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
	private void GetRewards(){

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
		FetchRequirement();
		GetRewards();
		return true;
	}

	#endregion
	
	
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
}
