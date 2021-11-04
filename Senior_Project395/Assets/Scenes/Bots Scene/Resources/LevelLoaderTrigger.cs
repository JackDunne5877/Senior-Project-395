using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelLoaderTrigger : MonoBehaviourPun
{
    [SerializeField]
    private int levelIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered");
        if (other.CompareTag("Player")) {
            PhotonNetwork.LoadLevel(levelIndex);

            Debug.Log("Player entered");
        }
    }
}
