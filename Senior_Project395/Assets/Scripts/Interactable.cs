using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public static bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

  

    private void Update()
    {
        if (StarterAssetsInputs.inspect) //if we're in range to interact
        {
            /*
            Here is where you can invoke an action, which might help trigger an animation
            interactAction.Invoke(); //instantiate event */



            ShowUI.showDesc = true;
            ShowUI.descriptionChecker();
            Debug.Log("interacting with object");
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            //here should be trigger a UI textbox saying "press f to inspect item" etc
            //uiObject.SetActive(true);
            ShowUI.showTxt = true;
            ShowUI.InteractChecker();
            Debug.Log("showTxt var is true");

            if (StarterAssetsInputs.inspect)
            {
                ShowUI.showDesc = true;
                ShowUI.descriptionChecker();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isInRange = false;

        //NOTE: might have to put this stuff in it's own script, but for now should be good
        //uiObject.SetActive(false);
        ShowUI.showTxt = false;
        ShowUI.showDesc = false;
        ShowUI.InteractChecker();
        Debug.Log("Player no longer in range");
        StarterAssetsInputs.inspect = false;
    }
}
