using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickupable:MonoBehaviour{
	public Pickup item;

	public void PickupItem(GameObject instigator, bool isTreasure=false){

		item = Pickup.PickupItem(item, instigator);

		if(item.amount < 1){
			gameObject.SetActive(false);
		}

	}

}

[System.Serializable]
public class Pickup{
	public ItemType type;
	public ItemName itemName;
	public int amount;

	public static Pickup PickupItem(Pickup item, GameObject instigator){
		
		if(instigator.CompareTag("Player")){
			switch(item.type){
				case ItemType.Amulet:
					Amulet amulet = ItemDictionary.Instance.GetAmulet(item.itemName);
					if(amulet!=null){
						
						AmuletPointer amuletPointer = new AmuletPointer();
						amuletPointer.label = item.itemName;
						int amount = Inventory.Instance.AddAmulet(amuletPointer);
						Debug.Log((1-amount)+" Item equipment "+amulet.label+" masuk ke inventory");
						item.amount = amount;

						// TODO implement UI notification
						
					}
					break;
				case ItemType.Equipment:
					Equipment equipment = ItemDictionary.Instance.GetEquipment(item.itemName);
					if(equipment!=null){
						
						EquipmentPointer equipmentPointer = new EquipmentPointer();
						equipmentPointer.label = item.itemName;
						int amount = Inventory.Instance.AddEquipment(equipmentPointer);
						Debug.Log((1-amount)+" Item equipment "+equipment.label+" masuk ke inventory");
						item.amount = amount;

						// TODO implement UI notification
				
					}
					break;
				case ItemType.Consumable:
					Consumable consumable = ItemDictionary.Instance.GetConsumable(item.itemName);
					if(consumable!=null){
						
						ConsumablePointer consumablePointer = new ConsumablePointer();
						consumablePointer.label = item.itemName;
						consumablePointer.amount = item.amount;
						int amount = Inventory.Instance.AddConsumable(consumablePointer);
						Debug.Log((item.amount - amount)+" Item consumable "+consumable.label+" masuk ke inventory");
						item.amount = amount;

						// TODO implement UI notification

					}
					break;
				case ItemType.QuestItem:
					QuestItem questItem = ItemDictionary.Instance.GetQuestItem(item.itemName);
					if(questItem!=null){
						
						QuestItemPointer questItemPointer = new QuestItemPointer();
						questItemPointer.label = item.itemName;
						questItemPointer.amount = item.amount;
						int amount = Inventory.Instance.AddQuestItem(questItemPointer);
						Debug.Log((item.amount - amount)+" Item quest "+questItem.label+" masuk ke inventory");
						item.amount = amount;
						
						// TODO implement UI notification

					}
					break;
			}
		}

		return item;
	}
}