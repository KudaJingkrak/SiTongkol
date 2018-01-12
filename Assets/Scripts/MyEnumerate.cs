using System.Collections;
using System.Collections.Generic;
public enum Direction{
	Front, Right, Back, Left
}
public enum AmuletType{
	Normal, Fire, Water, Earth, Air //blablablabal.....
}
public enum DamageType{
	Normal, Magic
}
public enum DamageEffect{
	None, Fuzzy, Poison, Confused, Sleep //blablabla....
}
public enum ItemEffect{
	None, Heal_HP, Heal_MP //blablablabla
}
public enum ItemName{
	// Amulet
	Amulet_Normal, Amulet_Fire, Amulet_Water, Amulet_Earth, Amulet_Air, 
	
	//Equipment
	Branch,
	
	//Consumable
	Trash, Banana, HP_Potion // bbalablablaba.....
}
public enum ItemType{
	Amulet, Equipment, Consumable
}

public enum MoveAble_State{
    Idle, MoveX, MoveY
}

public enum ButtonDoor_Type
{
    Door, Bridge, Switch
}
