using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AIMovementComp), typeof(AITargetComp))]
public class GoblinSniper : BaseDungeonEnemy, IAttackable
{   
    private AIMovementComp _move;
    private AITargetComp _aim;

    #region PandaTask

    #endregion

	#region Attackable
    public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None)
    {
        Debug.Log("Diserang player");
        doFlash();
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

    void Awake()
    {
        _aim = GetComponent<AITargetComp>();
        _move = GetComponent<AIMovementComp>();
    }
	void Update()
    {
        // Update Aim Direction
        switch(_move.GetDirection())
        {
            case(Direction.Back):
                _aim.SetAimRotation(180f);
            break;
            case(Direction.Front):
                _aim.SetAimRotation(0f);
            break;
            case(Direction.Left):
                _aim.SetAimRotation(270f);
            break;
            case(Direction.Right):
                _aim.SetAimRotation(90f);
            break;
        }

        
    }

}
