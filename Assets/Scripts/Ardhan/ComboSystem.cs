using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {
	public int MaxCombo;
	public bool PlayerHit;
	public Slider SliderPointer;
	public Text Status_UI;
	public Text Combo_UI;
	public float Interval;
	public float speed;
	public int comboCurrent = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(AddValue(Interval));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			PlayerHit = true;
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			PlayerHit = false;
		}
	}

	IEnumerator AddValue(float second)
	{
		while(comboCurrent<=MaxCombo)
		{
			if(PlayerHit)
			{
				if(SliderPointer.value >= 0.71f && SliderPointer.value <= 0.835f)
				{
					comboCurrent++;
					Status_UI.text = "Combo Hit!";
					Combo_UI.text = "" + comboCurrent;
					SliderPointer.value = 0;  
					if(comboCurrent == MaxCombo)
					{
						comboCurrent = MaxCombo;
						Status_UI.text = "Full Combo";
						Combo_UI.text = "" + comboCurrent;
						SliderPointer.value = 0;
					}
					if(comboCurrent > MaxCombo)
					{
						comboCurrent = 1;
						Status_UI.text = "Combo Hit!";
						Combo_UI.text = "" + comboCurrent;
						SliderPointer.value = 0;					}          
				}
				else
			   {
				comboCurrent = 0;
				Status_UI.text = "Combo miss";
				Combo_UI.text = "" + comboCurrent;
				SliderPointer.value = 0;
			   }
			}

			else if(SliderPointer.value >= 1f)
			{
				comboCurrent = 0;
				SliderPointer.value = 0;
				Status_UI.text = "Full Slider";
				Combo_UI.text = "" + comboCurrent;
			}
			
			
			SliderPointer.value += speed;
			yield return new WaitForSeconds(second);			
		}
	}
}
