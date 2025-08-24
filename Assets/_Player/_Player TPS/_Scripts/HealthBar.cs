using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;


    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; // Set the current value to max health
    }

    public void setCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth; // Set the current health value
    }


}
