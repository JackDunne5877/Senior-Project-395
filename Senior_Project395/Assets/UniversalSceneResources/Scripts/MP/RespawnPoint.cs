using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void Awake()
    {
        //make sure players transitioning levels get pulled to the spawn point
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            p.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //p.transform.position = RespawnPoint.transform.position;
            //p.SendMessage("SpawnCharacter");
        }
    }
}
