using System.Collections;
using System.Collections.Generic;
public enum Direction{
	Front, Right, Back, Left
}
public enum AmuletType{
	Normal, Fire, Water, Earth, Air //blablablabal.....
}
public enum DamageType{
	Normal, Magic, Critical
}
public enum DamageEffect{
	None, Fuzzy, Poison, Confused, Sleep //blablabla....
}
public enum ItemName{
	None,
	// Amulet
	Amulet_Normal, Amulet_Fire, Amulet_Water, Amulet_Earth, Amulet_Air, 
	
	//Equipment
	Branch, Sword,
	
	//Consumable
	Trash, Banana, HP_Potion // bbalablablaba.....
}
public enum ItemEffect{
	None, Heal_HP, Heal_MP //blablablabla
}
public enum ItemType{
	Amulet, Equipment, Consumable, QuestItem
}

public enum MoveAble_State{
    Idle, MoveX, MoveY
}

public enum ButtonDoor_Type
{
    Door, Bridge, Switch
}

public enum MovementType{
	Walk, Swim, Fly
}

public enum JenisPuzzle
{
    Switching, Destroy, Combination
}

public enum Tier
{
	TierOne, TierTwo, TierThree,  
}