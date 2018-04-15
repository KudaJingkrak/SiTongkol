using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {
	public StatusManager Status {get{return GameManager.Instance.m_StatusManager;}}

	
	//UI
	public UIManager UI {get{return GameManager.Instance.m_UIManager;}}
}
