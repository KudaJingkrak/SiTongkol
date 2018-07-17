using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class StatusManager : BaseClass {

    public ProCamera2DTransitionsFX transisiCamera;

	public float currentHealth;
    public float currentMana;
    public float currentStamina;
    
    //Boundary MAX Health
    public float maxHealth;
    public float maxMana;
    public float maxStamina;

    public float percentHealth{ get{return currentHealth/maxHealth;}}
    public float percentMana{get{return currentMana/maxMana;}}
    public float percentStamina{get{return currentStamina/maxStamina;}}

    //How much speedRate Stamina
    public float staminaPoint;
    public bool isRegenStamina;

    #region Health Function
    public void Initiated_Health()
    {
        currentHealth = maxHealth;
    }

    public void Increased_Health(float banyak_Health)
    {
        currentHealth += banyak_Health;
    }

    public void Decreased_Health(float banyak_Health)
    {
        if (currentHealth < banyak_Health)
        {
            currentHealth = maxHealth;
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            currentHealth -= banyak_Health;
        }
    }

    public bool CheckBelow_Health(float banyak_Health)
    {
        if (currentHealth < banyak_Health)
        {
            return true;
        }
        return false;
    }

    public float get_Health()
    {
        return currentHealth;
    }
    #endregion 

#region Mana Function
    public void Initiated_Mana()
    {
        currentMana = maxMana;
    }

    public void Increased_Mana(float banyak_Mana)
    {
        currentMana += banyak_Mana;
    }

    public void Decreased_Mana(float banyak_Mana)
    {
        currentMana -= banyak_Mana;
    }

    public bool CheckBelow_Mana(float banyak_Mana)
    {
        if (currentMana < banyak_Mana)
        {
            return true;
        }
        return false;
    }

    public float get_Mana()
    {
        return currentMana;
    }

    #endregion Mana Function

    #region Stamina Function

    IEnumerator Stamina_itr;

    public void Initiated_Stamina()
    {
        currentStamina = maxStamina;
    }

    public void Increased_Stamina(float banyak_Stamina)
    {
        //float percentStamina = banyak_Stamina / maxStamina;
        if (CheckBoundaryUpper(banyak_Stamina))
        {
            currentStamina += banyak_Stamina;
        }
    }

    public void Decreased_Stamina(float banyak_Stamina)
    {
        if (CheckBoundaryBelow(currentStamina))
        {
            currentStamina -= banyak_Stamina;
        }
    }

    public bool CheckBoundaryBelow(float banyak_value)
    {
        if (banyak_value >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }

    public bool CheckBoundaryUpper(float banyak_value)
    {
        if (banyak_value < 100)
        {
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }

    public float get_Stamina()
    {
        return currentStamina;
    }

    //[TODO] NEED IMPROVEMENT.
    public void Start_Regenerating_Stamina()
    {
        //ini buat gate aja dari staminanya.
        isRegenStamina = true;
    }

    //[TODO] NEED IMPROVEMENT
    public void Stop_Regenerating_Stamina()
    {
        isRegenStamina = false;
    }

    #endregion Stamina Function

    // Use this for initialization
    void Start () {
        //nanti ini diganti pake variable yang di simpen
        isRegenStamina = true;
        Initiated_Stamina();
        Initiated_Mana();
        Initiated_Health();

        if(!transisiCamera)
        {
            transisiCamera = Camera.main.GetComponent<ProCamera2DTransitionsFX>();
        }

        if(transisiCamera)
        {
            transisiCamera.TransitionEnter();
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!transisiCamera)
        {
            transisiCamera = Camera.main.GetComponent<ProCamera2DTransitionsFX>();
        }
    }

    void FixedUpdate()
    {
        if (isRegenStamina && CheckBoundaryUpper(currentStamina))
        {
            currentStamina = currentStamina + (staminaPoint * Time.deltaTime);
        }
    }
}
