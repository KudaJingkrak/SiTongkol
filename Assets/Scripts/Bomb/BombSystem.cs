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

	// Use this for initialization
	void Start () {
        positionUser_Outside = new Vector3(28.58f,-12.42f,0);
        bomb_Sprite.transform.position = positionUser_Outside;
        area_KenaBomb.transform.position = positionUser_Outside;
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

    public void DeployBomb(Vector3 position)
    {
        positionUser_Inside = position;
        bomb_Sprite.transform.position = positionUser_Inside;
        /*
         * 1. Position Bomb = positionUser
         * 
         */
        StartCoroutine(Waiting());
    }
    IEnumerator Waiting()
    {
        float tempWait = waitingTime;
        while (tempWait != 0)
        {
            yield return new WaitForSeconds(1f);
            tempWait--;
            /*
             * 1. Play the animation by switching the color in here of the BombSprite attributes.
             * 
             */
        }
        Activated();
        //disini nge wait terus kaya animasi flip flop sampe abis waiting timenya terus meleduk.

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
