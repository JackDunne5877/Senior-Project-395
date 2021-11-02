using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private float attackRange = 4f;
    private GameObject player;
    private bool canAttack = true;
    public float minAttackInterval = 2.5f;
    public float zombieAttackAnimOffset = 1.04f;
    public bool zombieIsDead = false;



    void Update()
    {
        // Attack a player that is within attacking range
        if (!zombieIsDead && InAttackRange() && canAttack)
        {
            canAttack = false;
            Attack();
            StartCoroutine(WaitForAttackRecharge(minAttackInterval));
        }
    }

 
    public IEnumerator WaitForAttackRecharge(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        canAttack = true;
    }

    public IEnumerator WaitForAnimationHitForDamageOffset(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        if (InAttackRange())
        {
            player.SendMessage("TakeDamage");
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
        
        GetComponentInChildren<ZombieAnimation>().attackTriggered();
        StartCoroutine(WaitForAnimationHitForDamageOffset(zombieAttackAnimOffset));
    }

}
