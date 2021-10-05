using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{

    //The box's current health point total
    public int health = 3;
    public int points = 100;

    public int doDamage(int damageAmount) //returns points
    {
        //subtract damage amount when Damage function is called
        health -= damageAmount;

        //Check if health has fallen below zero
        if (health <= 0)
        {
            //if health has fallen below zero, deactivate it 
            gameObject.SetActive(false);
            return points;
        }
        return 0;
    }
}
