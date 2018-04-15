using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : BaseClass {

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
        currentHealth -= banyak_Health;
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
        currentStamina += banyak_Stamina;
    }

    public void Decreased_Stamina(float banyak_Stamina)
    {
        currentStamina -= banyak_Stamina;
    }

    public bool CheckBelow_Stamina(float banyak_Stamina)
    {
        if (currentStamina < banyak_Stamina)
        {
            return true;
        }
        return false;
    }

    public float get_Stamina()
    {
        return currentStamina;
    }

    public void Regenerating_Stamina()
    {
        isRegenStamina = true;
    }

    public void Stop_Regenerating()
    {
        isRegenStamina = false;
        if (Stamina_itr != null)
        {
            StopCoroutine(Stamina_itr);
        }
        Stamina_itr = Regenerate_Stamina(1f);
        StartCoroutine(Stamina_itr);
    }

    IEnumerator Regenerate_Stamina(float delay)
    {
        yield return new WaitForSeconds(delay);
        isRegenStamina = true;
    }
    #endregion Stamina Function

    // Use this for initialization
    void Start () {
        isRegenStamina = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRegenStamina)
        {
            if (currentStamina <= maxStamina)
            {
                currentStamina += staminaPoint * Time.deltaTime;
            }
        }
	}
}
