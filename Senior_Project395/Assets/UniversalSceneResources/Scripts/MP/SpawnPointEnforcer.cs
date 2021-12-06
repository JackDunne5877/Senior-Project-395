using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointEnforcer : MonoBehaviour
{
    bool enforcedSpawnPosition = false;
    public Camera overlayCamera;

    private void Start()
    {
    }

    private void Update()
    {
        
        if (!enforcedSpawnPosition)
        {
            Debug.Log("enforcing spawn position...");
            enforcedSpawnPosition = true;
            StartCoroutine(waitThenReposition());
        }
        
    }

    IEnumerator waitThenReposition()
    {
        //make sure players transitioning levels get pulled to the spawn point
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            overlayCamera.gameObject.SetActive(true);
            p.gameObject.SetActive(false);
            //GameObject overlay = p.transform.Find("PlayerCameraRoot/MainCamera/Canvas/SettingSpawnPointOverlay").gameObject;

            //overlay.SetActive(true);

            yield return new WaitForSeconds(1f);
            while (Vector3.Distance(p.transform.position, transform.position) > 1)
            {
                p.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Debug.Log($"pulled player to respawn point {transform.position.x}, {transform.position.y}, {transform.position.z}");
                //p.transform.position = RespawnPoint.transform.position;
                //p.SendMessage("SpawnCharacter");
            }
            enforcedSpawnPosition = true;
            p.gameObject.SetActive(true);
            overlayCamera.gameObject.SetActive(false);
            //overlay.SetActive(false);
        }
    }
}
