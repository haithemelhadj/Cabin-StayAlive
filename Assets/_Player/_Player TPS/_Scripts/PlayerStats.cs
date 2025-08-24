using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public float maxHealth;
    public float currentHealth;
    public float healthBalanceFormula = 10; // Example value, adjust as needed

    public HealthBar healthBar;

    public AnimatorHandler animatorHandler;
    public AnimationsHandler animationsHandler;


    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthlevel();
        currentHealth = healthLevel;
        healthBar.SetMaxHealth(maxHealth);
    }

    private float SetMaxHealthFromHealthlevel()
    {
        maxHealth = healthLevel * healthBalanceFormula;
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.setCurrentHealth(currentHealth);

        if(animatorHandler)
        {
            animatorHandler.PlayTargetAnimation("GetHit", true); // Play damage animation
            if (currentHealth <= 0)
            {
                currentHealth = 0; // Ensure health doesn't go below zero
                animatorHandler.PlayTargetAnimation("Death", true); // Play death animation
            }
        }
        else if (animationsHandler)
        {
            animationsHandler.PlayTargetAnimation("GetHit", true); // Play damage animation
            if (currentHealth <= 0)
            {
                currentHealth = 0; // Ensure health doesn't go below zero
                animationsHandler.PlayTargetAnimation("Death", true); // Play death animation
            }
        }
        else
        {
            Debug.LogWarning("AnimatorHandler and AnimationsHandler are not assigned.");
        }



    }



}
