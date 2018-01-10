using UnityEngine;
public interface IAttackable{
	void ApplyDamage(float damage);
	void ApplyDamage(float damage, GameObject causer);
	void ApplyDamage(float damage, GameObject causer, DamageType type);
	void ApplyDamage(float damage, GameObject causer, DamageType type, DamageEffect effect);
}
