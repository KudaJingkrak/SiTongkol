using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IAttackable{
	public Droppable droppable;
	public float health = 10f;
	private float _health;

    // Use this for initialization
    void Start () {
		_health = health;
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
		droppable.DropItem(transform);
		gameObject.SetActive(false);
    }
}
