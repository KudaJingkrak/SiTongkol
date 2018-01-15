using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour {
	public List<AmuletDictionary> amulets;
	public List<EqupmentDictionary> equipments;
	public List<ConsumableDictionary> consumables;
	public List<QuestItemDictionary> questItems;

	public Dictionary<ItemName, Amulet> dictAmulet;
	public Dictionary<ItemName, Equipment> dictEquipment;
	public Dictionary<ItemName, Consumable> dictConsumable;
	public Dictionary<ItemName, QuestItem> dictQuestItem;


	private static ItemDictionary instance = null;

	public static ItemDictionary Instance{
		get{return instance;}
	}
	
	void Awake()
	{
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		if(dictAmulet == null){
			dictAmulet =  new Dictionary<ItemName, Amulet>();
			//Debug.Log("Amulet Null");
			foreach(AmuletDictionary dict in amulets){
				dictAmulet.Add(dict.key, dict.amulet);
			}
			amulets.Clear();
		
			//Debug.Log("Amulet punya amulet air "+(dictAmulet.ContainsKey(ItemName.Amulet_Air)));
		}	

		if(dictEquipment == null){
			//Debug.Log("Equipment Null");
			dictEquipment =  new Dictionary<ItemName, Equipment>();
			foreach(EqupmentDictionary dict in equipments){
				dictEquipment.Add(dict.key, dict.equipment);
			}
			equipments.Clear();
		}	

		if(dictConsumable == null){
			//Debug.Log("Consumable Null");
			dictConsumable =  new Dictionary<ItemName, Consumable>();
			foreach(ConsumableDictionary dict in consumables){
				dictConsumable.Add(dict.key, dict.consumable);
			}
			consumables.Clear();
		}

		if(dictQuestItem == null){
			dictQuestItem = new Dictionary<ItemName, QuestItem>();
			foreach(QuestItemDictionary dict in questItems){
				dictQuestItem.Add(dict.key, dict.questItem);
			}
			questItems.Clear();
		}	
	}

	public Amulet GetAmulet(ItemName key){
		if(dictAmulet.ContainsKey(key)){
			return dictAmulet[key];
		}else{
			return null;
		}
	}

	public Equipment GetEquipment(ItemName key){
		if(dictEquipment.ContainsKey(key)){
			return dictEquipment[key];
		}else{
			return null;
		}
	}

	public Consumable GetConsumable(ItemName key){
		if(dictConsumable.ContainsKey(key)){
			return dictConsumable[key];
		}else{
			return null;
		}
	}

	public QuestItem GetQuestItem(ItemName key){
		if(dictQuestItem.ContainsKey(key)){
			return dictQuestItem[key];
		}else{
			return null;
		}
	}




}
[System.Serializable]
public class AmuletDictionary 
{
	public ItemName key;
	public Amulet amulet;
	private byte a;
}
[System.Serializable]
public class EqupmentDictionary 
{
	public ItemName key;
	public Equipment equipment;
	private byte a;
}
[System.Serializable]
public class ConsumableDictionary 
{
	public ItemName key;
	public Consumable consumable;
	private byte a;
}
[System.Serializable]
public class QuestItemDictionary{
	public ItemName key;
	public QuestItem questItem;
	private byte a;
}

