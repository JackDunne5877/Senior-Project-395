using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class PlayerAnimation : MonoBehaviour
{
    public Animator animator; // Player animation
    private CharacterController _controller;
    private FirstPersonController fpc;

    private float speed; // Player speed
    private Vector2 direction; // Player movement direction as a vector
    Vector3 velocity;

    void Awake()
    {
        animator.SetBool("isDead", false);
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        fpc = GetComponent<FirstPersonController>();
    }



    // Update is called once per frame
    void Update()
    {

        velocity = transform.InverseTransformDirection(new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z));
        speed = velocity.magnitude / fpc.MoveSpeed;//relative to the player's walk speed, will make all run animations automatically faster

        if (speed > 0.01)
        {
            // Get the horizontal and vertical move direction of the player
            //direction.x = Input.GetAxis("Horizontal");
            direction.x = velocity.normalized.x;
            //direction.y = Input.GetAxis("Vertical");
            direction.y = velocity.normalized.z;

            //Sets the direction parameters in the animator
            animator.SetFloat("xDirection", direction.x);
            animator.SetFloat("yDirection", direction.y);
            animator.SetFloat("speed", speed);
        }
        else
        {
            direction.x = 0;
            direction.y = 0;
            speed = 0;

            //Sets the direction parameters in the animator
            animator.SetFloat("xDirection", direction.x);
            animator.SetFloat("yDirection", direction.y);
            animator.SetFloat("speed", speed);
        }

    }

    void setIsDead()
    {
        animator.SetBool("isDead", true);
    }
}
