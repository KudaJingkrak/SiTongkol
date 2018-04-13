using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    public float Current_Health;
    public float Current_Mana;
    public float Current_Stamina;
    
    //Boundary MAX Health
    public float Max_Health;
    public float Max_Mana;
    public float Max_Stamina;

    //How much speedRate Stamina
    public float staminaPoint;
    public bool isRegenStamina;

    #region Health Function
    public void Initiated_Health()
    {
        Current_Health = Max_Health;
    }

    public void Increased_Health(float banyak_Health)
    {
        Current_Health += banyak_Health;
    }

    public void Decreased_Health(float banyak_Health)
    {
        Current_Health -= banyak_Health;
    }

    public bool CheckBelow_Health(float banyak_Health)
    {
        if (Current_Health < banyak_Health)
        {
            return true;
        }
        return false;
    }

    public float get_Health()
    {
        return Current_Health;
    }
    #endregion 

#region Mana Function
    public void Initiated_Mana()
    {
        Current_Mana = Max_Mana;
    }

    public void Increased_Mana(float banyak_Mana)
    {
        Current_Mana += banyak_Mana;
    }

    public void Decreased_Mana(float banyak_Mana)
    {
        Current_Mana -= banyak_Mana;
    }

    public bool CheckBelow_Mana(float banyak_Mana)
    {
        if (Current_Mana < banyak_Mana)
        {
            return true;
        }
        return false;
    }

    public float get_Mana()
    {
        return Current_Mana;
    }

    #endregion Mana Function

    #region Stamina Function

    IEnumerator Stamina_itr;

    public void Initiated_Stamina()
    {
        Current_Stamina = Max_Stamina;
    }

    public void Increased_Stamina(float banyak_Stamina)
    {
        Current_Stamina += banyak_Stamina;
    }

    public void Decreased_Stamina(float banyak_Stamina)
    {
        Current_Stamina -= banyak_Stamina;
    }

    public bool CheckBelow_Stamina(float banyak_Stamina)
    {
        if (Current_Stamina < banyak_Stamina)
        {
            return true;
        }
        return false;
    }

    public float get_Stamina()
    {
        return Current_Stamina;
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
            if (Current_Stamina <= Max_Stamina)
            {
                Current_Stamina += staminaPoint * Time.deltaTime;
            }
        }
	}
}
