using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {
    public SwitchManager switchnya;
    private Animator torch_Anim;
	// Use this for initialization
	void Start () {
        torch_Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (switchnya.boolSwitch)
        {
            torch_Anim.SetBool("isPressed", true);
        }
        else
        {
            torch_Anim.SetBool("isPressed", false);
        }
	}
}
