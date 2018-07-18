using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
    public MoveAble_State state_move = MoveAble_State.Idle;
    public TriggerMove Trigger_X;
    public TriggerMove Trigger_Y;
    public Rigidbody2D rb_Object;
    public BoxCollider2D boxCollider;
    public GameObject pulledActor;

    void Awake()
    {
        rb_Object = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!pulledActor)
        {
            gameObject.layer = 0;
        }
	}
}
