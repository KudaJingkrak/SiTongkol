using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Droppable))]
public class Enemy : MonoBehaviour, IAttackable{
	public MonsterName label; 	
	public float health = 10f;
	Droppable droppable;
	private float _health;

    // Use this for initialization
    void Start () {
		_health = health;
		droppable = gameObject.GetComponent<Droppable>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage(float damage = 0, GameObject causer = null, DamageType type = DamageType.Normal, DamageEffect effect = DamageEffect.None){
        _health -= damage;

		Debug.Log("Health "+gameObject.name+" : " + _health);
		
		if(_health <= 0) Die();

    } 

    public void Destruct(){
        throw new System.NotImplementedException();
    }

    public void Die()
    {
		Quest.Instance.AddMonsterCounter(label);
		Quest.Instance.CheckProgressingObjective(null, label);
		droppable.DropItem(transform);
		gameObject.SetActive(false);
    }
}
