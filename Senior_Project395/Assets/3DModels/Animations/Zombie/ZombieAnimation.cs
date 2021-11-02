using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimation : MonoBehaviour
{
    public Animator animator; // Player animation

    private float speed; // Player speed
    private Vector2 direction; // Player movement direction as a vector
    Vector3 velocity;
    private NavMeshAgent nma;
    private Damageable zombieDamage;

    void Awake()
    {
        animator.SetBool("isDead", false);
    }

    private void Start()
    {
        nma = GetComponentInParent<NavMeshAgent>();
        zombieDamage = GetComponentInParent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {

        velocity = transform.InverseTransformDirection(new Vector3(nma.velocity.x, 0.0f, nma.velocity.z));
        speed = velocity.magnitude / nma.speed;//relative to the player's walk speed, will make all run animations automatically faster

        if (speed > 0.01)
        {
            // Get the horizontal and vertical move direction of the player
            //direction.x = Input.GetAxis("Horizontal");
            direction.x = velocity.normalized.x;
            //direction.y = Input.GetAxis("Vertical");
            direction.y = velocity.normalized.z;

            //Sets parameters in the animator
            animator.SetFloat("speed", speed);
        }
        else
        {
            direction.x = 0;
            direction.y = 0;
            speed = 0;

            //Sets the direction parameters in the animator
            animator.SetFloat("speed", speed);
        }

        if(zombieDamage.currentHealth <= 1)
        {
            setIsDead();
        }
        

    }

    public void attackTriggered()
    {
        animator.SetTrigger("Attack");
    }

    public void setIsDead()
    {
        animator.SetBool("isDead", true);
    }


}
