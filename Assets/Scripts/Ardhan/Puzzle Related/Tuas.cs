using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuas : MonoBehaviour, IAttackable {
    public PuzzleManager puzzleNya;
    private Animator tuasAnim;
    public Pintu pintunya;
    public bool isCompleted;


	// Use this for initialization
	void Start () {
        tuasAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Animation_Wrong()
    {
        tuasAnim.SetBool("isPressed", true);
        yield return new WaitForSeconds(1);
        tuasAnim.SetBool("isPressed", false);
        yield return new WaitForSeconds(0.5f);
        puzzleNya.ResetSwitches();
    }

    IEnumerator Animation_Right()
    {
        tuasAnim.SetBool("isPressed", true);
        yield return new WaitForSeconds(1);
        pintunya.PintuKebuka();
    }



    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        puzzleNya.checkPuzzle();
        /*
         * TODO:
         * CheckingThePuzzle 
         */
        if (puzzleNya.PuzzleCompleted)
        {
            StartCoroutine(Animation_Right());

        }
        else
        {
            StartCoroutine(Animation_Wrong());
        }
        
    }

    public void Destruct()
    {

    }

    public void Die()
    {

    }

    public void Knockback(Transform causer)
    {

    }

    public void Knockback(Transform causer, float power = 0)
    {

    }

    public void Fall()
    {
        
    }
}
