using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Image healthStats, staminaStats;
    
    public void DisplayHealth(float health)
    {
        health /= 100f;
        healthStats.fillAmount = health;
    }

    public void DisplayStamina(float stamina)
    {
        stamina /= 100f;
        staminaStats.fillAmount = stamina;
    }
}
