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

    private Bomb_Script bombScript;
    private BombAoE bombAOE;

    //Harus Buat Animation Bool State untuk Bomb dan Explode nya.
    public bool isStartBomb;
    public bool isStartExploding;

	// Use this for initialization
	void Start () {
        bombScript = bomb_Sprite.GetComponent<Bomb_Script>();
        bombAOE = area_KenaBomb.GetComponent<BombAoE>();

        Damage_Able = false;
        isStartBomb = false;
        isStartExploding = false;

        Disable();
        
	}

    void Enable(){
        bomb_Sprite.SetActive(true);
        bombScript = bomb_Sprite.GetComponent<Bomb_Script>();
        area_KenaBomb.SetActive(true);
        bombAOE = area_KenaBomb.GetComponent<BombAoE>();
        area_KenaBomb.SetActive(false);
    }

    void Disable(){
        bomb_Sprite.SetActive(false);
        area_KenaBomb.SetActive(false);
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
        Enable();
        isActivated = true;
        isStartBomb = true;
        positionUser_Inside = position;
        bomb_Sprite.transform.position = positionUser_Inside;
        bombScript.SetStart(true);
    }

    //Step 2
    public void Exploded_Bomb()
    {
        bomb_Sprite.SetActive(false);
        area_KenaBomb.SetActive(true);
        bombAOE = area_KenaBomb.GetComponent<BombAoE>();
        isStartBomb = false;
        area_KenaBomb.transform.position = positionUser_Inside;
        isStartExploding = true;
        Damage_Able = true;
        // Bomb_Anim.SetBool("isStartBomb", false);
        // Bomb_Anim.SetBool("isStartExploding", true);
        bombScript.SetStart(false);
        bombAOE.SetStart(true);
    }

    //Step 3
    public void End_Explosion()
    {
        area_KenaBomb.transform.position = positionUser_Outside;
        isStartExploding = false;
        Damage_Able = false;
        //Bomb_Anim.SetBool("isStartExploding", false);
        bombAOE.SetStart(false);
        isActivated = false;
        Disable();
    }
	
	// Update is called once per frame
	void Update () {
        if(bombScript){
            bombScript.SetStart(isStartBomb);
        }

        if(bombAOE){
            bombAOE.SetStart(isStartExploding);
        }
	}
}
