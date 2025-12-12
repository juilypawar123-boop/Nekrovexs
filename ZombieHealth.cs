using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Score Settings")]
    public int pointsOnDeath = 10; // Add this line

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add points for killing the zombie
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoint(pointsOnDeath);
        }
        else
        {
            Debug.LogWarning("ScoreManager instance not found in scene!");
        }

        // Optional: Play a death animation or sound here
        // Example: animator.SetTrigger("Die");
        // Or instantiate a death effect prefab

        Destroy(gameObject);
    }
}
