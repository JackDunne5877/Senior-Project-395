using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* This scripts creates a health bar GUI that is consistent with the player health in TakeZombieDamage.cs
 * This health bar:
 *      1.) Loses 1 health when the player takes damage
 *      2.) Automatically regenerates 1 health every 3 seconds so long as the player is alive
 *      3.) Changes color according to the remaining life
 
     */
public class HealthBar : MonoBehaviour
{
    // Health bar slider
    public Slider slider; // Health bar slider
    public Image fill; // Health bar solid color
    public Gradient gradient; // Health bar changing color

    // Hearts image
    public Image hearts; 
    public Sprite oneHeartImage;
    public Sprite twoHeartsImage;
    public Sprite threeHeartsImage;

    // Health
    private int maxHealth; // The player's max health
    private int currHealth; // The player's current health



    void Start()
    {
        // Set the initial health
        maxHealth = SingletonManager.Instance.maxPlayerHealth;

        // Get the slider component
        slider = gameObject.GetComponent<Slider>();
        slider.value = maxHealth;

        // Set the initial health bar color
        fill.color = gradient.Evaluate(1f);

        // Set the initial hearts image as 3 hearts
        hearts.sprite = threeHeartsImage;
    }



    // Set the value of the slider according to the player's health
    public void SetHealth(int health)
    {
        currHealth = health;
        slider.value = currHealth;

        // Begin regenerating health when it falls below maxHealth
        if (currHealth == maxHealth - 1)
        {
            StartCoroutine("AutoRegenerateHealth");
        }
    }
    


    // Change the health bar color according to the remaining amount of health
    public void SetFill()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


    // Change the number of hearts that appear next to the health bar according to the remaining amount of health
    public void SetHearts()
    {
        float normalizedHealth = (float)currHealth / (float)maxHealth;

        if (normalizedHealth > .68)
        {
            hearts.sprite = threeHeartsImage;
        }
        if (normalizedHealth <= .68 && normalizedHealth > .34)
        {
            hearts.sprite = twoHeartsImage;
        }
        if (normalizedHealth <= .34)
        {
            hearts.sprite = oneHeartImage;
        }
    }



    // Automatically regenerate health every 3 seconds
    IEnumerator AutoRegenerateHealth()
    {
        while (currHealth < maxHealth)
        {
            yield return new WaitForSeconds(3f);
            if (currHealth == 0 || currHealth == maxHealth) { break; } // Prevent regeneration if player died within the past 3 seconds
            currHealth++;
            TakeZombieDamage.health = currHealth;
            slider.value = currHealth;
            SetFill();
            SetHearts();
        }
    }
}
