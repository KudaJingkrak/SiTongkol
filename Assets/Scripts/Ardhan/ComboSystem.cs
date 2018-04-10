using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {
    public ComboEnum kondisi_Player;
    public Slider Slider_Combo;
    public Image[] Visual_Cues;

    public float value_Slider;
    public int Current_Combo;
    int Maximum_Combo = 5;


    public float Time;
    public float value_added;

    float[] Threshold_Margin_Awal = new float[5];
    float[] Threshold_Margin_Akhir = new float[5];
    float[] Attack_Speed;

    public bool isHit;

    void Start()
    {
        Slider_Combo.value = 0;
        Current_Combo = 0;
        //initializing component

        //Unfilled Bawah
        Threshold_Margin_Awal[0] = 0;
        Threshold_Margin_Akhir[0] = 0.29f;
        //Next Hit Bawah
        Threshold_Margin_Awal[1] = 0.3f;
        Threshold_Margin_Akhir[1] = 0.49f;
        //Critical Damage
        Threshold_Margin_Awal[2] = 0.5f;
        Threshold_Margin_Akhir[2] = 0.59f;
        //Next Hit Atas
        Threshold_Margin_Awal[3] = 0.6f;
        Threshold_Margin_Akhir[3] = 0.79f;
        //Unfilled Atas
        Threshold_Margin_Awal[4] = 0.8f;
        Threshold_Margin_Akhir[4] = 1;
        StartCoroutine(Adding_Value());
    }

    void Update()
    {

    }

    IEnumerator Adding_Value()
    {
        //this is just filling the Slider
        while (Slider_Combo.value <= 1)
        {
            yield return new WaitForSeconds(Time);
            Slider_Combo.value += value_added;
            value_Slider = Slider_Combo.value;
            if (Slider_Combo.value == 1)
            {
                Reset_Value();
            }
        }
    }

    void Start_Value()
    {

    }

    void Reset_Value()
    {
        Slider_Combo.value = 0;
    }

    void Reset_Counter()
    {
        Current_Combo = 0;
    }

    public bool Check_Threshold(int Current_Combo)
    {
        if (Current_Combo < Maximum_Combo)
        {
            if (Slider_Combo.value >= Threshold_Margin_Awal[Current_Combo] && Slider_Combo.value <= Threshold_Margin_Akhir[Current_Combo])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        Reset_Value();
        return false;
    }

    public ComboEnum FilterCombo(int comboCounter)
    {
        if (Current_Combo < Maximum_Combo)
        {
            //Critical Damage
            if (Slider_Combo.value >= Threshold_Margin_Awal[2] && Slider_Combo.value <= Threshold_Margin_Akhir[2])
            {
                comboCounter++;
                Reset_Value();
                print("Perfect");
                return ComboEnum.Perfect;
            }

            //nextHit Bawah
            if (Slider_Combo.value >= Threshold_Margin_Awal[1] && Slider_Combo.value <= Threshold_Margin_Akhir[1])
            {
                comboCounter++;
                Reset_Value();
                print("Good");
                return ComboEnum.Good;
            }

            //nextHit Atas
            if (Slider_Combo.value >= Threshold_Margin_Awal[3] && Slider_Combo.value <= Threshold_Margin_Awal[3])
            {
                comboCounter++;
                Reset_Value();
                print("Good");
                return ComboEnum.Good;
            }

            //Unfilled Bawah
            if (Slider_Combo.value >= Threshold_Margin_Awal[0] && Slider_Combo.value <= Threshold_Margin_Akhir[0])
            {
                comboCounter = 0;
                Reset_Value();
                print("Miss");
                return ComboEnum.Miss;
            }

            //Unfilled Atas
            if (Slider_Combo.value >= Threshold_Margin_Awal[4] && Slider_Combo.value <= Threshold_Margin_Akhir[4])
            {
                comboCounter = 0;
                Reset_Value();
                print("Miss");
                return ComboEnum.Miss;
            }
            Reset_Value();
            return ComboEnum.Miss;
        }
        return ComboEnum.Default;
    }

}
