using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guirao.UltimateTextDamage;

public class Slime_Tutorial : BaseEnemy, IAttackable {
    Animator Slime_Anim;

    [Header("UI Manager")]
    public UltimateTextDamageManager textDamageManager;
    public GameObject healthCanvas;
    public float healthTimer = 1;
    private Slider _healthSlider;
    private float _healthTimer;

    public GameObject SpriteSorter;

    public float DamageSlime;
    public bool isAttacking;

    private GayatriCharacter playerPresence;

    public override void Initialized()
    {
        _health = health;
    }

    void Awake()
    {
        Initialized();
        _healthSlider = healthCanvas.GetComponentInChildren<Slider>();
        Slime_Anim = this.GetComponent<Animator>();
        
    }

    void LateUpdate()
    {
        if (healthCanvas.activeSelf)
        {
            _healthSlider.value = _health / health;
        }
    }

    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        Slime_Anim.SetTrigger("isHit");
        ResetAttack();
        isAttacking = false;
        doFlash();
        _health -= damage;
        ShowHealthCanvas();
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Destruct()
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public void Fall()
    {
        /*
         * TO DO:
         * - 1. Freeze into certain location (X,Y,Z)
         * - 2. DO The Fall Animation
         * - 3. Attackable.Die()
         * - 4. Disabled
         * - 5. Done
         */
    }

    public void Knockback(Transform causer)
    {

        throw new NotImplementedException();
    }

    public void Knockback(Transform causer, float power = 0)
    {
        throw new NotImplementedException();
    }
    
	// Use this for initialization
	void Start () {
		
	}

    void ShowHealthCanvas()
    {
        _healthTimer = healthTimer;
        if (!healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(true);
        }
    }
    void HideHealthCanvas()
    {
        if (healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
        }
    }
    public void ResetHit()
    {
        Slime_Anim.ResetTrigger("isHit");
    }

    public void ResetAttack()
    {
        Slime_Anim.ResetTrigger("isAttack");
        isAttacking = false;
        SpriteSorter.transform.position = transform.position + new Vector3(0, -0.52f) * transform.lossyScale.x;
    }

    public void Menimpa_Sorter()
    {
        SpriteSorter.transform.position = transform.position + new Vector3(0, -1.29f) * transform.lossyScale.x;
    }

    // Update is called once per frame
    void Update () {
        if (healthCanvas.activeSelf && _healthTimer <= 0 )
        {
            HideHealthCanvas();
        }
        else
        {
            _healthTimer -= Time.deltaTime;
        }
        if (playerPresence)
        {
            if (!isAttacking)
            {
                Slime_Anim.SetTrigger("isAttack");
                playerPresence.ApplyDamage(DamageSlime, this.gameObject);
                isAttacking = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPresence = collision.gameObject.GetComponent<GayatriCharacter>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPresence = null;
        }
    }
}
