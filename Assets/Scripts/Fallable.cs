using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallable : MonoBehaviour {
	public string fallableLayer = "Dodge";
	public List<Collider2D> colCaps = new List<Collider2D>();
	private CompositeCollider2D zone = null;
	private int zoneLayer = 0;
	private Vector2 lessVector = new Vector2(0.05f, 0.05f);
	
	void Awake()
	{
		zone = GetComponent<CompositeCollider2D>();
		zoneLayer = gameObject.layer;
	}

	// void OnTriggerEnter2D(Collider2D col)
	// {
	// 	if(col.gameObject.layer == LayerMask.NameToLayer(fallableLayer))
	// 	{
	// 		CapsuleCollider2D colCap = (CapsuleCollider2D)col;
	// 		if(colCap)
	// 		{
	// 			colCaps.Add(colCap);
	// 		}

	// 	}
	// }

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.layer == LayerMask.NameToLayer(fallableLayer))
		{
			CapsuleCollider2D colCap = (CapsuleCollider2D)col;
			if(colCap)
			{
				bool minInside = false, maxInside= false;
				
				// //Check bounds min
				RaycastHit2D[] minHits = Physics2D.LinecastAll((Vector2)colCap.bounds.min, (Vector2)colCap.bounds.min + lessVector);

				for(int i = 0; i < minHits.Length; i++)
				{
					if(minHits[i].collider.gameObject == this.gameObject)
					{
						minInside = true;
						break;
					}
				}

				//Check bounds max
				RaycastHit2D[] maxHits = Physics2D.LinecastAll((Vector2)colCap.bounds.max, (Vector2)colCap.bounds.max - lessVector);

				for(int i = 0; i < maxHits.Length; i++)
				{
					if(maxHits[i].collider.gameObject == this.gameObject)
					{
						maxInside = true;
						break;
					}
				}

				if(maxInside && minInside)
				{
					IAttackable attackable = colCap.GetComponentInParent<IAttackable>();
					if(attackable != null)
					{
						attackable.Die();
					}
				}
			}		
		}
	}

	// void OnTriggerExit2D(Collider2D col)
	// {
	// 	if(col.gameObject.layer == LayerMask.NameToLayer(fallableLayer))
	// 	{
	// 		CapsuleCollider2D colCap = (CapsuleCollider2D)col;
	// 		if(colCap)
	// 		{
	// 			colCaps.Remove(colCap);
	// 		}

	// 	}
	// }
}
