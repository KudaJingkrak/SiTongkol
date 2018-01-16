using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Inventory : MonoBehaviour {
	public static Inventory Instance;
	public AmuletPointer[] amulet = new AmuletPointer[6];
	public EquipmentPointer[] equipment = new EquipmentPointer[8];
	public ConsumablePointer[] consumable = new ConsumablePointer[32];
	public QuestItemPointer[] questItem = new QuestItemPointer[64];

	void Awake()
	{
		if(Instance == null){
			Instance = this;
		}else if(Instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		for(int i = 0 ; i < amulet.Length; i++){
			amulet[i].index = i;
		}

		for(int i = 0 ; i < equipment.Length; i++){
			equipment[i].index = i;
		}

		for(int i = 0 ; i < consumable.Length; i++){
			consumable[i].index = i;
		}

		for(int i = 0 ; i < questItem.Length; i++){
			questItem[i].index = i;
		}

		//TODO assign ke game manager

	}
	
	public void SaveInventory(string slotName){
		string savePath = Application.persistentDataPath+"/Item"+slotName+".dat";
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);

		InventoryData data =  new InventoryData();
		data.amulet = amulet;
		data.equipment = equipment;
		data.consumable = consumable;
		data.questItem = questItem;

		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadInventory(string slotName){
		string savePath = Application.persistentDataPath+"/Item"+slotName+".dat";
		if(File.Exists(savePath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(savePath, FileMode.Open);

			InventoryData data = (InventoryData) bf.Deserialize(file);
			file.Close();

			amulet = data.amulet;
			equipment = data.equipment;
			consumable = data.consumable;
			questItem = data.questItem;
		}
	}

}
[System.Serializable]
public class InventoryData{
	public AmuletPointer[] amulet;
	public EquipmentPointer[] equipment;
	public ConsumablePointer[] consumable;
	public QuestItemPointer[] questItem;
}

[System.Serializable]
public class AmuletPointer{
	public ItemName label;
	public int index;
}
[System.Serializable]
public class EquipmentPointer{
	public ItemName label;
	public int index;
}
[System.Serializable]
public class ConsumablePointer{
	public ItemName label;
	public int amount;
	public int index;
}
[System.Serializable]
public class QuestItemPointer{
	public ItemName label;
	public int amount;
	public int index;
}
