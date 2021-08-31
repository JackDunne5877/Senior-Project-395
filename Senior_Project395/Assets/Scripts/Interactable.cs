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
            Debug.Log("interacting with object");
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        Debug.Log("Player no longer in range");
    }
}
