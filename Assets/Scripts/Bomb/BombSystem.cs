using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BombSystem : MonoBehaviour {
    public SpriteRenderer bomb_Sprite;
    public GameObject area_KenaBomb;
    public bool isActivated;
    public float waitingTime;

	// Use this for initialization
	void Start () {
        isActivated = false;
        StartCoroutine(Waiting());
	}

    void Activated()
    {
        /*
         * Basically, ini cuma class switching aja sih.
         * 
         * Bomb Meledak, dengan Step switch position bombSprite dan Area Bomb
         * 1. Object Pooling bomb Sprite
         * 2. Object Pooling Area Bomb
         * 3. Nanti tinggal gimana efek dari Trigger Area Bomb aja makanya bentuknya GameObject.
         * 4. berarti buat IEnumerator lagi kapan ilangnya.
         */
    }

    IEnumerator ExplosionWait()
    {

        /*
         * Step by Step:
         * 1. Animate
         * 2. Wait
         * 3. *poof* pake object Pooling si GameObjectnya.
         * 4. Done, isActivated False.
         */

        yield return null;
    }

    IEnumerator Waiting()
    {
        while (waitingTime != 0)
        {
            yield return new WaitForSeconds(0.5f);
            //animasi
            //waktu di kurangin.
        }
        Activated();
        //disini nge wait terus kaya animasi flip flop sampe abis waiting timenya terus meleduk.

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
