using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    public float currentWeaponDamage = 10f; // Example damage amount, can be adjusted

    public string hittableTag = "hittable"; // Tag for objects that can be hit
    public string playerTag = "Player"; // Tag for objects that can be hit
    public string enemyTag = "Enemy"; // Tag for objects that can be hit

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(hittableTag))
        //{
        if (other.CompareTag(playerTag))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            //EnemyStats enemyStats = other.GetComponent<EnemyStats>();

            if (playerStats != null)//&& !playerStats.isInvulnerable)
            {
                playerStats.TakeDamage(currentWeaponDamage); // Example damage value
            }
        }
        //if (other.CompareTag(enemyTag))
        //{
        //    //PlayerStats playerStats = other.GetComponent<PlayerStats>();
        //    EnemyStats enemyStats = other.GetComponent<EnemyStats>();

        //    if (enemyStats != null)//&& !playerStats.isInvulnerable)
        //    {
        //        enemyStats.TakeDamage(currentWeaponDamage); // Example damage value
        //    }
        //}
        //}
    }
}
