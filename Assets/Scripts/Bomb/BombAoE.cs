﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAoE : MonoBehaviour {
    public int Damage;
    public BombSystem sysBomb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndExplosion()
    {
        sysBomb.End_Explosion();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //does it right?
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        if (attackable != null)
        {
            attackable.ApplyDamage(Damage);
        }
    }
}
