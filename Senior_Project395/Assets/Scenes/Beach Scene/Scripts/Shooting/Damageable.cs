using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{

    //The box's current health point total
    public float healthCapacity = 3f;
    private float currentHealth;
    public int points = 100;
    public Material[] damageLevelMats;

    public void Start()
    {
        currentHealth = healthCapacity;
    }
    public int doDamage(int damageAmount) //returns points
    {
        //subtract damage amount when Damage function is called
        currentHealth -= (float)damageAmount;
        int currentMatIdx = (int)Mathf.Ceil((currentHealth / healthCapacity) * (float)(damageLevelMats.Length - 1));
        Debug.Log("current Mat: " + currentMatIdx);
        gameObject.GetComponent<Renderer>().material = damageLevelMats[currentMatIdx];

        //Check if health has fallen below zero
        if (currentHealth <= 0f)
        {
            //if health has fallen below zero, deactivate it 
            gameObject.SetActive(false);
            return points;
        }
        return 0;
    }
}
