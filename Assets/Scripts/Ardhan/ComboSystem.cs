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
	public RectTransform Perfect_Region;
	public RectTransform Good_Region;
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
			Perfect_Region.sizeDelta = new Vector2(4.5f*(senjata.perfect[comboBerapa].top-senjata.perfect[comboBerapa].bottom),Perfect_Region.sizeDelta.y);
			Perfect_Region.anchoredPosition = new Vector2(4.5f*senjata.perfect[comboBerapa].bottom,Perfect_Region.anchoredPosition.y);
			
			Good_Region.sizeDelta = new Vector2(4.5f*(senjata.good[comboBerapa].top-senjata.good[comboBerapa].bottom),Good_Region.sizeDelta.y);
			Good_Region.anchoredPosition = new Vector2(4.5f*senjata.good[comboBerapa].bottom,Good_Region.anchoredPosition.y);

			if(SliderPointer.value >= senjata.perfect[comboBerapa].bottom && SliderPointer.value <= senjata.perfect[comboBerapa].top)
			{
				SliderPointer.value = 0;
				return ComboEnum.Perfect;
			}
			else if(SliderPointer.value >= senjata.good[comboBerapa].bottom && SliderPointer.value <= senjata.good[comboBerapa].top)
			{
				SliderPointer.value = 0;
				return ComboEnum.Good;
			}
			else if(SliderPointer.value >= 1)
			{
				SliderPointer.value = 0;
				return ComboEnum.Good;
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
		while(true)
		{
			if(SliderPointer.value < 1)
			{
				SliderPointer.value += speed;
			}
			else
			{
				SliderPointer.value = 1;
				SliderPointer.enabled = false;
			}
			yield return new WaitForSeconds(second);
		}
	}
}
