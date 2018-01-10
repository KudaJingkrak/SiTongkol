using UnityEngine;
public interface IAttackable{
	void ApplyDamage(float damage = 0f, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None);

	void Destruct();

	void Die();
}
