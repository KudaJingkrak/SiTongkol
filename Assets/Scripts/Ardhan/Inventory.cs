using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}

	public List<Consumable> consum_Inventory = new List<Consumable>();
	public List<Equipment> equip_Inventory = new List<Equipment>();
	public List<Amulet> amulet_Inventory = new List<Amulet>();
	public List<QuestItem> quest_Inventory = new List<QuestItem>();

	public void AddConsumeable(Consumable Item)
	{
		consum_Inventory.Add(Item);
	}

	public void RemoveConsumeable(Consumable Item)
	{
		consum_Inventory.Remove(Item);
	}

	public void AddEquipment(Equipment Item)
	{
		equip_Inventory.Add(Item);
	}

	public void RemoveEquipment(Equipment Item)
	{
		equip_Inventory.Remove(Item);
	}
	
	public void AddAmulet(Amulet Item)
	{
		amulet_Inventory.Add(Item);
	}

	public void RemoveAmulet(Amulet Item)
	{
		amulet_Inventory.Remove(Item);
	}

	public void AddItemQuest(QuestItem Item)
	{
		quest_Inventory.Add(Item);
	}

	public void RemoveItemQuest(QuestItem Item)
	{
		quest_Inventory.Remove(Item);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
