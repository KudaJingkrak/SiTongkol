using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLayer : MonoBehaviour {
	SpriteRenderer parentRender;
    SpriteRenderer myRender;

    void Awake()
    {
        parentRender = GetComponentInParent<SpriteRenderer>();
        myRender = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        myRender.sortingOrder = parentRender.sortingOrder;  
    }
}
