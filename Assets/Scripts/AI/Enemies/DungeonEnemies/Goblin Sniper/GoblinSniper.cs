using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSniper : BaseDungeonEnemy, IAttackable
{

	#region Attackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        throw new System.NotImplementedException();
    }

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Knockback(Transform causer)
    {
        throw new System.NotImplementedException();
    }

    public void Knockback(Transform causer, float power = 0)
    {
        throw new System.NotImplementedException();
    }
	#endregion

	
}
