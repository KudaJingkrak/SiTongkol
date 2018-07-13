using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour, IAttackable{
	public BaseEnemy parent;
	private IAttackable parentAttack;

	void Awake()
	{
		parentAttack = parent.GetComponent<IAttackable>();
	}


    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
		parentAttack.ApplyDamage(damage, causer, type, effect);
		
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
}
