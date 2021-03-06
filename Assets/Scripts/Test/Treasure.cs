﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable{
	public SpriteRenderer render;
	public Pickup[] items;
	public bool isOpened = false;
	public Sprite openedSprite;
    // Use this for initialization
    void Start () {
		if(isOpened){
			render.sprite = openedSprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyInteract(GameObject instigator = null)
    {
        
		if(instigator != null && instigator.CompareTag("Player")){
			bool haveItem=false;
			for(int i =0;i < items.Length; i++){
				if(items[i].amount > 0){
					haveItem = true;
					break;
				}
			}


			if(haveItem){
				Debug.Log("Player mendapatkan item");
				if(!isOpened){
					isOpened = true;
					render.sprite = openedSprite;
				}
				
				for(int i =0;i < items.Length; i++){
					if(items[i].amount > 0){
						Pickup item = Pickup.PickupItem(items[i], instigator);
						items[i].amount = item.amount;
					}
				}
			}

			// TODO harusnya muncul notifikasi setelah ity di false-in ketika selesai notifikasi
			instigator.GetComponent<GayatriCharacter>().isInteracting = false;
		}
		
    }

}
