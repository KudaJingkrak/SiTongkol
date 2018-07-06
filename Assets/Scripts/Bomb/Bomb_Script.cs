using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : MonoBehaviour {
    public BombSystem sysBomb;
	public Animator anim;
	void Awake(){
		//anim = GetComponent<Animator>();
	}

    public void StartExplosion()
    {
        sysBomb.Exploded_Bomb();
    }

	public void SetStart(bool isStart){
		if(!anim){
			anim = GetComponent<Animator>();
		}

		if(anim.isActiveAndEnabled)
		{
			anim.SetBool("isStartBomb", isStart);
		}
		
	}
}
