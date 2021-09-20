// MoveDestination.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveDirection : MonoBehaviour
{
    //private vars
    private NavMeshAgent agent;
    private GameObject[] players;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Nav Agent Found This Number of players: " + players.Length);
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent.destination = findClosestPlayer().position;
        
    }

    private Transform findClosestPlayer() {
        GameObject closestPlayer = players[0];
        
        foreach (var p in players) {
            if (Vector3.Distance(closestPlayer.transform.position, this.transform.position) >
                Vector3.Distance(p.transform.position, this.transform.position)) {
                closestPlayer = p;
            }
        }

        return closestPlayer.transform;
    }
}