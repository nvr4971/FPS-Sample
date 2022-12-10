using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Taking damage");

            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(projectile.projectileDamage);
           // Destroy(projectile.gameObject);
        }
    }

    private void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    private void Die()
    {
        //Destroy(gameObject);
    }
}
