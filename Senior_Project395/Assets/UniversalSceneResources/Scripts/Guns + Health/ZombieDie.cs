using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDie : MonoBehaviour
{
    private Damageable dmg;
    private NavMeshAgent nma;
    private AttackPlayer atk;
    private float deathTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        dmg = GetComponent<Damageable>();
        nma = GetComponent<NavMeshAgent>();
        atk = GetComponent<AttackPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dmg.currentHealth <= 1)
        {
            GetComponentInChildren<ZombieAnimation>().setIsDead();
            nma.isStopped = true;
            atk.zombieIsDead = true;
        }
    }

}
