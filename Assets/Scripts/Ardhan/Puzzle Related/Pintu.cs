using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintu : MonoBehaviour {
    public GameObject PintuBuka;
    public GameObject PintuTutup;
    private bool _hasPlayer = false;

    public void PintuKebuka()
    {
        if(!PintuBuka.activeSelf)PintuBuka.SetActive(true);
        if(PintuTutup.activeSelf)PintuTutup.SetActive(false);
    }

    public void PintuKetutup()
    {
        if(_hasPlayer) return;
        if(PintuBuka.activeSelf)PintuBuka.SetActive(false);
        if(!PintuTutup.activeSelf)PintuTutup.SetActive(true);
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            _hasPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            _hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            _hasPlayer = false;
        }
    }
}
