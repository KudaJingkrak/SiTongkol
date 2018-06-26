using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    public JenisPuzzle tipePuzzle;
    [Header("Switch")]
    public SwitchManager[] switchedObject;
    public bool[] jawabanBener;
    
    [Header("Destroying")]
    public GameObject[] destroyedObject;
    public bool PuzzleCompleted;

	// Use this for initialization
	void Start () {
        PuzzleCompleted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkPuzzle()
    {
        if (tipePuzzle == JenisPuzzle.Switching)
        {
            for (int i = 0; i < switchedObject.Length; i++)
            {
                if (switchedObject[i].boolSwitch == jawabanBener[i])
                {
                    if (i == switchedObject.Length - 1)
                    {
                        PuzzleCompleted = true;
                        Debug.Log("Puzzle Completed");
                    }
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        if (tipePuzzle == JenisPuzzle.Destroy)
        {
            //TODO:
            /*
             * 1. Loops.
             * 2. is each of the gameobject is finally destroyed.
             * 3. Opened.
             */
        }
    }

}
