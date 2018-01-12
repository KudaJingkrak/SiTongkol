using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickupable:MonoBehaviour{
	public Pickup item;

	public void PickupItem(GameObject instigator){
		if(instigator.CompareTag("Player")){

		}
	}

}

[System.Serializable]
public class Pickup{
	public ItemName item;
	public int amount;
}