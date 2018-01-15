using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Inventory : MonoBehaviour {
	public static Inventory instance;
	public List<AmuletPointer> amulet = new List<AmuletPointer>();
	public List<EquipmentPointer> equipment = new List<EquipmentPointer>();
	public List<ConsumablePointer> consumable = new List<ConsumablePointer>();
	public List<QuestItemPointer> questItem = new List<QuestItemPointer>();

	void Awake()
	{
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

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
	public List<AmuletPointer> amulet;
	public List<EquipmentPointer> equipment;
	public List<ConsumablePointer> consumable;
	public List<QuestItemPointer> questItem;
}

[System.Serializable]
public class AmuletPointer{
	public ItemName label;
}
[System.Serializable]
public class EquipmentPointer{
	public ItemName label;
}
[System.Serializable]
public class ConsumablePointer{
	public ItemName label;
	public int amount;
}
[System.Serializable]
public class QuestItemPointer{
	public ItemName label;
	public int amount;
}
