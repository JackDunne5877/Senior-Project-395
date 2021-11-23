using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject spawnLocation;
    public GameObject player;
    private Vector3 respawnLocation;


    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = GameObject.FindGameObjectWithTag("SpawnPoint");

        respawnLocation = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCharacter() 
    {
        GameObject.Instantiate(player, spawnLocation.transform.position, Quaternion.identity);
        Debug.LogError("SpawnCharacter was called, not really an error");
    }
}
