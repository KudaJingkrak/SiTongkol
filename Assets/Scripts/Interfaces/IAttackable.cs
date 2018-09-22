using UnityEngine;
public interface IAttackable{
	void ApplyDamage(float damage = 0f, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None);

	void Destruct();

	void Die();

    void Fall();

    void Knockback(Transform causer);

    void Knockback(Transform causer, float power = 0f);
}
