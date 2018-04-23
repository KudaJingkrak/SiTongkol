using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {
	public StatusManager Status {get{return GameManager.Instance.m_StatusManager;}}

    public void SetPlayer(GameObject player)
    {
        GameManager.Instance.m_Player = player;
    }

	
	//UI
	public UIManager UI {get{return GameManager.Instance.m_UIManager;}}
}
