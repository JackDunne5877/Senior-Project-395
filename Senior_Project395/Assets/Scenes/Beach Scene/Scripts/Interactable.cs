using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public static bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public bool isBeingInteracted = false;
    public bool isInRangeOfThisObject = false;

    //Testing out dynamic text box to change description for each object
    public string objectDescription;

    private void Update()
    {
        //if we're in range to interact
        if (StarterAssetsInputs.interact && isInRangeOfThisObject)
        {
            isBeingInteracted = true;
            Debug.Log(objectDescription);
            ShowUI.uiObjectDesc.GetComponentInChildren<Text>().text = objectDescription;

            ShowUI.showDesc = true;
            ShowUI.descriptionChecker();
            Debug.Log("interacting with object");

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
