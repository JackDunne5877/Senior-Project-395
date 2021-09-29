using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InteractableGun : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public static bool isInRange;
    //public KeyCode interactKey;
    public bool isBeingInteracted = false;
    public bool isInRangeOfThisObject = false;
    public GameObject currentGun;
    public GameObject player;

    //Testing out dynamic text box to change description for each object
    public string objectDescription;

    private void Start()
    {
        currentGun.SetActive(true);
    }


    private void Update()
    {
        //if we're in range to interact
        //NOTE: clean up all input settings (here, interactable objects, gunsmenu), to be operated consistently
        if (/*StarterAssetsInputs.pickUpGun*/ Input.GetKeyDown(KeyCode.F) && isInRangeOfThisObject)
        {
            isBeingInteracted = true;
            ShowUI.uiObjectDesc.GetComponentInChildren<Text>().text = objectDescription;
            ShowUI.showDesc = true;
            ShowUI.descriptionChecker();

            Debug.Log("picking up gun");

            //add gun to inventory
            Debug.Log("adding weapon to inventory");
            player.GetComponentInChildren<GunsMenu>().addWeapon(currentGun);

            //instantiate currentGun as child of WeaponHolder, and reset positioning
            currentGun.transform.parent = GameObject.Find("WeaponHolder").transform;
            currentGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
            currentGun.transform.localPosition = Vector3.zero;

            currentGun.SetActive(false);

            //reset conditions (as ontrigger exit doesn't work after deleting gun
            //OnTriggerExit(GameObject.Find("MyPlayer").GetComponent<CapsuleCollider>());
            isInRange = false;
            isInRangeOfThisObject = false;
            isBeingInteracted = false;
            ShowUI.showTxt = false;
            ShowUI.showDesc = false;
            ShowUI.InteractChecker();
            Debug.Log("Player no longer in range");
            StarterAssetsInputs.interact = false;
            ShowUI.uiObjectDesc.GetComponentInChildren<Text>().text = "";

            

        }

    }

    //when entering/colliding with 3d physics sphere
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRangeOfThisObject = true;
            isInRange = true;
            ShowUI.showTxt = true;
            ShowUI.InteractChecker();
            player = other.gameObject;
        }
    }

    //when exiting range of 3d physics sphere
    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        isInRangeOfThisObject = false;
        isBeingInteracted = false;
        ShowUI.showTxt = false;
        ShowUI.showDesc = false;
        ShowUI.InteractChecker();
        Debug.Log("Player no longer in range");
        StarterAssetsInputs.interact = false;
        ShowUI.uiObjectDesc.GetComponentInChildren<Text>().text = "";
    }
}
