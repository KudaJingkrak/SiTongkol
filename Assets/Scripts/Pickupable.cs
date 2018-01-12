using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickupable:MonoBehaviour{
	public Pickup item;

	public void PickupItem(GameObject instigator){
		if(instigator.CompareTag("Player")){
			switch(item.type){
				case ItemType.Amulet:
					Amulet amulet = ItemDictionary.Instance.GetAmulet(item.item);
					if(amulet!=null){
						Debug.Log("Item Amulet "+amulet.label+" masuk ke inventory");



					}
					break;
				case ItemType.Equipment:
					Equipment equipment = ItemDictionary.Instance.GetEquipment(item.item);
					if(equipment!=null){
						Debug.Log("Item Equipment "+equipment.label+" masuk ke inventory");



					}
					break;
				case ItemType.Consumable:
					Consumable consumable = ItemDictionary.Instance.GetConsumable(item.item);
					if(consumable!=null){
						Debug.Log("Item Consumable "+consumable.label+" masuk ke inventory");



					}
					break;
			}
		}
	}

}

[System.Serializable]
public class Pickup{
	public ItemType type;
	public ItemName item;
	public int amount;
}