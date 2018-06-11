using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAoE : MonoBehaviour {
    public int Damage;
    public BombSystem sysBomb;
    public Animator anim;
	
    void Awake(){
		anim = GetComponent<Animator>();
        
	}
	
    public void EndExplosion()
    {
        sysBomb.End_Explosion();
    }

    public void SetStart(bool isStart){
		if(!anim){
			anim = GetComponent<Animator>();
		}

		anim.SetBool("isStartExploding", isStart);
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //does it right?
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        if (attackable != null)
        {
            attackable.ApplyDamage(Damage,gameObject);
        }
    }
}
