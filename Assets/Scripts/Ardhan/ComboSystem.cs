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
        comboCurrent = comboBerapa;
		if(comboCurrent < senjata.maxCombo)
		{
            if (comboCurrent == 0)
            {
                //change the speed to senjata.speed [0]

                Interval = senjata.attackSpeed[0].wait;
                speed = senjata.attackSpeed[0].value;
            }
            if (comboCurrent == 1)
            {
                Interval = senjata.attackSpeed[1].wait;
                speed = senjata.attackSpeed[1].value;
                //change the speed to senjata.speed [1]
            }

            if (comboCurrent == 2)
            {
                Interval = senjata.attackSpeed[2].wait;
                speed = senjata.attackSpeed[2].value;
                //change the speed to senjata.speed [2]
            }
            if (comboCurrent == 3)
            {
                Interval = senjata.attackSpeed[3].wait;
                speed = senjata.attackSpeed[3].value;

                //change the speed to senjata.speed [3]
            }
            if (comboCurrent == 4)
            {
                Interval = senjata.attackSpeed[4].wait;
                speed = senjata.attackSpeed[4].value;
                 
                //change the speed to senjata.speed [4]
            }
			Perfect_Region.sizeDelta = new Vector2(4.5f*(senjata.perfect[comboCurrent].top-senjata.perfect[comboCurrent].bottom),Perfect_Region.sizeDelta.y);
			Perfect_Region.anchoredPosition = new Vector2(4.5f*senjata.perfect[comboCurrent].bottom,Perfect_Region.anchoredPosition.y);
			
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
        SliderPointer.value = 0;
        comboCurrent = 0;
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
