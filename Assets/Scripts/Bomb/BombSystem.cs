using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BombSystem : MonoBehaviour {
    public GameObject bomb_Sprite;
    public GameObject area_KenaBomb;
    public bool isActivated;
    public float waitingTime;
    public Vector3 positionUser_Inside;
    public Vector3 positionUser_Outside;
    public bool Damage_Able;

    public Animator Bomb_Anim;
    public Animator BombExploding_Anim;

    //Harus Buat Animation Bool State untuk Bomb dan Explode nya.
    public bool isStartBomb;
    public bool isStartExploding;

	// Use this for initialization
	void Start () {
        positionUser_Outside = new Vector3(28.58f,-12.42f,0);
        bomb_Sprite.transform.position = positionUser_Outside;
        area_KenaBomb.transform.position = positionUser_Outside;
        Damage_Able = false;
        isStartBomb = false;
        isStartExploding = false;
	}

    void Activated()
    {
        StartCoroutine(ActivatedWait());
        //Animation of area_KenaBomb


        /*
         * Things to implements:
         * 1. Move Bomb_Sprite outside
         * 2. Move area_KenaBomb inside
         * 3. Play Animation of area_KenaBomb
         * 4. Function of BombAoE
         * 5. Move area_KenaBomb outside
         * 6. Done. 
         */
         
    }

    IEnumerator ActivatedWait()
    {
        bomb_Sprite.transform.position = positionUser_Outside;
        area_KenaBomb.transform.position = positionUser_Inside;
        //animation of the area_kenaBomb
        //function of BombAoE
        yield return new WaitForSeconds(1);
        Debug.Log("harusnya keluar");
        area_KenaBomb.transform.position = positionUser_Outside;

    }

    //Step 1
    public void Start_Bomb(Vector3 position)
    {
        isStartBomb = true;
        positionUser_Inside = position;
        bomb_Sprite.transform.position = positionUser_Inside;
        Bomb_Anim.SetBool("isStartBomb", true);

    }

    //Step 2
    public void Exploded_Bomb()
    {
        bomb_Sprite.transform.position = positionUser_Outside;
        isStartBomb = false;
        area_KenaBomb.transform.position = positionUser_Inside;
        isStartExploding = true;
        Damage_Able = true;
        Bomb_Anim.SetBool("isStartBomb", false);
        Bomb_Anim.SetBool("isStartExploding", true);
    }

    //Step 3
    public void End_Explosion()
    {
        area_KenaBomb.transform.position = positionUser_Outside;
        isStartExploding = false;
        Damage_Able = false;
        Bomb_Anim.SetBool("isStartExploding", false);
    }
	
	// Update is called once per frame
	void Update () {
        Bomb_Anim.SetBool("isStartBomb", isStartBomb);
        BombExploding_Anim.SetBool("isStartExploding", isStartExploding);
	}
}
