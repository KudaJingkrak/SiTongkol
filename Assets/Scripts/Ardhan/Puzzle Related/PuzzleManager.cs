using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    public JenisPuzzle tipePuzzle;
    [Header("Switch")]
    public SwitchManager[] switchedObject;
    public bool[] jawabanBener;
    public float SwitchesCooldown;
    
    [Header("Destroying")]
    public GameObject[] destroyedObject;
    public bool PuzzleCompleted;
    public bool isJembatan;

	// Use this for initialization
	void Start () {
        if (!isJembatan)
        {
            PuzzleCompleted = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ResetSwitches()
    {
        for (int i = 0; i < switchedObject.Length; i++)
        {
            switchedObject[i].boolSwitch = false;
            switchedObject[i].changeSprite();
        }
    }

    IEnumerator StartCooldownResetSwitches()
    {
        yield return new WaitForSeconds(SwitchesCooldown);
        ResetSwitches();
        Debug.Log("Finished Reset Switches");
    }

    public void checkResetSwitch()
    {
        for (int i = 0; i < switchedObject.Length; i++)
        {
            if (switchedObject[i].boolSwitch == true)
            {
                if (i == switchedObject.Length - 1)
                {
                    StartCoroutine(StartCooldownResetSwitches());
                    Debug.Log("Starting Reset Switches");
                }
            }
        }
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
                    PuzzleCompleted = false;
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
