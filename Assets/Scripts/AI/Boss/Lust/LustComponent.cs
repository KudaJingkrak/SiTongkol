using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AIMovementComp))]
public class LustComponent : MonoBehaviour, IAttackable
{
	private AIMovementComp _movementComp;

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
}
