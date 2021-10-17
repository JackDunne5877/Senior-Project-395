using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* The purposes of the TakeZombieDamage script are:
 *      1.) Make the player lose health when attacked by a zombie
 *      2.) Update the health bar to reflect the player's current health
 */
public class TakeZombieDamage : MonoBehaviour
{
    public static int health; // Player starting health
    private HealthBar healthBar; // Health bar GUI



    void Start()
    {
        // Get player health
        health = SingletonManager.Instance.maxPlayerHealth;

        // Initialize the health bar to be full
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        
        healthBar.slider.maxValue = health;
        healthBar.slider.value = health;
    }


    
    // Take damage from a zombie's attack
    void TakeDamage()
    {
        if (health > 0)
        {
            // The player loses 1 health
            health -= 1;

            // The health bar is updated
            healthBar.SetHealth(health);
            healthBar.SetFill();
            healthBar.SetHearts();
        }
    }
}
