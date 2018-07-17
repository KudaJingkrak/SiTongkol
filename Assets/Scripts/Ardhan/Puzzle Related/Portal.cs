using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Portal : MonoBehaviour {
    public string NamaSceneTujuan;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3(7.716572f, 51.5f, 0);
            collision.GetComponent<GayatriCharacter>().transisiCamera.TransitionExit();
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(4);
        Application.LoadLevel(NamaSceneTujuan);
    }
}
