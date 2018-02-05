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
	public float valueMarginAwal;
	public float valueMarginAkhir;
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
		//intinya disini gue 
	}

	public ComboEnum FilterCombo(Equipment senjata,int comboBerapa)
	{
		//perfect Filter;	
		if(comboBerapa < senjata.maxCombo)
		{
			if(SliderPointer.value >= senjata.perfect[comboBerapa].bottom && SliderPointer.value <= senjata.perfect[comboBerapa].top)
			{
				comboBerapa++; //kayanya salah deh
				return ComboEnum.Perfect;
			}
			else if(SliderPointer.value >= senjata.good[comboBerapa].bottom && SliderPointer.value <= senjata.good[comboBerapa].top)
			{
				comboBerapa++; //kayanya salah deh
				return ComboEnum.Good;
			}
			else
			{
				comboBerapa = 0;
				return ComboEnum.Miss;
				/*
				Harus make sure disini dia nge hide visibility dari slidePointernya
				bedanya sama miss apa ya?
				 */
			}
			//emang comboBerapanya bisa di set
		}
		return ComboEnum.Miss;
	}
	IEnumerator AddValue(float second)
	{
		while(comboCurrent<=MaxCombo)
		{
			if(PlayerHit)
			{
				if(SliderPointer.value >= valueMarginAwal && SliderPointer.value <= valueMarginAkhir)
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
						//disini pokoknya ada informasi bahwa dia kombo -> Interaction
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
