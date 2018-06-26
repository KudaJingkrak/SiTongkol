using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AIMovementComp))]
public class LustComponent : MonoBehaviour, IAttackable
{
    private GameObject player;
	private AIMovementComp _movementComp;
    
    // Temporaray
    public bool isCharge = true;
    public float launchPower = 800f;

    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        
    }

    public void Destruct()
    {
        
    }

    public void Die()
    {
        
    }

    public void Knockback(Transform causer)
    {
        Knockback(causer, 0f);
    }

    public void Knockback(Transform causer, float power = 0)
    {
         Vector2 force = -(causer.position - transform.position).normalized * power;
        _movementComp.Rigid2D.velocity = force;
    }

    private void Awake()
    {
        _movementComp = GetComponent<AIMovementComp>();
    }

    private void Start()
    {
        StartCoroutine(SwitchMove(1.75f));
    }

    private void Update()
    {
        if(!player)
        {   
            if(GameManager.Instance)
            {
                player = GameManager.Instance.m_Player;
            }
        }

        // temporary nani?
        if(isCharge)
        {
            _movementComp.Move(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f));
        }
        
    }

    // temporary nani?
    IEnumerator SwitchMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        isCharge = false;
        StartCoroutine(LaunchToPlayer(1f));
    }

    // temporary nani?
    IEnumerator LaunchToPlayer(float delay){
        Vector2 dir = (player.transform.position - transform.position).normalized;
        _movementComp.Move(Vector2.zero);
        _movementComp.Launch(dir, launchPower);
        yield return new WaitForSeconds(delay);
        _movementComp.Move(Vector2.zero);
        isCharge = true;
        StartCoroutine(SwitchMove(1.75f));
    }

    

}
