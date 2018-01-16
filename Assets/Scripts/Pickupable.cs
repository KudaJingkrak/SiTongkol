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
	public ItemName item;
	public int amount;

	public static Pickup PickupItem(Pickup item, GameObject instigator){
		if(instigator.CompareTag("Player")){
			switch(item.type){
				case ItemType.Amulet:
					Amulet amulet = ItemDictionary.Instance.GetAmulet(item.item);
					if(amulet!=null){
						
						AmuletPointer amuletPointer = new AmuletPointer();
						amuletPointer.label = item.item;
						int index = -1;
						for(int i = 0; i < Inventory.Instance.amulet.Length; i++){
							
							if(Inventory.Instance.amulet[i].label == ItemName.None && index == -1){
								index = i;
							}

							if(Inventory.Instance.amulet[i].label != ItemName.None && Inventory.Instance.amulet[i].label == item.item){
								// TODO implement UI notification gagal masuk
								

								Debug.Log("Item Amulet "+amulet.label+" gagal masuk ke inventory");
								return item;
							}
						}
						item.amount = 0;
						amuletPointer.index = index;
						Inventory.Instance.amulet[index] = amuletPointer;

						// TODO implement UI notification

						Debug.Log("Item Amulet "+amulet.label+" sukses masuk ke inventory");

					}
					break;
				case ItemType.Equipment:
					Equipment equipment = ItemDictionary.Instance.GetEquipment(item.item);
					if(equipment!=null){
						Debug.Log("Item Equipment "+equipment.label+" masuk ke inventory");
						EquipmentPointer equipmentPointer = new EquipmentPointer();
						equipmentPointer.label = item.item;
						int index = -1;
						for(int i = 0; i < Inventory.Instance.equipment.Length; i++){
							
							if(Inventory.Instance.equipment[i].label == ItemName.None && index == -1){
								index = i;
							}

							if(Inventory.Instance.equipment[i].label != ItemName.None && Inventory.Instance.equipment[i].label == item.item){
								// TODO implement UI notification gagal masuk
								

								Debug.Log("Item Equipment "+equipment.label+" gagal masuk ke inventory");
								return item;
							}
						}

						item.amount = 0;
						equipmentPointer.index = index;
						Inventory.Instance.equipment[index] = equipmentPointer;

						// TODO implement UI notification
				
					}
					break;
				case ItemType.Consumable:
					Consumable consumable = ItemDictionary.Instance.GetConsumable(item.item);
					if(consumable!=null){
						
						ConsumablePointer consumablePointer = new ConsumablePointer();
						consumablePointer.label = item.item;
						consumablePointer.amount = item.amount;
						int index = -1, amount = 0;
						for(int i=0; i < Inventory.Instance.consumable.Length;i++){
							if(Inventory.Instance.consumable[i].label == ItemName.None && index == -1){
								index = i;
							}

							if(Inventory.Instance.consumable[i].label != ItemName.None &&Inventory.Instance.consumable[i].label==item.item){
								while(consumablePointer.amount > 0 && Inventory.Instance.consumable[i].amount < 99){
									Inventory.Instance.consumable[i].amount += 1;
									consumablePointer.amount -= 1;
									amount++;
								}
							}
						}

						item.amount = consumablePointer.amount;

						if(consumablePointer.amount > 0){
							if(index == -1){
								Debug.Log("Menyisahkan "+consumable.label+" sebanyak "+consumablePointer.amount);

								//TODO implement UI notification kepenuhan
								return item;
							}else{
								int _amount = consumablePointer.amount;
								while(_amount > 99 && index < Inventory.Instance.consumable.Length){
									ConsumablePointer _consumable = new ConsumablePointer();
									consumablePointer.amount -= 99;
									_consumable.amount = 99;
									_consumable.label = item.item;
									_consumable.index = index;
									Inventory.Instance.consumable[index] = _consumable;
									index++;
									item.amount -= 99;
									amount += 99;
									_amount = consumablePointer.amount;
								}

								if(index < Inventory.Instance.consumable.Length){
									consumablePointer.index = index;
									Inventory.Instance.consumable[index] = consumablePointer;
									amount += consumablePointer.amount;
									item.amount = 0;
								}
								
							}
						}
					
						Debug.Log(amount+" Item consumable "+consumable.label+" masuk ke inventory");

						// TODO implement UI notification

				
					}
					break;
				case ItemType.QuestItem:
					QuestItem questItem = ItemDictionary.Instance.GetQuestItem(item.item);
					if(questItem!=null){
						
						QuestItemPointer questItemPointer = new QuestItemPointer();
						questItemPointer.label = item.item;
						questItemPointer.amount = item.amount;

						int index = -1, amount = 0;
						for(int i=0; i < Inventory.Instance.questItem.Length;i++){
							if(Inventory.Instance.questItem[i].label == ItemName.None && index == -1){
								index = i;
							}

							if(Inventory.Instance.questItem[i].label != ItemName.None && Inventory.Instance.questItem[i].label==item.item){
								while(questItemPointer.amount > 0 && Inventory.Instance.questItem[i].amount < 99){
									Inventory.Instance.questItem[i].amount += 1;
									questItemPointer.amount -= 1;
									amount++;
								}
							}
						}

						item.amount = questItemPointer.amount;

						if(questItemPointer.amount > 0){
							if(index == -1){
								Debug.Log("Menyisahkan "+questItem.label+" sebanyak "+questItemPointer.amount);

								//TODO implement UI notification kepenuhan
								return item;
							}else{
								int _amount = questItemPointer.amount;
								while(_amount > 99 && index < Inventory.Instance.questItem.Length){
									QuestItemPointer _questItem = new QuestItemPointer();
									questItemPointer.amount -= 99;
									_questItem.amount = 99;
									_questItem.label = item.item;
									_questItem.index = index;
									Inventory.Instance.questItem[index] = _questItem;
									index++;
									item.amount -= 99;
									amount += 99;
									_amount = questItemPointer.amount;
								}

								if(index < Inventory.Instance.questItem.Length){
									questItemPointer.index = index;
									Inventory.Instance.questItem[index] = questItemPointer;
									amount += questItemPointer.amount;
									item.amount = 0;
								}
							}
						}

						Debug.Log(amount+" Item quest "+questItem.label+" masuk ke inventory");
						// TODO implement UI notification

					
					}
					break;
			}

			return item;
		}


		return null;
	}
}