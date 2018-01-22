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
	
	public int AddAmulet(AmuletPointer _amulet, int index = -1, bool check = false){
		if(check || index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(amulet[i].label == _amulet.label) return 1;
			}
		}

		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(amulet[i].label == ItemName.None){
					amulet[index] = _amulet;	
					return 0;
				}
			}
		}

		if(index > -1){
			if(amulet[index].label == ItemName.None){
				amulet[index] = _amulet;
				return 0;
			}
		}

		return 1;
	}

	public bool RemoveAmulet(AmuletPointer _amulet, int index=-1){
		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(amulet[i].label == _amulet.label){
					amulet[i] = new AmuletPointer();
					return true;
				}
			}
		}

		if(index > -1){
			if(amulet[index].label == _amulet.label){
				amulet[index] = new AmuletPointer();
				return true;
			}
		}
		return false;
	}

	public int AddEquipment(EquipmentPointer _equipment, int index = -1, bool check = false){
		if(check || index == -1){
			for(int i = 0; i < equipment.Length; i++){
				if(equipment[i].label == _equipment.label) return 1;
			}
		}

		if(index == -1){
			for(int i = 0; i < equipment.Length; i++){
				if(equipment[i].label == ItemName.None){
					equipment[index] = _equipment;		
					return 0;
				}
			}
		}

		if(index > -1){
			if(equipment[index].label == ItemName.None){
				equipment[index] = _equipment;
				return 0;
			}
		}

		return 1;
	}

	public bool RemoveEquipment(EquipmentPointer _equipment, int index=-1){
		if(index == -1){
			for(int i = 0; i < equipment.Length; i++){
				if(equipment[i].label == _equipment.label){
					equipment[i].label = ItemName.None; 
					return true;
				}
			}
		}

		if(index > -1){
			if(equipment[index].label == _equipment.label){
				equipment[index].label = ItemName.None;
				return true;
			}
		}
		return false;
	}
	/**
	 * Amount pada pointer adalah jumlah item yang akan ditambahkan
	 */
	public int AddConsumable(ConsumablePointer _consumable, int index=-1, bool check=false){
		int _amount = _consumable.amount;
		if(check || index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(consumable[i].label == _consumable.label && consumable[i].amount < 100){
					_amount = consumable[i].amount + _amount;
					if(_amount < 100){
						consumable[i].amount = _amount;
						return 0;
					}else{
						consumable[i].amount = 99;
						return _amount - 99;
					}
				}
			}
		}

		_amount = _consumable.amount;
		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(consumable[i].label == ItemName.None){
					_amount = consumable[i].amount + _amount;
					consumable[i].label = _consumable.label;
					if(_amount < 100){
						consumable[i].amount = _amount;
						return 0;
					}else{
						consumable[i].amount = 99;
						return _amount - 99;
					}
				}
			}
		}
		_amount= _consumable.amount;
		if(index > -1){
			if(consumable[index].label == _consumable.label && consumable[index].amount < 100){
				_amount = consumable[index].amount + _amount;
				if(_amount < 100){
					consumable[index].amount = _amount;
					return 0;
				}else{
					consumable[index].amount = 99;
					return _amount - 99;
				}
			}else if(consumable[index].label == ItemName.None){
				_amount = consumable[index].amount + _amount;
				consumable[index].label = _consumable.label;
				if(_amount < 100){
					consumable[index].amount = _amount;
					return 0;
				}else{
					consumable[index].amount = 99;
					return _amount - 99;
				}
			}
		}

		return _amount;
	}

	/**
	 * Amount dalam pointer adalah jumlah yang akan dikurangkan dari inventory
	 */
	public bool RemoveConsumable(ConsumablePointer _consumable, int index=-1){
		int _amount = _consumable.amount;
		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(consumable[i].label == _consumable.label){
					_amount = consumable[i].amount - _amount;
					if(_amount < 0){
						return false;
					}else{
						consumable[i].amount = _amount;
						if(_amount == 0){
							consumable[i].label = ItemName.None;
						}
						return true;
					}
				}
			}
		}

		_amount = _consumable.amount;
		if(index > -1){
			if(consumable[index].label == _consumable.label){
				_amount = consumable[index].amount - _amount;
				if(_amount < 0){
					return false;
				}else{
					consumable[index].amount = _amount;
					if(_amount == 0){
						consumable[index].label = ItemName.None;
					}
					return true;
				}
			}
		}

		return false;
	}
	/**
	 * Amount pada pointer adalah jumlah item yang akan ditambahkan
	 */
	public int AddQuestItem(QuestItemPointer _questItem,int index=-1, bool check=false){
		int _amount = _questItem.amount;
		if(check || index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(questItem[i].label == _questItem.label && questItem[i].amount < 100){
					_amount = questItem[i].amount + _amount;
					if(_amount < 100){
						questItem[i].amount = _amount;
						return 0;
					}else{
						questItem[i].amount = 99;
						return _amount - 99;
					}
				}
			}
		}

		_amount = _questItem.amount;
		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(questItem[i].label == ItemName.None){
					_amount = questItem[i].amount + _amount;
					if(_amount < 100){
						questItem[i].amount = _amount;
						return 0;
					}else{
						questItem[i].amount = 99;
						return _amount - 99;
					}
				}
			}
		}
		_amount= _questItem.amount;
		if(index > -1){
			if(questItem[index].label == _questItem.label && questItem[index].amount < 100){
				_amount = questItem[index].amount + _amount;
				if(_amount < 100){
					questItem[index].amount = _amount;
					return 0;
				}else{
					questItem[index].amount = 99;
					return _amount - 99;
				}
			}else if(questItem[index].label == ItemName.None){
				_amount = questItem[index].amount + _amount;
				if(_amount < 100){
					questItem[index].amount = _amount;
					return 0;
				}else{
					questItem[index].amount = 99;
					return _amount - 99;
				}
			}
		}

		return _amount;
	}
	/**
	 * Amount dalam pointer adalah jumlah yang akan dikurangkan dari inventory
	 */
	public bool RemoveQuestItem(QuestItemPointer _questItem, int index=-1){
		int _amount = _questItem.amount;
		if(index == -1){
			for(int i = 0; i < amulet.Length; i++){
				if(questItem[i].label == _questItem.label){
					_amount = questItem[i].amount - _amount;
					if(_amount < 0){
						return false;
					}else{
						questItem[i].amount = _amount;
						if(_amount == 0){
							questItem[i].label = ItemName.None;
						}
						return true;
					}
				}
			}
		}

		_amount = _questItem.amount;
		if(index > -1){
			if(questItem[index].label == _questItem.label){
				_amount = questItem[index].amount - _amount;
				if(_amount < 0){
					return false;
				}else{
					questItem[index].amount = _amount;
					if(_amount == 0){
						questItem[index].label = ItemName.None;
					}
					return true;
				}
			}
		}

		return false;
	}

	public int GetItemAmount(ItemType type, ItemName label){

		switch(type){
			case ItemType.Amulet:
				for(int i=0; i < amulet.Length; i++){
					if(amulet[i].label == label){
						return 1;
					}
				}
				break;
			case ItemType.Consumable:
				for (int i = 0; i < consumable.Length; i++)
				{
					if(consumable[i].label == label){
						return consumable[i].amount;
					}
				}
				break;
			case ItemType.Equipment:
				for (int i = 0; i < equipment.Length; i++)
				{
					if(equipment[i].label == label){
						return 1;
					}
				}
				break;
			case ItemType.QuestItem:
				for (int i = 0; i < questItem.Length; i++)
				{
					if(questItem[i].label == label){
						return questItem[i].amount;
					}
				}
				break;
		}

		return -1;
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
