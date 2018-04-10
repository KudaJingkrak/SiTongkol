using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {
    public const float SPEED_MUL = 0.0001f;
    public ComboEnum kondisi_Player;
    public Slider sliderCombo;
    public Image[] Visual_Cues;

    public int value_Slider;
    public int Current_Combo;
    
    [Header("UI Fill Region")]
    public RectTransform fillArea;
    public RectTransform perfectRegion;
    public RectTransform goodRegion;

    float addedValue;
    float initAttackSpeed = 30f;

    void Start()
    {
        sliderCombo.value = 1;
        //initializing component
        
        goodRegion.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0.3f, 3f);

        Debug.Log("Panjanaaan " + fillArea.rect.width);
    }

    void Update()
    {
        if(sliderCombo.value <= 1){
            sliderCombo.value += addedValue;
        }
    }
    
    public void SetMargin(int comboCounter, Equipment equipment){
        float perfectInset = fillArea.rect.width * equipment.perfect[comboCounter].bottom;
        float perfectSize = (equipment.perfect[comboCounter].top - equipment.perfect[comboCounter].bottom) * fillArea.rect.width;
        float goodInset = fillArea.rect.width * equipment.good[comboCounter].bottom;
        float goodSize = (equipment.good[comboCounter].top - equipment.good[comboCounter].bottom) * fillArea.rect.width;

        perfectRegion.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, perfectInset, perfectSize);
        goodRegion.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, goodInset, goodSize);
    }

    void ResetValue()
    {
        sliderCombo.value = 0;
    }

    public void CalcAddedValue(float attackSpeed){
        addedValue = (1f / Time.deltaTime) * (attackSpeed * SPEED_MUL);
    }

    public bool IsBetween(float value, Margin margin){
        return value >= margin.bottom && value <= margin.top;
    }

    public ComboFeedback FilterCombo(int comboCounter, Equipment equipment){
        
        ComboFeedback feedback = new ComboFeedback();
        initAttackSpeed = equipment.attackSpeed[0].value;

        if(comboCounter > 0 && sliderCombo.value >= 1){ // jika bar sudah penuh otomatis good dan combo = 1
            feedback.combo = ComboEnum.Good;
            feedback.counter = 1;

        }else if(comboCounter < equipment.perfect.Length){
                        
            if(IsBetween(sliderCombo.value, equipment.perfect[comboCounter])){  //cek perfect
                feedback.combo = ComboEnum.Perfect;
                feedback.counter = comboCounter + 1;

            }else if(IsBetween(sliderCombo.value, equipment.good[comboCounter])){ //cek good
                feedback.combo = ComboEnum.Good;
                feedback.counter = comboCounter + 1;

            }else{ //else miss
                feedback.combo = ComboEnum.Miss;
                feedback.counter = 0;
            }

            if(feedback.counter > 0 && feedback.counter >= equipment.perfect.Length){ // if after max combo
                feedback.counter = 0;
            }
        }

        CalcAddedValue(equipment.attackSpeed[feedback.counter].value);
        SetMargin(feedback.counter, equipment);
        ResetValue();
        return feedback;
    }
}

public class ComboFeedback{
    public ComboEnum combo = ComboEnum.Miss;
    public int counter = 0;
}
