using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class LevelLoaderTrigger : MonoBehaviourPun
{
    [SerializeField]
    private string levelName = "";
    [SerializeField]
    private bool isLoadingUIScene = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered");
        if (other.CompareTag("Player")) {

            if (isLoadingUIScene)
            {
                other.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                other.gameObject.SetActive(true);
            }

            PhotonNetwork.LoadLevel(levelName);

            Debug.Log("Player entered");
        }
    }
}
