using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintu : MonoBehaviour {
    public GameObject PintuBuka;
    public GameObject PintuTutup;


    public void PintuKebuka()
    {
        PintuBuka.active = true;
        PintuTutup.active = false;
    }

    public void PintuKetutup()
    {
        PintuBuka.active = false;
        PintuTutup.active = true;
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
