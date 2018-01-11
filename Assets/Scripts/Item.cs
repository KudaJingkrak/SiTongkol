using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
/**
	Item must be paired with enum item name
 */
[System.Serializable]
public class Item{
	/**
		Sprite of item
	 */
	public Sprite sprite;
	/**
		Label name of item
	 */
	public string label;
	/**
		Description of item
	 */
	[TextArea(5,15)]
	public string description;
}

[System.Serializable]
public class Consumable: Item{
	public ItemEffect effect;
	public float amount;
} 

[System.Serializable]
public class Equipment: Item{
	public float durability;
}

[System.Serializable]
public class Amulet: Item{
	public AmuletType type;
	public float manaUsage;
}

























// [CustomPropertyDrawer(typeof(Sprite))]
// public class SpriteDrawer : PropertyDrawer {
 
//     private static GUIStyle s_TempStyle = new GUIStyle();
 
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {                                    
//         var ident = EditorGUI.indentLevel;
//         //EditorGUI.indentLevel = 0;
		
//         Rect spriteRect;
		
     
//         //create object field for the sprite
//         //spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
//         spriteRect =  new Rect(position.x, position.y, position.width * 0.75f, EditorGUIUtility.singleLineHeight);
// 		property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Sprite), false);
 
//         //if this is not a repain or the property is null exit now
//         if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
//             return;
 
//         //draw a sprite
//         Sprite sp = property.objectReferenceValue as Sprite;
 
//         spriteRect.y = position.y;
// 		spriteRect.x = position.width * 0.8f;
//         spriteRect.width = 64f;
//         spriteRect.height = 64f;   
//         s_TempStyle.normal.background = sp.texture;
//         s_TempStyle.Draw(spriteRect,GUIContent.none, false, false, false, false); 
	
//         EditorGUI.indentLevel = ident;
//     }
 
//     public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//     {
//         return base.GetPropertyHeight(property, label) + 70f;
//     }
// }

