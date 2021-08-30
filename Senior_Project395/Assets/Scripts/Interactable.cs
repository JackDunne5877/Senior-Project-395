using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    private void Update()
    {
        if (isInRange) //if we're in range to interact
        {
            /*
            if (Input.GetKeyDown(interactKey))
            {
                PlayerInput.
                interactAction.Invoke(); //instantiate event
                Debug.Log("interacting with object");
            } */
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MyPlayer"))
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
