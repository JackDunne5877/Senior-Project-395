using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private float attackRange = 1f;
    private GameObject player;



    void Update()
    {
        // Attack a player that is within attacking range
        if (InAttackRange())
        {
            Attack();
        }
    }



    // Determine if a player is within the zombie's attack range
    // Return true if the zombie is within attacking range
    bool InAttackRange()
    {
        // Use a RaycastHit
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 startPos = transform.position;

        // If there is an object with the "Player" tag within range, the RaycastHit will detect it
        if (Physics.Raycast(startPos, forward, out hit, attackRange) && hit.transform.gameObject.tag == "Player")
        {
            player = hit.transform.gameObject;
            return true;
        }

        return false;
    }
    


    // Deal damage to the a player
    void Attack()
    {
        player.SendMessage("TakeDamage");
    }
}
