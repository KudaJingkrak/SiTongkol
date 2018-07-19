using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMove : MonoBehaviour {
    public bool inArea;
    public Rigidbody2D parent_Rigidbody;
	// Use this for initialization
	void Start () {
        parent_Rigidbody.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = true;
                
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            // if(Input.GetButtonDown("X"))
            // {
            //     transform.parent.SetParent(other.transform);
            //     parent_Rigidbody.isKinematic = true;
            //     other.gameObject.GetComponent<Rigidbody2D>().constraints = transform.parent.gameObject.GetComponent<Rigidbody2D>().constraints;
            // }
            // else if(Input.GetButtonUp("X"))
            // {   
            //      transform.parent.SetParent(null);
            //      parent_Rigidbody.isKinematic = false;
            //      other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                 
            // }
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = false;
        }
        
    }
}
