using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* The purposes of the TakeZombieDamage script are:
 *      1.) Make the player lose health when attacked by a zombie
 *      2.) Update the health bar to reflect the player's current health
 *      3.) Show, then fade the damage overlay when attacked by a zombie
 */
public class TakeZombieDamage : MonoBehaviour
{
    public static int health; // Player starting health
    private HealthBar healthBar; // Health bar GUI
    public GameObject TakeDamageOverlay;
    

    void Start()
    {
        // Get player health
        health = SingletonManager.Instance.maxPlayerHealth;
        Debug.Log("Starting player health is " + health);

        // Initialize the health bar to be full
        healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        
        healthBar.slider.maxValue = health;
        healthBar.slider.value = health;
    }


    
    // Take damage from a zombie's attack
    void TakeDamage()
    {
        StartCoroutine(showTakeDamageOverlay());
        if (health > 0)
        {
            // The player loses 1 health
            health -= 1;
            Debug.Log("Player health is now " + health);

            // The health bar is updated
            healthBar.SetHealth(health);
            healthBar.SetFill();
            healthBar.SetHearts();
        }
        else
        {
            GetComponent<HUD_Controller>().playerDie();
        }
    }

    private float takeDamageOverlayMaxAlpha = 0.6f;
    private float takeDamageOverlayFadeSpeed = 0.6f;
    IEnumerator showTakeDamageOverlay()
    {
        Image overlay = TakeDamageOverlay.GetComponent<Image>();
        Color currentColor = overlay.color;
        currentColor.a = takeDamageOverlayMaxAlpha;
        overlay.color = currentColor;
        while(Mathf.Abs(currentColor.a - 0f) > 0.001f)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, 0f, takeDamageOverlayFadeSpeed * Time.deltaTime);
            overlay.color = currentColor;
            yield return null;
        }


    }
}
