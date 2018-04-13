using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    public int Current_Health;
    public int Current_Mana;
    public int Current_Stamina;
    
    //Boundary MAX Health
    public int Max_Health;
    public int Max_Mana;
    public int Max_Stamina;

    #region Health Function
    public void Initiated_Health()
    {
        Current_Health = Max_Health;
    }

    public void Increased_Health(int banyak_Health)
    {
        Current_Health += banyak_Health;
    }

    public void Decreased_Health(int banyak_Health)
    {
        Current_Health -= banyak_Health;
    }

    public bool CheckBelow_Health(int banyak_Health)
    {
        if (Current_Health < banyak_Health)
        {
            return true;
        }
        return false;
    }

    public int get_Health()
    {
        return Current_Health;
    }
    #endregion 

#region Mana Function
    public void Initiated_Mana()
    {
        Current_Mana = Max_Mana;
    }

    public void Increased_Mana(int banyak_Mana)
    {
        Current_Mana += banyak_Mana;
    }

    public void Decreased_Mana(int banyak_Mana)
    {
        Current_Mana -= banyak_Mana;
    }

    public bool CheckBelow_Mana(int banyak_Mana)
    {
        if (Current_Mana < banyak_Mana)
        {
            return true;
        }
        return false;
    }

    public int get_Mana()
    {
        return Current_Mana;
    }

    #endregion Mana Function

    #region Stamina Function
    public void Initiated_Stamina()
    {
        Current_Stamina = Max_Stamina;
    }

    public void Increased_Stamina(int banyak_Stamina)
    {
        Current_Stamina += banyak_Stamina;
    }

    public void Decreased_Stamina(int banyak_Stamina)
    {
        Current_Stamina -= banyak_Stamina;
    }

    public bool CheckBelow_Stamina(int banyak_Stamina)
    {
        if (Current_Stamina < banyak_Stamina)
        {
            return true;
        }
        return false;
    }
    public int get_Stamina()
    {
        return Current_Stamina;
    }
    #endregion Stamina Function

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
