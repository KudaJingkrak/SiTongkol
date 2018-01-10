using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTreasure : MonoBehaviour, IInteractable{
	public SpriteRenderer render;
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
			if(!isOpened){
				Debug.Log("Player mendapatkan item");
				isOpened = true;
				render.sprite = openedSprite;
			}

			// TODO harusnya muncul notifikasi setelah ity di false-in ketika selesai notifikasi
			instigator.GetComponent<GayatriCharacter>().isInteracting = false;
		}
		
    }
}
