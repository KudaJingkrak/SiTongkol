using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
    public MoveAble_State state_move = MoveAble_State.Idle;
    public TriggerMove Trigger_X;
    public TriggerMove Trigger_Y;
    public Rigidbody2D rb_Object;
    public BoxCollider2D boxCollider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // if (Trigger_X.inArea)
        // {
        //     state_move = MoveAble_State.MoveX;
        //     rb_Object.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        // }
        // else if (Trigger_Y.inArea)
        // {
        //     state_move = MoveAble_State.MoveY;
        //     rb_Object.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        // }
        // else
        // {
        //     state_move = MoveAble_State.Idle;
        //     rb_Object.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        // }
	}
}
