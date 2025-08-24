using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private float damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
    }
}
